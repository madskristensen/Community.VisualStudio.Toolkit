using System.Threading.Tasks;
using Microsoft.VisualStudio.TaskStatusCenter;

namespace VS
{
    public static partial class Notifications
    {
        public static Task<IVsTaskStatusCenterService> GetTaskStatusCenterAsync() => Helpers.GetServiceAsync<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();

    }
}
