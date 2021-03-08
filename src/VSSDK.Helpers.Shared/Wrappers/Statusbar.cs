using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace VS
{
    public class Statusbar : ServiceWrapperBase<SVsStatusbar, IVsStatusbar>
    {
        public static string GetText() => Helpers.RunSync(GetTextAsync);
        public static async Task<string> GetTextAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.GetText(out var pszText);
                return pszText;
            }
            catch (Exception ex)
            {
                VsShellUtilities.LogError(ex.Source, ex.ToString());
                return null;
            }
        }

        public static void SetText(string text) => SetTextAsync(text).ConfigureAwait(false);
        public static async Task SetTextAsync(string text)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.SetText(text);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static void Clear() => ClearAsync().ConfigureAwait(false);
        public static async Task ClearAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Clear();
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static void StartAnimation(StatusAnimation animation) => StartAnimationAsync(animation).ConfigureAwait(false);
        public static async Task StartAnimationAsync(StatusAnimation animation)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Animation(1, animation);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static void EndAnimation(StatusAnimation animation) => EndAnimationAsync(animation).ConfigureAwait(false);
        public static async Task EndAnimationAsync(StatusAnimation animation)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Animation(0, animation);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public enum StatusAnimation
        {
            General = 0,
            Print = 1,
            Save = 2,
            Deploy = 3,
            Sync = 4,
            Build = 5,
            Find = 6
        }
    }
}