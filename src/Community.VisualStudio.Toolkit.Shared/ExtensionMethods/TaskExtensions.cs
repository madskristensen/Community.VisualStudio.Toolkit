using System;

namespace Microsoft.VisualStudio.Shell
{
    /// <summary>Extension methods for the <see cref="System.Threading.Tasks.Task"/> object.</summary>
    public static class TaskExtensions
    {
        /// <summary>Starts a .</summary>
        public static void FireAndForget(this System.Threading.Tasks.Task task)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await task.ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await ex.LogAsync();
                }
            });
        }
    }
}
