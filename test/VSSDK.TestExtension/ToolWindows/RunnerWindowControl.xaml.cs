using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Helpers;
using Microsoft.VisualStudio.Shell;

namespace TestExtension
{
    public partial class RunnerWindowControl : UserControl
    {
        public RunnerWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await VS.Notifications.Statusbar.SetTextAsync("Test");
                var text = await VS.Notifications.Statusbar.GetTextAsync();
                await VS.Notifications.Statusbar.SetTextAsync(text + " OK");

                var ex = new Exception(nameof(TestExtension));
                await ex.LogAsync();

                VSConstants.MessageBoxResult button = VS.Notifications.ShowMessage("message", "title");
                Debug.WriteLine(button);
            });
        }
    }
}