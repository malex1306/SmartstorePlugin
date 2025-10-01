using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartstore.Core.Configuration;
using Smartstore.Web.Controllers;

namespace Smartstore.CustomDashboard.Controllers
{
    [Authorize]
    public class SwitchAdminController : AdminController
    {
        private readonly ISettingService _settingService;

        public SwitchAdminController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost]
        public async Task<IActionResult> GetWidgetHtml(string widgetName)
        {
            if (string.IsNullOrEmpty(widgetName))
            {
                return BadRequest("widgetName fehlt");
            }

            // Setting speichern und awaiten, um DbContext-Fehler zu vermeiden
            await _settingService.ApplySettingAsync("Dashboard.LastSelectedWidget", widgetName);

            string componentName = widgetName switch
            {
                "Payments" => "PaymentStatsDashboard",
                "LastContacts" => "NewContactDashboard",
                "Bestsellers" => "BestsellerDashboard",
                "TopCustomers" => "TopCustomerDashboard",
                "CustomerRegistrations" => "CustomerRegistrationDashboard",
                "LatestOrders" => "LatestOrderDashboard",
                "StoreStatistics" => "StoreStatisticsDashboard",
                _ => null
            };

            if (componentName == null)
            {
                return Content("<div class='alert alert-danger'>Widget not found</div>", "text/html");
            }

            try
            {
                return ViewComponent(componentName);
            }
            catch (Exception ex)
            {
                return Content($"<div class='alert alert-danger'>Rendering fail {ex.Message}</div>", "text/html");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLastWidget(string widgetName)
        {
            if (!string.IsNullOrEmpty(widgetName))
            {
                await _settingService.ApplySettingAsync("Dashboard.LastSelectedWidget", widgetName);
            }
            return Ok();
        }
    }
}
