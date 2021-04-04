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
#if VS16
        public static System.Threading.Tasks.Task ShowToolWindowAsync<T>(this AsyncPackage package)
        {
            return package.ShowToolWindowAsync(typeof(T), 0, true, package.DisposalToken);
        }
#else
        public static async System.Threading.Tasks.Task ShowToolWindowAsync<T>(this AsyncPackage package)
        {
            ToolWindowPane window = package.FindToolWindow(typeof(T), 0, true);

            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
#endif
    }
}
