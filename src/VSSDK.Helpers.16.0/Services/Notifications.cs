using System.Threading.Tasks;
using Microsoft.VisualStudio.TaskStatusCenter;

namespace VS
{
    public static partial class Notifications
    {
        // Task Status Center
        public static IVsTaskStatusCenterService GetTaskStatusCenter() => Helpers.GetService<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();
        public static async Task<IVsTaskStatusCenterService> GetTaskStatusCenterAsync() => await Helpers.GetServiceAsync<SVsTaskStatusCenterService, IVsTaskStatusCenterService>();

    }
}
