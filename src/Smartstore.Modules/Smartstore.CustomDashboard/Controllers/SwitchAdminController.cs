using System;
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
        public IActionResult GetWidgetHtml(string widgetName)
        {
            if (string.IsNullOrEmpty(widgetName))
            {
                return BadRequest("widgetName fehlt");
            }

            
            _settingService.ApplySettingAsync("Dashboard.LastSelectedWidget", widgetName);

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
        public IActionResult SaveLastWidget(string widgetName)
        {
            if (!string.IsNullOrEmpty(widgetName))
            {
                _settingService.ApplySettingAsync("Dashboard.LastSelectedWidget", widgetName);
            }
            return Ok();
        }
    }
}
