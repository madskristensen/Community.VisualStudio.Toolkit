using System;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Shell
{
    /// <summary>
    /// Extension methods for the <c>AsyncPackage</c> class.
    /// </summary>
    public static class PackageExtensions
    {
        /// <summary>
        /// A helper method for asynchronously open tool windows.
        /// </summary>
        /// <typeparam name="T">The type of tool window to open.</typeparam>
        /// <param name="package">An instance of an <see cref="AsyncPackage"/></param>
        public static async System.Threading.Tasks.Task ShowToolWindowAsync<T>(this AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
#if VS16
            ToolWindowPane window = await package.ShowToolWindowAsync(typeof(T), 0, true, package.DisposalToken);
#else
            ToolWindowPane window = package.FindToolWindow(typeof(T), 0, true);
#endif
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            var windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
