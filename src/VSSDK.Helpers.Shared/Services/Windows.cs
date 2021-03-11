using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Windows
    {
        internal Windows()
        { }

        public Task<IVsCallBrowser> GetCallBrowserAsync() => VS.GetServiceAsync<SVsCodeWindow, IVsCallBrowser>();
        public Task<IVsClassView> GetClassViewAsync() => VS.GetServiceAsync<SVsClassView, IVsClassView>();
        public Task<IVsCodeWindow> GetCodeWindowAsync() => VS.GetServiceAsync<SVsCodeWindow, IVsCodeWindow>();
        public Task<IVsCommandWindow> GetCommandWindowAsync() => VS.GetServiceAsync<SVsCommandWindow, IVsCommandWindow>();
        public Task<IVsObjBrowser> GetObjectBrowserAsync() => VS.GetServiceAsync<SVsObjBrowser, IVsObjBrowser>();
        public Task<IVsOutputWindow> GetOutputWindowAsync() => VS.GetServiceAsync<SVsOutputWindow, IVsOutputWindow>();
        public Task<IVsTaskList> GetTaskListAsync() => VS.GetServiceAsync<SVsTaskList, IVsTaskList>();
        public Task<IVsToolbox2> GetToolboxAsync() => VS.GetServiceAsync<SVsToolbox, IVsToolbox2>();
    }
}