using System;
using System.Runtime.InteropServices;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace TestExtension
{
    [Guid("d3b3ebd9-87d1-41cd-bf84-268d88953417")]
    public class RunnerWindow : ToolWindowPane
    {
        public const string Title = "Runner Window";

        public RunnerWindow() : base(null)
        { }

        public RunnerWindow(DTE2 dte) : base()
        {
            Caption = Title;

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new RunnerWindowControl(dte);
        }
    }
}
