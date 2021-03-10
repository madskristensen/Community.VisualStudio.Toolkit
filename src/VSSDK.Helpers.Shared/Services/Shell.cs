using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static class Shell
    {
        public static Task<IVsShell> GetShellAsync() => Helpers.GetServiceAsync<SVsShell, IVsShell>();
        public static Task<IVsUIShell> GetUIShellAsync() => Helpers.GetServiceAsync<SVsUIShell, IVsUIShell>();
        public static Task<IVsAppCommandLine> GetAppCommandLineAsync() => Helpers.GetServiceAsync<SVsAppCommandLine, IVsAppCommandLine>();
        public static Task<IVsImageService2> GetImageServiceAsync() => Helpers.GetServiceAsync<SVsImageService, IVsImageService2>();
        public static Task<IVsFontAndColorCacheManager> GetFontAndColorCacheManagerAsync() => Helpers.GetServiceAsync<SVsFontAndColorCacheManager, IVsFontAndColorCacheManager>();
        public static Task<IVsFontAndColorStorage> GetFontAndColorStorageAsync() => Helpers.GetServiceAsync<SVsFontAndColorStorage, IVsFontAndColorStorage>();
        public static Task<IVsToolsOptions> GetToolsOptionsAsync() => Helpers.GetServiceAsync<SVsToolsOptions, IVsToolsOptions>();

        public static async Task<bool> OpenDocumentViaProjectAsync(string fileName)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IVsUIShellOpenDocument openDoc = await Helpers.GetServiceAsync<SVsUIShellOpenDocument, IVsUIShellOpenDocument>();

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
