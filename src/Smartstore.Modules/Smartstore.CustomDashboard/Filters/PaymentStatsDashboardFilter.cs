using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Smartstore.Core.Widgets;
using Smartstore.CustomDashboard.Components;

namespace Smartstore.CustomDashboard
{
    public class PaymentStatsDashboardFilter : IResultFilter
    {
        private readonly Lazy<IWidgetProvider> _widgetProvider;

        public PaymentStatsDashboardFilter(Lazy<IWidgetProvider> widgetProvider)
        {
            _widgetProvider = widgetProvider;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            
            if (context.Result.IsHtmlViewResult())
            {
                _widgetProvider.Value.RegisterViewComponent<CustomDashboard.Components.PaymentStatsDashboardViewComponent>(
                    "admin_dashboard_bottom");

                _widgetProvider.Value.RegisterViewComponent<CustomDashboard.Components.NewContactDashboardViewComponent>(
                    "admin_dashboard_top");

            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
