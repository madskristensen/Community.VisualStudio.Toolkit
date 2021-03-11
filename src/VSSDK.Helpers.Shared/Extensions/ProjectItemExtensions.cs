using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace EnvDTE
{
    public static class ProjectItemExtensions
    {
        /// <summary>
        /// Opens a file in the Preview Tab (provisional tab) if supported by the editor factory.
        /// </summary>
        public static void PreviewDocument(this ProjectItem item)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            PreviewDocument(item.FileNames[1]);
        }

        /// <summary>
        /// Opens a file in the Preview Tab (provisional tab) if supported by the editor factory.
        /// </summary>
        public static void PreviewDocument(string file)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (new NewDocumentStateScope(__VSNEWDOCUMENTSTATE2.NDS_TryProvisional, VSConstants.NewDocumentStateReason.Navigation))
            {
                VsShellUtilities.OpenDocument(ServiceProvider.GlobalProvider, file);
            }
        }

        public static void SetItemType(this ProjectItem item, string itemType)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                if (item == null || item.ContainingProject == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(itemType) || item.ContainingProject.IsKind(ProjectTypes.WEBSITE_PROJECT, ProjectTypes.UNIVERSAL_APP))
                {
                    return;
                }

                item.Properties.Item("ItemType").Value = itemType;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
