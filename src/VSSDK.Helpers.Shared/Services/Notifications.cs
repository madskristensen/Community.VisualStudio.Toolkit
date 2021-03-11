using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public partial class Notifications
    {
        internal Notifications()
        { }

        public Task<IVsThreadedWaitDialogFactory> GetThreadedWaitDialogAsync() => VS.GetServiceAsync<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();
        public Task<IVsActivityLog> GetActivityLogAsync() => VS.GetServiceAsync<SVsActivityLog, IVsActivityLog>();
        public Statusbar Statusbar => new();
    }
}
