using Smartstore.Web.Modelling;
using Smartstore.Core.Localization;

namespace Smartstore.CustomDashboard.Models
{
    
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("Plugins.CustomDashboard.Enabled")]
        public bool Enabled { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.IncomplteOrdersPosition")]
        public string IncompleteOrdersPosition{ get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.PaymentsPosition")]
        public string PaymentsPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.LastContactsPosition")]
        public string LastContactsPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.NewsFeedPosition")]
        public string NewsFeedPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.BestsellersPosition")]
        public string BestsellersPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.TopCustomersPosition")]
        public string TopCustomersPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.CustomerRegistrationsPosition")]
        public string CustomerRegistrationsPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.LatestOrdersPosition")]
        public string LatestOrdersPosition { get; set; }

        [LocalizedDisplay("Plugins.CustomDashboard.StoreStatisticsPosition")]
        public string StoreStatisticsPosition { get; set; }
    }
}
