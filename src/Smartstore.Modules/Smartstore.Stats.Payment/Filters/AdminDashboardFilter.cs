using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Smartstore.Core.Widgets;
using Smartstore.Stats.Payment.Components;

namespace Smartstore.Stats.Filters
{
    public class AdminDashboardFilter : IResultFilter
    {
        private readonly Lazy<IWidgetProvider> _widgetProvider;

        public AdminDashboardFilter(Lazy<IWidgetProvider> widgetProvider)
        {
            _widgetProvider = widgetProvider;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result.IsHtmlViewResult())
            {
                _widgetProvider.Value.RegisterViewComponent<PaymentStatsDashboardViewComponent>(
                    ["admin_dashboard_bottom"]);
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
