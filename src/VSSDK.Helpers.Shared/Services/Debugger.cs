using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Debugger
    {
        internal Debugger()
        { }

        public Task<IVsDebugger> GetDebuggerAsync() => VS.GetServiceAsync<SVsShell, IVsDebugger>();
        public Task<IVsDebugLaunch> GetDebugLaunchAsync() => VS.GetServiceAsync<SVsDebugLaunch, IVsDebugLaunch>();
        public Task<IVsDebuggableProtocol> GetDebuggableProtocolAsync() => VS.GetServiceAsync<SVsDebuggableProtocol, IVsDebuggableProtocol>();
    }
}
