using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartstore.Core.Configuration;

namespace Smartstore.CustomDashboard.Settings
{
        public class PaymentStatsSettings : ISettings
        {
            public bool Enabled { get; set; } = true;
            public bool EnabledEmail { get; set; } = true;
            public string IncompleteOrdersPosition { get; set; }
            public string PaymentsPosition { get; set; }
            public string LastContactsPosition { get; set; }
            public string NewsFeedPosition { get; set; }
    }
}
