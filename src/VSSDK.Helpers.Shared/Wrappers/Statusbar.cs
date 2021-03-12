using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudio.Helpers
{
    public class Statusbar
    {
        internal Statusbar()
        { }

        private Task<IVsStatusbar> GetServiceAsync()
        {
            return VS.GetServiceAsync<SVsStatusbar, IVsStatusbar>();
        }

        public async Task<string?> GetTextAsync()
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

        public async Task SetTextAsync(string text)
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
                await ex.LogAsync();
            }
        }

        public async Task ClearAsync()
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
                await ex.LogAsync();
            }
        }

        public async Task StartAnimationAsync(StatusAnimation animation)
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
                await ex.LogAsync();
            }
        }

        public async Task EndAnimationAsync(StatusAnimation animation)
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
                await ex.LogAsync();
            }

        }
    }

    /// <summary>A list of built-in animation visuals for the status bar.</summary>
    public enum StatusAnimation
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        General = 0,
        Print = 1,
        Save = 2,
        Deploy = 3,
        Sync = 4,
        Build = 5,
        Find = 6
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}