using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Smartstore.Core.Security;
using Smartstore.Web.Controllers;

namespace Smartstore.CustomDashboard.Controllers.Admin
{
    public class DashboardAdminController : AdminController
    {
        private readonly IViewComponentHelper _viewComponentHelper;

        public DashboardAdminController(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
        }

        [AuthorizeAdmin]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeAdmin]
        public async Task<IActionResult> GetWidgetHtml(string widgetName)
        {
            string widgetHtml = null;
            int widgetWidth = 6;

            
            using (var writer = new StringWriter())
            {
                var result = await _viewComponentHelper.InvokeAsync(widgetName);
                result.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                widgetHtml = writer.ToString();
            }

            return Json(new { widgetHtml, widgetWidth });
        }
    }
}