using System;
using System.Runtime.InteropServices;
using System.Threading;
using Community.VisualStudio.Toolkit;
using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using TestExtension;
using Task = System.Threading.Tasks.Task;

namespace VSSDK.TestExtension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("05271709-8845-42fb-9d10-621cc8cffc5d")]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), nameof(TestExtension), "General", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), nameof(TestExtension), "General", 0, 0, true)]
    [ProvideToolWindow(typeof(RunnerWindow), Style = VsDockStyle.Float, Window = ToolWindowGuids.SolutionExplorer)]
    [ProvideToolWindowVisibility(typeof(RunnerWindow), VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class TestExtensionPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await TestCommand.InitializeAsync(this);
            await RunnerWindowCommand.InitializeAsync(this);

            System.Windows.Media.Imaging.BitmapSource bitmap = await KnownMonikers.Reference.ToBitmapSourceAsync(16);
            var svc = await VS.Shell.GetImageServiceAsync() as IVsImageService2;
            IVsUIShell test = await VS.Shell.GetUIShellAsync();
            Assumes.Present(bitmap);
            Assumes.Present(svc);
            Assumes.Present(test);
        }
    }
}
