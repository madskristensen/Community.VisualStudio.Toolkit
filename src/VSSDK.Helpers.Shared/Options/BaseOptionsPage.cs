using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudio.Helpers.Options
{
    /// <summary>
    /// A base class for a DialogPage to show in Tools -> Options.
    /// </summary>
    public class BasePage<T> : DialogPage where T : BaseModel<T>, new()
    {
        private readonly BaseModel<T> _model;

        public BasePage()
        {
#pragma warning disable VSTHRD104 // Offer async methods
            _model = ThreadHelper.JoinableTaskFactory.Run(BaseModel<T>.CreateAsync);
#pragma warning restore VSTHRD104 // Offer async methods
        }

        public override object AutomationObject => _model;

        public override void LoadSettingsFromStorage()
        {
            _model.Load();
        }

        public override void SaveSettingsToStorage()
        {
            _model.Save();
        }
    }
}