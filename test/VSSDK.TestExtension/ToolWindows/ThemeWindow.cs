using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace TestExtension
{
    [Guid("e3be6dd3-f017-4d6e-ae88-2b29319a77a2")]
    public class ThemeWindow : ToolWindowPane
    {
        public const string Title = "Theme Window";

        public ThemeWindow(ThemeWindowControlViewModel viewModel) : base(null)
        {
            Caption = Title;
            Content = new ThemeWindowControl { DataContext = viewModel };
        }
    }
}
