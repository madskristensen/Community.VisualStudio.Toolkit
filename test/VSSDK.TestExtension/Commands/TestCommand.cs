using System;
using Microsoft.VisualStudio.Shell;
using VSSDK.Community.Toolkit;
using Task = System.Threading.Tasks.Task;

namespace TestExtension
{
    public class TestCommand : BaseCommand<TestCommand>
    {
        public TestCommand() : base(Guid.Empty, 123)
        { }

        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return base.ExecuteAsync(e);
        }
    }
}
