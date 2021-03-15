using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace TestExtension
{
    [Guid("37b4bf68-1163-413d-819b-74a5b2267b05")]
    public class RunnerWindow : ToolWindowPane
    {
        public RunnerWindow() : base(null)
        {
            Caption = "RunnerWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new RunnerWindowControl();
        }
    }
}
