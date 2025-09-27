using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Smartstore.Core.Widgets;
using Smartstore.CustomDashboard.Components;
using Smartstore.Stats.Payment.Components;

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
                _widgetProvider.Value.RegisterViewComponent<Smartstore.Stats.Payment.Components.PaymentStatsDashboardViewComponent>(
                    "admin_dashboard_bottom");

                _widgetProvider.Value.RegisterViewComponent<Smartstore.Stats.Payment.Components.NewContactDashboardViewComponent>(
                    "admin_dashboard_top");

            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
