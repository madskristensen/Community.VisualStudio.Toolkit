using System.Threading.Tasks;
using Microsoft.VisualStudio.TaskStatusCenter;

namespace Microsoft.VisualStudio.Helpers
{
    public partial class Notifications
    {
        /// <summary>The Task Status Center is used to run background tasks and is located in the left-most side of the Status bar.</summary>
        public Task<IVsTaskStatusCenterService> GetTaskStatusCenterAsync() => VS.GetServiceAsync<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();
    }
}
