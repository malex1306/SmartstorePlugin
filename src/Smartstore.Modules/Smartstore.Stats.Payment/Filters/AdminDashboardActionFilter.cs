using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Smartstore.Core.Logging;
using Smartstore.Core.Web;

namespace Smartstore.CustomDashboard.Filters
{
    internal class AdminDashboardActionFilter : IAsyncActionFilter
    {
        private readonly IViewDataAccessor _viewDataAccessor;

        public AdminDashboardActionFilter(IViewDataAccessor viewDataAccessor)
        {
            _viewDataAccessor = viewDataAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var viewData = _viewDataAccessor.ViewData;

            if (viewData == null)
            {
                await next();
                return;
            }

            //var myViewData = new ViewDataDictionary(
            //    new EmptyModelMetadataProvider(), 
            //    new ModelStateDictionary()) { 
            //    { "PublicInfoModel", publicInfoModel } 
            //};

            //myViewData.Model = publicInfoModel;

            context.Result = new PartialViewResult
            {
                //ViewData = myViewData,
                ViewName = "~/Modules/Smartstore.Stats.Payment/Views/Shared/HelloWorld.cshtml"
            };

            //if (filterContext.Result is ViewResult)
            //{
            //    var controllerName = filterContext.RouteData.Values.GetControllerName();
            //    var actionName = filterContext.RouteData.Values.GetActionName();

            //    ViewResult viewResult = (ViewResult)filterContext.Result;

            //    filterContext.Result = new ViewResult
            //    {
            //        ViewName = "",
            //        ViewData = viewResult.ViewData,
            //        TempData = viewResult.TempData
            //    };

            //}
        }
    }
}