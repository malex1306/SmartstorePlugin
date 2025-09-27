using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Smartstore.Admin.Controllers;
using Smartstore.CustomDashboard;
using Smartstore.CustomDashboard.Filters;
using Smartstore.Engine;
using Smartstore.Engine.Builders;

namespace Smartstore.CustomDashboard
{
    internal class Startup : StarterBase
    {
        public override void ConfigureServices(IServiceCollection services, IApplicationContext appContext)
        {
            services.Configure<MvcOptions>(o =>
            {
                o.Filters.AddEndpointFilter<PaymentStatsDashboardFilter, HomeController>()
                    .ForAction(x => x.Index())
                    .WhenNonAjax();
                o.Filters.AddEndpointFilter<AdminDashboardActionFilter, HomeController>()
                    .ForAction(x => x.Index())
                    .WhenNonAjax();
            });
        }
    }
}
