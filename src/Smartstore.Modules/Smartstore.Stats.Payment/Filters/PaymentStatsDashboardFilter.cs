using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Smartstore.Core.Widgets;
using Smartstore.CustomDashboard.Components;

namespace Smartstore.Stats.Filters
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
            // Nur für Admin/Home/Index
            if (context.Result.IsHtmlViewResult())
            {
                _widgetProvider.Value.RegisterViewComponent<PaymentStatsDashboardViewComponent>(
                    "admin_dashboard_bottom"); // string statt Array
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
