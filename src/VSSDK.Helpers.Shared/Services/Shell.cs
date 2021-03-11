using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Shell
    {
        internal Shell()
        { }

        public Task<IVsShell> GetShellAsync() => VS.GetServiceAsync<SVsShell, IVsShell>();
        public Task<IVsUIShell> GetUIShellAsync() => VS.GetServiceAsync<SVsUIShell, IVsUIShell>();
        public Task<IVsAppCommandLine> GetAppCommandLineAsync() => VS.GetServiceAsync<SVsAppCommandLine, IVsAppCommandLine>();
        public Task<IVsImageService2> GetImageServiceAsync() => VS.GetServiceAsync<SVsImageService, IVsImageService2>();
        public Task<IVsFontAndColorCacheManager> GetFontAndColorCacheManagerAsync() => VS.GetServiceAsync<SVsFontAndColorCacheManager, IVsFontAndColorCacheManager>();
        public Task<IVsFontAndColorStorage> GetFontAndColorStorageAsync() => VS.GetServiceAsync<SVsFontAndColorStorage, IVsFontAndColorStorage>();
        public Task<IVsToolsOptions> GetToolsOptionsAsync() => VS.GetServiceAsync<SVsToolsOptions, IVsToolsOptions>();

        public async Task<bool> OpenDocumentViaProjectAsync(string fileName)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IVsUIShellOpenDocument openDoc = await VS.GetServiceAsync<SVsUIShellOpenDocument, IVsUIShellOpenDocument>();

            System.Guid viewGuid = VSConstants.LOGVIEWID_TextView;
            if (ErrorHandler.Succeeded(openDoc.OpenDocumentViaProject(fileName, ref viewGuid, out _, out _, out _, out IVsWindowFrame frame)))
            {
                if (frame != null)
                {
                    frame.Show();
                    return true;
                }
            }

            return false;
        }
    }
}
