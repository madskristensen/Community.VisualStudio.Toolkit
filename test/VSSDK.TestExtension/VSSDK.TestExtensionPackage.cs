using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Helpers;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace VSSDK.TestExtension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("05271709-8845-42fb-9d10-621cc8cffc5d")]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class TestExtensionPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            await VS.Notifications.Statusbar.SetTextAsync("Test");
            var text = await VS.Notifications.Statusbar.GetTextAsync();
            await VS.Notifications.Statusbar.SetTextAsync(text + " OK");

            var ex = new Exception(nameof(TestExtension));
            await ex.LogAsync();
        }
    }
}
