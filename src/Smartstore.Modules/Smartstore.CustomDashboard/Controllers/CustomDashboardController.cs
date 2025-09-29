using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smartstore.CustomDashboard.Components;
using Smartstore.CustomDashboard.Settings;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Smartstore.CustomDashboard.Controllers
{
    
    public class CustomDashboardController : Controller
    {
        private readonly NewContactDashboardViewComponent _newContacts;
        private readonly PaymentStatsDashboardViewComponent _paymentStats;
        private readonly IViewComponentHelper _viewComponentHelper;

        public CustomDashboardController(
            NewContactDashboardViewComponent newContacts,
            PaymentStatsDashboardViewComponent paymentStats,
            IViewComponentHelper viewComponentHelper)
        {
            _newContacts = newContacts;
            _paymentStats = paymentStats;
            _viewComponentHelper = viewComponentHelper;
        }

        [HttpGet]
        public async Task<IActionResult> LoadWidget(string widgetName)
        {
            switch (widgetName)
            {
                case "NewContacts":
                    return ViewComponentToContent("NewContactDashboard");

                case "PaymentStats":
                    return ViewComponentToContent("PaymentStatsDashboard");

                default:
                    return Content("<div>Widget nicht gefunden</div>");
            }
        }

        private IActionResult ViewComponentToContent(string componentName)
        {
            var result = ViewComponent(componentName);
            return result;
        }
    }
}
