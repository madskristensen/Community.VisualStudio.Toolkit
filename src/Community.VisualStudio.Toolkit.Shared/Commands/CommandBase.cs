using System;
using System.ComponentModel.Design;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Community.VisualStudio.Toolkit
{
    /// <summary>
    /// A base class that makes it easier to handle commands.
    /// </summary>
    /// <typeparam name="T">The implementation type itself.</typeparam>
    public abstract class BaseCommand<T> where T : BaseCommand<T>, new()
    {
        private CommandID _commandId { get; }

        /// <summary>
        /// Creates a new instance of the implementation.
        /// </summary>
        protected BaseCommand(Guid commandGuid, int commandId)
        {
            _commandId = new CommandID(commandGuid, commandId);
        }

        /// <summary>The command object associated with the command ID (guid/id).</summary>
        public OleMenuCommand? Command { get; private set; }

        /// <summary>The package class that initialized this class.</summary>
        public AsyncPackage? Package { get; private set; }

        /// <summary>Initializes the command.</summary>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            var instance = new T();

            instance.Command = new OleMenuCommand(instance.ExecuteInternal, instance._commandId);
            instance.Package = package;

            instance.Command.BeforeQueryStatus += (s, e) => { instance.BeforeQueryStatus(e); };
            instance.Command.Supported = false;

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as  IMenuCommandService;
            Assumes.Present(commandService);

            commandService?.AddCommand(instance.Command);

            await instance.InitializeCompletedAsync();
        }

        /// <summary>Allows the implementor to manipulate the command before its execution.</summary>
        protected virtual Task InitializeCompletedAsync()
        {
            return Task.CompletedTask;
        }

        private void ExecuteInternal(object sender, EventArgs e)
        {
            Assumes.Present(Package);
            Package?.JoinableTaskFactory.RunAsync(async delegate
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

        /// <summary>Executes when the command is invoked.</summary>
        protected virtual Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return Task.CompletedTask;
        }

        /// <summary>Override this method to control the commands visibility and other properties.</summary>
        protected virtual void BeforeQueryStatus(EventArgs e)
        {
            // Leave empty
        }
    }
}