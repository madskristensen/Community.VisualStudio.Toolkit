using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace VS
{
    public static class Windows
    {
        public static Task<IVsCallBrowser> GetCallBrowserAsync() => Helpers.GetServiceAsync<SVsCodeWindow, IVsCallBrowser>();
        public static Task<IVsClassView> GetClassViewAsync() => Helpers.GetServiceAsync<SVsClassView, IVsClassView>();
        public static Task<IVsCodeWindow> GetCodeWindowAsync() => Helpers.GetServiceAsync<SVsCodeWindow, IVsCodeWindow>();
        public static Task<IVsCommandWindow> GetCommandWindowAsync() => Helpers.GetServiceAsync<SVsCommandWindow, IVsCommandWindow>();
        public static Task<IVsObjBrowser> GetObjectBrowserAsync() => Helpers.GetServiceAsync<SVsObjBrowser, IVsObjBrowser>();
        public static Task<IVsOutputWindow> GetOutputWindowAsync() => Helpers.GetServiceAsync<SVsOutputWindow, IVsOutputWindow>();
        public static Task<IVsTaskList> GetTaskListAsync() => Helpers.GetServiceAsync<SVsTaskList, IVsTaskList>();
        public static Task<IVsToolbox2> GetToolboxAsync() => Helpers.GetServiceAsync<SVsToolbox, IVsToolbox2>();
    }
}