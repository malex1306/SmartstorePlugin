using Smartstore.Web.Modelling;

namespace Smartstore.Stats.Payment.Models
{
    [LocalizedDisplay("Plugins.Stats.Payment.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Enabled")]
        public bool Enabled { get; set; }

        [LocalizedDisplay("*EnabledEmail")]
        public bool EnabledEmail { get; set; }
    }
}
