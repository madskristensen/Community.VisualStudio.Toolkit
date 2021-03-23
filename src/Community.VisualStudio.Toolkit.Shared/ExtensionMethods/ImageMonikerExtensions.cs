using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Community.VisualStudio.Toolkit;

namespace Microsoft.VisualStudio.Imaging.Interop
{
    /// <summary>Extension methods for the ImageMoniker class.</summary>
    public static class ImageMonikerExtensions
    {
        /// <summary>Converts an ImageMoniker to a bitmap in the specified size.</summary>
        public static async Task<BitmapSource?> ToBitmapSourceAsync(this ImageMoniker moniker, int size)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IVsUIObject result = await ToUiObjectAsync(moniker, size);
            ErrorHandler.ThrowOnFailure(result.get_Data(out var data));

            return data as BitmapSource;
        }

        /// <summary>Converts an ImageMoniker to an IVsUIObject in the specified size.</summary>
        public static async Task<IVsUIObject> ToUiObjectAsync(this ImageMoniker moniker, int size)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IVsImageService2? imageService = await VS.Shell.GetImageServiceAsync();
            Color backColor = VSColorTheme.GetThemedColor(EnvironmentColors.ToolWindowBackgroundColorKey);

            var imageAttributes = new ImageAttributes
            {
                Flags = (uint)_ImageAttributesFlags.IAF_RequiredFlags | unchecked((uint)_ImageAttributesFlags.IAF_Background),
                ImageType = (uint)_UIImageType.IT_Bitmap,
                Format = (uint)_UIDataFormat.DF_WPF,
                LogicalHeight = size,
                LogicalWidth = size,
                Background = (uint)backColor.ToArgb(),
                StructSize = Marshal.SizeOf(typeof(ImageAttributes))
            };

            return imageService.GetImage(moniker, imageAttributes);
        }
    }
}
