using System;
using System.IO;
using System.Linq;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Helpers;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace EnvDTE
{
    public static class ProjectExtensions
    {
        public static ProjectItem? AddFileToProject(this Project project, string file)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (project.DTE.Solution.FindProjectItem(file) == null)
            {
                return project.ProjectItems.AddFromFile(file);
            }

            return null;
        }

        public static SolutionFolder AsSolutionFolder(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return (SolutionFolder)project.Object;
        }

        /// <summary>Gets the root folder of any Visual Studio project.</summary>
        public static string? GetRootFolder(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (string.IsNullOrEmpty(project.FullName))
            {
                return null;
            }

            string? fullPath;

            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (ArgumentException)
            {
                try
                {
                    // MFC projects don't have FullPath, and there seems to be no way to query existence
                    fullPath = project.Properties.Item("ProjectDirectory").Value as string;
                }
                catch (ArgumentException)
                {
                    // Installer projects have a ProjectPath.
                    fullPath = project.Properties.Item("ProjectPath").Value as string;
                }
            }

            if (string.IsNullOrEmpty(fullPath))
            {
                return File.Exists(project.FullName) ? Path.GetDirectoryName(project.FullName) : null;
            }

            if (Directory.Exists(fullPath))
            {
                return fullPath;
            }

            if (File.Exists(fullPath))
            {
                return Path.GetDirectoryName(fullPath);
            }

            return null;
        }

        public static async Task AddFilesToProjectAsync(this Project project, params string[] files)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (project == null || project.IsKind(ProjectTypes.ASPNET_5, ProjectTypes.DOTNET_Core, ProjectTypes.SSDT))
            {
                return;
            }

            DTE2? dte = await VS.GetDTEAsync();

            if (project.IsKind(ProjectTypes.WEBSITE_PROJECT))
            {
                Command command = dte.Commands.Item("SolutionExplorer.Refresh");

                if (command.IsAvailable)
                {
                    dte.ExecuteCommand(command.Name);
                }

                return;
            }

            IVsSolution? solutionService = await VS.GetServiceAsync<SVsSolution, IVsSolution>();
            solutionService.GetProjectOfUniqueName(project.UniqueName, out IVsHierarchy? hierarchy);

            if (hierarchy == null)
            {
                return;
            }

            var ip = (IVsProject)hierarchy;
            var result = new VSADDRESULT[files.Count()];

            ip.AddItem(VSConstants.VSITEMID_ROOT,
                       VSADDITEMOPERATION.VSADDITEMOP_LINKTOFILE,
                       string.Empty,
                       (uint)files.Count(),
                       files.ToArray(),
                       IntPtr.Zero,
                       result);
        }

        public static bool IsKind(this Project project, params string[] kindGuids)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var guid in kindGuids)
            {
                if (project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public const string MISC = "{66A2671D-8FB5-11D2-AA7E-00C04F688DDE}";
        public const string NODE_JS = "{9092AA53-FB77-4645-B42D-1CCCA6BD08BD}";
        public const string SOLUTION_FOLDER = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";
        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";
        public const string WAP = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
    }
}
