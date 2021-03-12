using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    /// <summary>A collection of services used to notify the user.</summary>
    public partial class Notifications
    {
        internal Notifications()
        { }

        /// <summary>Used for background tasks that needs to block the UI if they take longer than the specified seconds.</summary>
        public Task<IVsThreadedWaitDialogFactory> GetThreadedWaitDialogAsync() => VS.GetServiceAsync<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();

        /// <summary>Used to write log messaged to the ActivityLog.xml file.</summary>
        public Task<IVsActivityLog> GetActivityLogAsync() => VS.GetServiceAsync<SVsActivityLog, IVsActivityLog>();

        /// <summary>Used to write messages to or animate the status bar.</summary>
        public Statusbar Statusbar => new();
    }
}
