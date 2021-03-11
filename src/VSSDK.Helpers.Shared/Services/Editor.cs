using System.Threading.Tasks;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.VisualStudio.Helpers
{
    public class Editor
    {
        internal Editor()
        { }

        public async Task<IWpfTextView?> GetCurrentWpfTextViewAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IComponentModel compService = await VS.GetServiceAsync<SComponentModel, IComponentModel>();
            IVsEditorAdaptersFactoryService? editorAdapter = compService.GetService<IVsEditorAdaptersFactoryService>();
            IVsTextView viewAdapter = await GetCurrentNativeTextViewAsync();

            return editorAdapter.GetWpfTextView(viewAdapter);
        }

        public async Task<IVsTextView> GetCurrentNativeTextViewAsync()
        {
            IVsTextManager textManager = await VS.GetServiceAsync<SVsTextManager, IVsTextManager>();
            ErrorHandler.ThrowOnFailure(textManager.GetActiveView(1, null, out IVsTextView activeView));

            return activeView;
        }
    }
}
