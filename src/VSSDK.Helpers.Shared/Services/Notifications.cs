using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static partial class Notifications
    {
        // Threaded Wait Dialog
        public static IVsThreadedWaitDialogFactory GetThreadedWaitDialog() => Helpers.GetService<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();
        public static async Task<IVsThreadedWaitDialogFactory> GetThreadedWaitDialogAsync() => await Helpers.GetServiceAsync<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();

        // Activity Log
        public static IVsActivityLog GetActivityLog() => Helpers.GetService<SVsActivityLog, IVsActivityLog>();
        public static async Task<IVsActivityLog> GetActivityLogAsync() => await Helpers.GetServiceAsync<SVsActivityLog, IVsActivityLog>();
    }
}
