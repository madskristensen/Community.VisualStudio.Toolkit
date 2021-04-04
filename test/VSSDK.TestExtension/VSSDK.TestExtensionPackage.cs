using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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
    [ProvideBindingPath]
    [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), nameof(TestExtension), "General", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), nameof(TestExtension), "General", 0, 0, true)]
    [ProvideToolWindow(typeof(RunnerWindow), Style = VsDockStyle.Float, Window = ToolWindowGuids.SolutionExplorer)]
    [ProvideToolWindowVisibility(typeof(RunnerWindow), VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideToolWindow(typeof(ThemeWindow))]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class TestExtensionPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await TestCommand.InitializeAsync(this);
            await RunnerWindowCommand.InitializeAsync(this);
            await ThemeWindowCommand.InitializeAsync(this);

            System.Windows.Media.Imaging.BitmapSource bitmap = await KnownMonikers.Reference.ToBitmapSourceAsync(16);
            var svc = (IVsImageService2)await VS.Shell.GetImageServiceAsync();
            Microsoft.VisualStudio.ComponentModelHost.IComponentModel2 test = await VS.Shell.GetComponentModelAsync();
            Assumes.Present(bitmap);
            Assumes.Present(svc);
            Assumes.Present(test);
        }

        public override IVsAsyncToolWindowFactory GetAsyncToolWindowFactory(Guid toolWindowType)
        {
            return (toolWindowType == typeof(RunnerWindow).GUID) ||
                (toolWindowType == typeof(ThemeWindow).GUID)
                ? this
                : null;
        }

        protected override string GetToolWindowTitle(Type toolWindowType, int id)
        {
            if (toolWindowType == typeof(RunnerWindow))
            {
                return RunnerWindow.Title;
            }
            else if (toolWindowType == typeof(ThemeWindow))
            {
                return ThemeWindow.Title;
            }

            return GetToolWindowTitle(toolWindowType, id);
        }

        protected override async Task<object> InitializeToolWindowAsync(Type toolWindowType, int id, CancellationToken cancellationToken)
        {
            if (toolWindowType == typeof(RunnerWindow))
            {
                await Task.Yield();
                await Task.Delay(2000);
                await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
                return await VS.GetDTEAsync();
            }
            else if (toolWindowType == typeof(ThemeWindow))
            {
                return new ThemeWindowControlViewModel();
            }

            return base.InitializeToolWindowAsync(toolWindowType, id, cancellationToken);
        }
    }
}
