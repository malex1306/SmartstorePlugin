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
                    "report-bestsellers");

                _widgetProvider.Value.RegisterViewComponent<CustomDashboard.Components.NewContactDashboardViewComponent>(
                    "report-contacts");

            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
