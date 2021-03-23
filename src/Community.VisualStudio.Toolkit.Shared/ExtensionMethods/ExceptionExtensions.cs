using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Community.VisualStudio.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace System
{
    /// <summary>Extension methods for the Exception class.</summary>
    public static class ExceptionExtensions
    {
        private const string _paneTitle = "Extensions";

        private static IVsOutputWindowPane? _pane;

        /// <summary>Log the error to the Output Window.</summary>
        public static async Task LogAsync(this Exception exception)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                if (await EnsurePaneAsync())
                {
                    _pane?.OutputString(exception + Environment.NewLine);
                }
            }
            catch
            {
                // Swallow the exception
            }
        }

        private static async Task<bool> EnsurePaneAsync()
        {
            if (_pane == null)
            {
                try
                {
                    if (_pane == null)
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                        IVsOutputWindow output = await VS.Windows.GetOutputWindowAsync();
                        var guid = new Guid();

                        ErrorHandler.ThrowOnFailure(output.CreatePane(ref guid, _paneTitle, 1, 1));
                        ErrorHandler.ThrowOnFailure(output.GetPane(ref guid, out _pane));
                    }
                }
                catch
                {
                    // Swallow the exception
                }
            }

            return _pane != null;
        }
    }
}