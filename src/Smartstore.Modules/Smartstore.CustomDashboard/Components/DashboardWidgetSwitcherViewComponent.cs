using Microsoft.AspNetCore.Mvc;

namespace Smartstore.CustomDashboard.Components
{
    public class DashboardWidgetSwitcherViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
