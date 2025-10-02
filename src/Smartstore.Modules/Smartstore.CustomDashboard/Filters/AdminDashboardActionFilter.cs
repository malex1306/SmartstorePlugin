using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Smartstore.Core.Web;
using Smartstore.CustomDashboard.Models;

namespace Smartstore.CustomDashboard.Filters
{
    internal class AdminDashboardActionFilter : IAsyncActionFilter
    {
        private readonly IViewDataAccessor _viewDataAccessor;
        private readonly IWebHelper _webHelper;

        public AdminDashboardActionFilter(IViewDataAccessor viewDataAccessor, IWebHelper webHelper)
        {
            _viewDataAccessor = viewDataAccessor;
            _webHelper = webHelper;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jsonPath = _webHelper.MapPath("~/Modules/Smartstore.CustomDashboard/wwwroot/adminwidgets.json");

            PublicInfoModel publicInfoModel = null;

            if (File.Exists(jsonPath))
            {
                var json = await File.ReadAllTextAsync(jsonPath);
                publicInfoModel = JsonSerializer.Deserialize<PublicInfoModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            var viewData = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = publicInfoModel
            };

            context.Result = new PartialViewResult
            {
                ViewData = viewData,
                ViewName = "~/Modules/Smartstore.CustomDashboard/Views/Shared/_CustomDashboard.cshtml"
            };
        }
    }
    
}
