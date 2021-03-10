using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static partial class Notifications
    {
        public static Task<IVsThreadedWaitDialogFactory> GetThreadedWaitDialogAsync() => Helpers.GetServiceAsync<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();
        public static Task<IVsActivityLog> GetActivityLogAsync() => Helpers.GetServiceAsync<SVsActivityLog, IVsActivityLog>();
    }
}
