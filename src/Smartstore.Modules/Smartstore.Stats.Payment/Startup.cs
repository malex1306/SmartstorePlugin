using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Smartstore.Engine;
using Smartstore.Engine.Builders;
using Smartstore.Stats.Filters;
using Smartstore.Admin.Controllers;

namespace Smartstore.Stats.Payment
{
    internal class Startup : StarterBase
    {
        public override void ConfigureServices(IServiceCollection services, IApplicationContext appContext)
        {
            services.Configure<MvcOptions>(o =>
            {
                o.Filters.AddEndpointFilter<AdminDashboardFilter, HomeController>()
                    .ForAction(x => x.Index()) 
                    .WhenNonAjax();          
            });
        }
    }
}
