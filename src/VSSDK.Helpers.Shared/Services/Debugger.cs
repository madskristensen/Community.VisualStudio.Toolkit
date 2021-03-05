using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static class Debugger
    {
        // Debugger
        public static IVsDebugger GetDebugger() => Helpers.GetService<SVsShellDebugger, IVsDebugger>();
        public static async Task<IVsDebugger> GetDebuggerAsync() => await Helpers.GetServiceAsync<SVsShell, IVsDebugger>();

        // Debug Launch
        public static IVsDebugLaunch GetDebugLaunch() => Helpers.GetService<SVsDebugLaunch, IVsDebugLaunch>();
        public static async Task<IVsDebugLaunch> GetDebugLaunchAsync() => await Helpers.GetServiceAsync<SVsDebugLaunch, IVsDebugLaunch>();

        // Deb staticuggable protocol
        public static IVsDebuggableProtocol GetDebuggableProtocol() => Helpers.GetService<SVsDebuggableProtocol, IVsDebuggableProtocol>();
        public static async Task<IVsDebuggableProtocol> GetDebuggableProtocolAsync() => await Helpers.GetServiceAsync<SVsDebuggableProtocol, IVsDebuggableProtocol>();
    }
}
