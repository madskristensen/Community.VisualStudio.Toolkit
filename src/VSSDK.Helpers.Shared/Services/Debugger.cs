using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static class Debugger
    {
        public static Task<IVsDebugger> GetDebuggerAsync() => Helpers.GetServiceAsync<SVsShell, IVsDebugger>();
        public static Task<IVsDebugLaunch> GetDebugLaunchAsync() => Helpers.GetServiceAsync<SVsDebugLaunch, IVsDebugLaunch>();
        public static Task<IVsDebuggableProtocol> GetDebuggableProtocolAsync() => Helpers.GetServiceAsync<SVsDebuggableProtocol, IVsDebuggableProtocol>();
    }
}
