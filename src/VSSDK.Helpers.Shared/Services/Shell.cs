using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VS
{
    public static class Shell
    {
        // Shell
        public static IVsShell GetShell() => Helpers.GetService<SVsShell, IVsShell>();
        public static async Task<IVsShell> GetShellAsync() => await Helpers.GetServiceAsync<SVsShell, IVsShell>();

        // UI Shell
        public static IVsUIShell GetUIShell() => Helpers.GetService<SVsUIShell, IVsUIShell>();
        public static async Task<IVsUIShell> GetUIShellAsync() => await Helpers.GetServiceAsync<SVsUIShell, IVsUIShell>();

        // App command line
        public static IVsAppCommandLine GetAppCommandLine() => Helpers.GetService<SVsAppCommandLine, IVsAppCommandLine>();
        public static async Task<IVsAppCommandLine> GetAppCommandLineAsync() => await Helpers.GetServiceAsync<SVsAppCommandLine, IVsAppCommandLine>();

        // Image service
        public static IVsImageService2 GetImageService() => Helpers.GetService<SVsImageService, IVsImageService2>();
        public static async Task<IVsImageService2> GetImageServiceAsync() => await Helpers.GetServiceAsync<SVsImageService, IVsImageService2>();

        // Theming
        public static IVsFontAndColorCacheManager GetFontAndColorCacheManager() => Helpers.GetService<SVsFontAndColorCacheManager, IVsFontAndColorCacheManager>();
        public static async Task<IVsFontAndColorCacheManager> GetFontAndColorCacheManagerAsync() => await Helpers.GetServiceAsync<SVsFontAndColorCacheManager, IVsFontAndColorCacheManager>();

        public static IVsFontAndColorStorage GetFontAndColorStorage() => Helpers.GetService<SVsFontAndColorStorage, IVsFontAndColorStorage>();
        public static async Task<IVsFontAndColorStorage> GetFontAndColorStorageAsync() => await Helpers.GetServiceAsync<SVsFontAndColorStorage, IVsFontAndColorStorage>();

        // Tools Options
        public static IVsToolsOptions GetToolsOptions() => Helpers.GetService<SVsToolsOptions, IVsToolsOptions>();
        public static async Task<IVsToolsOptions> GetToolsOptionsAsync() => await Helpers.GetServiceAsync<SVsToolsOptions, IVsToolsOptions>();

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
