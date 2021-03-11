using System.ComponentModel.Design;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Commanding
    {
        internal Commanding()
        { }

        public Task<IMenuCommandService> GetCommandServiceAsync() => VS.GetServiceAsync<IMenuCommandService, IMenuCommandService>();
        public Task<IVsRegisterPriorityCommandTarget> GetPriorityCommandTargetAsync() => VS.GetServiceAsync<SVsRegisterPriorityCommandTarget, IVsRegisterPriorityCommandTarget>();
    }
}
