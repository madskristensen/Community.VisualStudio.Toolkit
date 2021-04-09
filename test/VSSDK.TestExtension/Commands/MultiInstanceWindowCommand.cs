using System;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace TestExtension
{
    internal sealed class MultiInstanceWindowCommand : BaseCommand<MultiInstanceWindowCommand>
    {
        public MultiInstanceWindowCommand() : base(new Guid("cb765f49-fc35-4c14-93af-bb48ca4f2ce3"), 0x0102)
        { }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            // Create the window with the first free ID.
            for (var i = 0; i < 10; i++)
            {
                ToolWindowPane window = await MultiInstanceWindow.ShowAsync(id: i, create: false);

                if (window == null)
                {
                    await MultiInstanceWindow.ShowAsync(id: i, create: true);
                    break;
                }
            }
        }
    }
}
