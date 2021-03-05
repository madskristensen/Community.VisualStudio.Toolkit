using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace EnvDTE
{
    public static class ProjectItemExtensions
    {
        public static void PreviewDocument(this ProjectItem item)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (new NewDocumentStateScope(__VSNEWDOCUMENTSTATE2.NDS_TryProvisional, VSConstants.NewDocumentStateReason.Navigation))
            {
                VsShellUtilities.OpenDocument(ServiceProvider.GlobalProvider, item.FileNames[1]);
            }
        }
    }
}
