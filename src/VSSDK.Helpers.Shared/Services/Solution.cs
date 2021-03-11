using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Solution
    {
        internal Solution()
        { }

        /// <summary>
        /// Returns either a Project or ProjectItem. Returns null if Solution is Selected.
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

        ///<summary>Gets the full paths to the currently selected item(s) in the Solution Explorer.</summary>
        public async Task<IEnumerable<string>?> GetSelectedItemFilePathsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            DTE2? dte = await VS.GetServiceAsync<SDTE, DTE2>();

            var items = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;
            List<string> list = new();

            foreach (UIHierarchyItem selItem in items)
            {
                if (selItem.Object is ProjectItem item && item.Properties != null)
                {
                    list.Add(item.Properties.Item("FullPath").Value.ToString());
                }
            }

            return list;
        }

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
