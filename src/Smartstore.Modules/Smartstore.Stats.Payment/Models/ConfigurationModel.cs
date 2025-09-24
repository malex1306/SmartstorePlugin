using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartstore.Web.Modelling;

namespace Smartstore.Stats.Payment.Models
{
    [LocalizedDisplay("Plugins.Smartstore.Stats.Payment")]
    public class ConfigurationModel : ModelBase
    {
        [LocalizedDisplay("*Enabled")]
        public bool Enabled { get; set; }
    }
}
