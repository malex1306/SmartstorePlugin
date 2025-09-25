using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Smartstore.Admin.Controllers;
using Smartstore.Engine;
using Smartstore.Engine.Builders;
using Smartstore.Stats.Filters;
using Smartstore.Stats.Payment.Controllers.Admin;

namespace Smartstore.Stats.Payment
{
    internal class Startup : StarterBase
    {
        public override void ConfigureServices(IServiceCollection services, IApplicationContext appContext)
        {
            services.Configure<MvcOptions>(o =>
            {
                o.Filters.AddEndpointFilter<AdminDashboardFilter, HomeController>()
                .ForController("PaymentStatsAdmin")
                .ForAction(nameof(PaymentStatsAdminController));
            });
        }
    }
}
