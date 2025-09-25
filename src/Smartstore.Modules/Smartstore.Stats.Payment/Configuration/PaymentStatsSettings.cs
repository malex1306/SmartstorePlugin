using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartstore.Core.Configuration;

namespace Smartstore.Stats.Payment.Settings
{
        public class PaymentStatsSettings : ISettings
        {
            public bool Enabled { get; set; } = true;
        }
}
