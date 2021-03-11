using System.Threading.Tasks;
using Microsoft.VisualStudio.TaskStatusCenter;

namespace Microsoft.VisualStudio.Helpers
{
    public partial class Notifications
    {
        public Task<IVsTaskStatusCenterService> GetTaskStatusCenterAsync() => VS.GetServiceAsync<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();
    }
}
