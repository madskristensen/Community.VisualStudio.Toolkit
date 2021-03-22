using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using VisualStudio.SDK.Toolkit;

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
                await VS.Notifications.SetStatusbarTextAsync("Test");
                var text = await VS.Notifications.GetStatusbarTextAsync();
                await VS.Notifications.SetStatusbarTextAsync(text + " OK");

                var ex = new Exception(nameof(TestExtension));
                await ex.LogAsync();

                VSConstants.MessageBoxResult button = VS.Notifications.ShowMessage("message", "title");
                Debug.WriteLine(button);
            });
        }
    }
}