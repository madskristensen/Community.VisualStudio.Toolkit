using System;
using System.IO;
using System.Linq;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Community.VisualStudio.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace EnvDTE
{
    /// <summary>Extension methods for the Project class.</summary>
    public static class ProjectExtensions
    {
        /// <summary>Casts the Project to a SolutionFolder.</summary>
        public static SolutionFolder AsSolutionFolder(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return (SolutionFolder)project.Object;
        }

        /// <summary>Gets the root folder of any Visual Studio project.</summary>
        public static string? GetDirectory(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string? path = null;
            var properties = new[] { "FullPath", "ProjectPath", "ProjectDirectory" };

            foreach (var name in properties)
            {
                try
                {
                    if (project?.Properties.Item(name)?.Value is string fullPath)
                    {
                        path = fullPath;
                        break;
                    }
                }
                catch (Exception)
                { }
            }

            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            return path;
        }

        /// <summary>Adds one or more files to the project.</summary>
        public static async Task AddFilesToProjectAsync(this Project project, params string[] files)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (project == null || project.IsKind(ProjectTypes.ASPNET_Core, ProjectTypes.DOTNET_Core, ProjectTypes.SSDT))
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

        /// <summary>Check what kind the project is. Use the <see cref="ProjectKinds"/> list of strings.</summary>
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

    /// <summary>A list of known project types.</summary>
    public static class ProjectTypes
    {
        /// <summary>The project type of ASP.NET Core projects.</summary>
        public const string ASPNET_Core = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";

        /// <summary>The project type of .NET Core projects.</summary>
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";

        /// <summary>The project type of Misc projects.</summary>
        public const string MISC = "{66A2671D-8FB5-11D2-AA7E-00C04F688DDE}";

        /// <summary>The project type of Node.js projects.</summary>
        public const string NODE_JS = "{9092AA53-FB77-4645-B42D-1CCCA6BD08BD}";

        /// <summary>The project type of a solution folder.</summary>
        public const string SOLUTION_FOLDER = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

        /// <summary>The project type of SQL Server Data projects.</summary>
        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";

        /// <summary>The project type of UWP app projects.</summary>
        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";

        /// <summary>The project type of Web Application projects.</summary>
        public const string WAP = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

        /// <summary>The project type of the legacy website projects.</summary>
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
    }
}
