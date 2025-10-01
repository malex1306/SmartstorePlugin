using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartstore.Web.Controllers;

namespace Smartstore.CustomDashboard.Controllers
{
    public class SwitchAdminController : AdminController
    {
        [Authorize]
        [HttpPost]
        public IActionResult GetWidgetHtml(string widgetName)
        {
            if (string.IsNullOrEmpty(widgetName))
            {
                return BadRequest("widgetName fehlt");
            }

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
    }
}
