using Microsoft.AspNetCore.Mvc;

namespace Smartstore.CustomDashboard.Components
{
    public class DashboardSwitcherViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
           
            return View("~/Modules/Smartstore.CustomDashboard/Views/Shared/Components/DashboardWidgetSwitcher/Default.cshtml");
        }
    }
}
