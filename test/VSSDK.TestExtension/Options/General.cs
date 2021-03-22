using System.ComponentModel;
using VisualStudio.SDK.Toolkit;

namespace TestExtension
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes in your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "My options", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "My options", "General", 0, 0, true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("My category")]
        [DisplayName("My Options")]
        [Description("An informative description.")]
        [DefaultValue(true)]
        public bool MyOption { get; set; } = true;
    }
}
