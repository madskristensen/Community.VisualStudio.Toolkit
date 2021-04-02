using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Community.VisualStudio.Toolkit
{
    /// <summary>A collection of services and helpers related to solutions.</summary>
    public class Solution
    {
        internal Solution()
        { }

        /// <summary>Provides top-level manipulation or maintenance of the solution.</summary>
        public Task<IVsSolution> GetSolutionAsync() => VS.GetServiceAsync<SVsSolution, IVsSolution>();

        /// <summary>
        /// Returns either a <see cref="Project"/> or <see cref="ProjectItem" />. Returns null sf Solution is selected.
        /// </summary>
        public async Task<object?> GetSelectedItemAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            object? selectedObject = null;
            
            IVsMonitorSelection? monitorSelection = await VS.GetServiceAsync<SVsShellMonitorSelection, IVsMonitorSelection>();
            IntPtr hierarchyPointer = IntPtr.Zero;
            IntPtr selectionContainerPointer = IntPtr.Zero;

            try
            {
                monitorSelection.GetCurrentSelection(out hierarchyPointer,
                                                 out var itemId,
                                                 out IVsMultiItemSelect multiItemSelect,
                                                 out selectionContainerPointer);

                if (Marshal.GetTypedObjectForIUnknown(hierarchyPointer, typeof(IVsHierarchy)) is IVsHierarchy selectedHierarchy)
                {
                    ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(itemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out selectedObject));
                }
            }
            catch (Exception ex)
            {
                await ex.LogAsync();
            }
            finally
            {
                Marshal.Release(hierarchyPointer);
                Marshal.Release(selectionContainerPointer);
            }

            return selectedObject;
        }


        /// <summary>
        /// Returns an array of either <see cref="Project"/> or <see cref="ProjectItem" />.
        /// </summary>
        public async Task<IEnumerable<object>?> GetSelectedItemsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2? dte = await VS.GetServiceAsync<SDTE, DTE2>();

            var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;
            List<object> list = new();

            foreach (UIHierarchyItem selItem in items)
            {
                list.Add(selItem.Object);
            }

            return list;
        }

        /// <summary>
        /// Returns the selected <see cref="ProjectItem" /> or null.
        /// </summary>
        public async Task<ProjectItem?> GetSelectedProjectItemAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            return await GetSelectedItemAsync() as ProjectItem;
        }

        /// <summary>
        /// Returns an array of the selected <see cref="ProjectItem" />s.
        /// </summary>
        public async Task<IEnumerable<ProjectItem>?> GetSelectedProjectItemsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2? dte = await VS.GetServiceAsync<SDTE, DTE2>();

            var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;
            List<ProjectItem> list = new();
            
            foreach (UIHierarchyItem selItem in items)
            {
                if (selItem is ProjectItem pi)
                {
                    list.Add(pi);
                }
            }

            return list;
        }

        /// <summary>Gets the active project.</summary>
        public async Task<Project?> GetActiveProjectAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2? dte = await VS.GetServiceAsync<SDTE, DTE2>();

            try
            {
                if (dte.ActiveSolutionProjects is Array projects && projects.Length > 0)
                {
                    return projects.GetValue(0) as Project;
                }
            }
            catch (Exception ex)
            {
                await ex.LogAsync();
            }

            return null;
        }
    }
}
