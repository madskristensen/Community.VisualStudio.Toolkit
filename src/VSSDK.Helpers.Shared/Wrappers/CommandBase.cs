using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudio.Helpers
{
    public abstract class BaseCommand<T> where T : BaseCommand<T>, new()
    {
        private CommandID _commandId { get; }

        protected BaseCommand(Guid guid, int id)
        {
            _commandId = new CommandID(guid, id);
        }

        public OleMenuCommand? Command { get; private set; }
        public AsyncPackage? Package { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            var instance = new T();

            instance.Command = new OleMenuCommand(instance.ExecuteInternal, instance._commandId);
            instance.Package = package;

            instance.Command.BeforeQueryStatus += (s, e) => { instance.BeforeQueryStatus(e); };
            instance.Command.Supported = false;

            IMenuCommandService commandService = await VS.Commanding.GetCommandServiceAsync();
            commandService.AddCommand(instance.Command);

            await instance.InitializeCompletedAsync();
        }

        protected virtual Task InitializeCompletedAsync()
        {
            return Task.CompletedTask;
        }

        private void ExecuteInternal(object sender, EventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async delegate
            {
                try
                {
                    await ExecuteAsync((OleMenuCmdEventArgs)e);
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
            });
        }

        protected virtual Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return Task.CompletedTask;
        }

        protected virtual void BeforeQueryStatus(EventArgs e)
        {
            // Leave empty
        }
    }
}