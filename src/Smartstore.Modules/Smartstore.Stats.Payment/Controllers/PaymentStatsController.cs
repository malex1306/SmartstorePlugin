using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smartstore.ComponentModel;
using Smartstore.Core.Security;
using Smartstore.Stats.Payment.Settings;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;
using Smartstore.Stats.Payment.Models;

namespace Smartstore.Stats.Payment.Controllers
{
    public class PaymentStatsController : AdminController
    {
        [LoadSetting, AuthorizeAdmin]
        public IActionResult Configure(PaymentStatsSettings settings)
        {
            var model = MiniMapper.Map<PaymentStatsSettings, ConfigurationModel>(settings);
            
              return View(model);
        }

        [HttpPost, SaveSetting, AuthorizeAdmin]
        public IActionResult Configure(ConfigurationModel model, PaymentStatsSettings settings)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }

            ModelState.Clear();
            MiniMapper.Map(model, settings);

            return RedirectToAction(nameof(Configure));
        }

    }
}
