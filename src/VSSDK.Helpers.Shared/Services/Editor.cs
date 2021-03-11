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

        public class ContentTypes
        {
            public const string Any = "any";
            public const string Text = "text";
            public const string Code = "code";


            public const string CSharp = "CSharp";
            public const string VisualBasic = "Basic";
            public const string FSharp = "F#";
            public const string CPlusPlus = "C/C++";
            public const string Css = "CSS";
            public const string Less = "LESS";
            public const string Scss = "SCSS";
            public const string HTML = "HTMLX";
            public const string WebForms = "HTML";
            public const string Json = "JSON";
            public const string Xaml = "XAML";
            public const string Xml = "XML";
        }
    }
}
