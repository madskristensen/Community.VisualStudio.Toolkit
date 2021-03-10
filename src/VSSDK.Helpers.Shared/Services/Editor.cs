using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Threading.Tasks;

namespace VS
{
    public static class Editor
    {
        public static async Task<IWpfTextView?> GetCurrentWpfTextViewAsync()
        {
            IComponentModel? compService = await Helpers.GetServiceAsync<SComponentModel, IComponentModel>();
            Assumes.Present(compService);

            IVsEditorAdaptersFactoryService? editorAdapter = compService.GetService<IVsEditorAdaptersFactoryService>();
            Assumes.Present(editorAdapter);

            IVsTextView? viewAdapter = await GetCurrentNativeTextViewAsync();
            Assumes.Present(viewAdapter);

            return editorAdapter.GetWpfTextView(viewAdapter);
        }

        public static async Task<IVsTextView?> GetCurrentNativeTextViewAsync()
        {
            IVsTextManager textManager = await Helpers.GetServiceAsync<SVsTextManager, IVsTextManager>();
            Assumes.Present(textManager);

            ErrorHandler.ThrowOnFailure(textManager.GetActiveView(1, null, out IVsTextView activeView));

            return activeView;
        }
    }
}
