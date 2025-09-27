using Smartstore.Web.Modelling;

namespace Smartstore.CustomDashboard.Models
{
    [LocalizedDisplay("Plugins.CustomDashboard.")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Enabled")]
        public bool Enabled { get; set; }

        [LocalizedDisplay("*EnabledEmail")]
        public bool EnabledEmail { get; set; }
    }
}
