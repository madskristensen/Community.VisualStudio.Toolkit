using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static class Solution
    {
        /// <summary>
        /// Returns either a Project or ProjectItem. Returns null if Solution is Selected.
        /// </summary>
        public static object? GetSelectedItem()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            object? selectedObject = null;

            IVsMonitorSelection monitorSelection = Helpers.GetService<SVsShellMonitorSelection, IVsMonitorSelection>();

            try
            {
                monitorSelection.GetCurrentSelection(out IntPtr hierarchyPointer,
                                                 out var itemId,
                                                 out IVsMultiItemSelect multiItemSelect,
                                                 out IntPtr selectionContainerPointer);

                if (Marshal.GetTypedObjectForIUnknown(hierarchyPointer, typeof(IVsHierarchy)) is IVsHierarchy selectedHierarchy)
                {
                    ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(itemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out selectedObject));
                }

                Marshal.Release(hierarchyPointer);
                Marshal.Release(selectionContainerPointer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
            }

            return selectedObject;
        }

        ///<summary>Gets the full paths to the currently selected item(s) in the Solution Explorer.</summary>
        public static IEnumerable<string> GetSelectedItemFilePaths()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var items = (Array)Helpers.GetDTE().ToolWindows.SolutionExplorer.SelectedItems;

            foreach (UIHierarchyItem selItem in items)
            {
                if (selItem.Object is ProjectItem item && item.Properties != null)
                {
                    yield return item.Properties.Item("FullPath").Value.ToString();
                }
            }
        }

        public static Project? GetActiveProject()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                if (Helpers.GetDTE().ActiveSolutionProjects is Array projects && projects.Length > 0)
                {
                    return projects.GetValue(0) as Project;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
            }

            return null;
        }
    }
}
