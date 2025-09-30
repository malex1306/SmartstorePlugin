using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smartstore.Web.Controllers;

namespace Smartstore.CustomDashboard.Controllers
{
    public class SwitchAdminController : AdminController
    {
        private readonly IViewComponentHelper _viewComponentHelper;

        public SwitchAdminController(IViewComponentHelper viewComponentHelper)
        {
                       _viewComponentHelper = viewComponentHelper;
        }

        [Authorize]
        public async Task<IActionResult> GetWidgetHtml(string widgetName)
        {
            string widgetHtml = null;

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
                return Json(new { error = "Widget not found" });
            }

            try
            {
                using (var writer = new StringWriter())
                {
                    var result = await _viewComponentHelper.InvokeAsync(componentName);
                    result.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                    widgetHtml = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Error rendering widget: {ex.Message}" });
            }

            return Json(new { widgetHtml = widgetHtml });
        }
    }
}
