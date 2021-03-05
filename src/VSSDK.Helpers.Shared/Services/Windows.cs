using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace VS
{
    public static class Windows
    {
        // Output Window
        public static IVsOutputWindow GetOutputWindow() => Helpers.GetService<SVsOutputWindow, IVsOutputWindow>();
        public static async Task<IVsOutputWindow> GetOutputWindowAsync() => await Helpers.GetServiceAsync<SVsOutputWindow, IVsOutputWindow>();

        // Command Window
        public static IVsCommandWindow GetCommandWindow() => Helpers.GetService<SVsCommandWindow, IVsCommandWindow>();
        public static async Task<IVsCommandWindow> GetCommandWindowAsync() => await Helpers.GetServiceAsync<SVsCommandWindow, IVsCommandWindow>();

        // Command Window
        public static IVsCodeWindow GetCodeWindow() => Helpers.GetService<SVsCodeWindow, IVsCodeWindow>();
        public static async Task<IVsCodeWindow> GetCodeWindowAsync() => await Helpers.GetServiceAsync<SVsCodeWindow, IVsCodeWindow>();

        // Call browser
        public static IVsCallBrowser GetCallBrowser() => Helpers.GetService<SVsCodeWindow, IVsCallBrowser>();
        public static async Task<IVsCallBrowser> GetCallBrowserAsync() => await Helpers.GetServiceAsync<SVsCodeWindow, IVsCallBrowser>();

        // Class view
        public static IVsClassView GetClassView() => Helpers.GetService<SVsClassView, IVsClassView>();
        public static async Task<IVsClassView> GetClassViewAsync() => await Helpers.GetServiceAsync<SVsClassView, IVsClassView>();

        // Object browser
        public static IVsObjBrowser GetObjectBrowser() => Helpers.GetService<SVsObjBrowser, IVsObjBrowser>();
        public static async Task<IVsObjBrowser> GetObjectBrowserAsync() => await Helpers.GetServiceAsync<SVsObjBrowser, IVsObjBrowser>();

        //svstasklist
        //svstoolbox
    }
}
