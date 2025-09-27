using Microsoft.AspNetCore.Mvc;
using Smartstore.ComponentModel;
using Smartstore.Core.Security;
using Smartstore.CustomDashboard.Settings;
using Smartstore.Web.Controllers;
using Smartstore.Web.Modelling.Settings;
using Smartstore.CustomDashboard.Models;

namespace Smartstore.CustomDashboard.Controllers.Admin
{
    public class PaymentStatsAdminController : AdminController
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

            TempData["Success"] = T("Admin.Common.ChangesSaved");

            return RedirectToAction(nameof(Configure));
        }
    }
}
