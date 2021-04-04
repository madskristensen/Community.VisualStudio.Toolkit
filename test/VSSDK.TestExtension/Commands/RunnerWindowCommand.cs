using System;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Community.VisualStudio.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace TestExtension
{
    internal sealed class RunnerWindowCommand : BaseCommand<RunnerWindowCommand>
    {
        public RunnerWindowCommand() : base(new Guid("cb765f49-fc35-4c14-93af-bb48ca4f2ce3"), 0x0100)
        { }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ToolWindowPane window = await Package.FindToolWindowAsync(typeof(RunnerWindow), 0, true, CancellationToken.None);

            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            var windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
