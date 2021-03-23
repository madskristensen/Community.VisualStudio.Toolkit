using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
#if VS16
using Microsoft.VisualStudio.TaskStatusCenter;
#endif

namespace VSSDK.Community.Toolkit
{
    /// <summary>A collection of services used to notify the user.</summary>
    public partial class Notifications
    {
        internal Notifications()
        { }

#if VS16

        /// <summary>The Task Status Center is used to run background tasks and is located in the left-most side of the Status bar.</summary>
        public Task<IVsTaskStatusCenterService> GetTaskStatusCenterAsync() => VS.GetServiceAsync<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();
#endif

        /// <summary>Used for background tasks that needs to block the UI if they take longer than the specified seconds.</summary>
        public Task<IVsThreadedWaitDialogFactory> GetThreadedWaitDialogAsync() => VS.GetServiceAsync<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();

        /// <summary>Used to write log messaged to the ActivityLog.xml file.</summary>
        public Task<IVsActivityLog> GetActivityLogAsync() => VS.GetServiceAsync<SVsActivityLog, IVsActivityLog>();
    }
}
