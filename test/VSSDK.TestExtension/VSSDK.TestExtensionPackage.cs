using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
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

            // async
            await VS.Statusbar.SetTextAsync("Test");
            var text = await VS.Statusbar.GetTextAsync();
            await VS.Statusbar.SetTextAsync(text + " OK");

            await Task.Delay(2000);

            // sync
#pragma warning disable VSTHRD103 // Call async methods when in an async method
            VS.Statusbar.SetText("test 2");
            text = VS.Statusbar.GetText();
            VS.Statusbar.SetText(text + " OK");
#pragma warning restore VSTHRD103 // Call async methods when in an async method
        }
    }
}
