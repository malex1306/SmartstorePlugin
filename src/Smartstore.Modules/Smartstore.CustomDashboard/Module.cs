using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Smartstore.Core.Widgets;
using Smartstore.Engine;
using Smartstore.Engine.Modularity;
using Smartstore.Http;
using Smartstore.CustomDashboard.Components;

namespace Smartstore.CustomDashboard
{
    internal class Module : ModuleBase, IConfigurable
    {
        public RouteInfo GetConfigurationRoute()
            => new("Configure", "PaymentStatsAdmin", new { area = "Admin" });

        //public Widget GetDisplayWidget(string widgetZone, object model, int storeId)
        //    => new ComponentWidget(typeof(PaymentStatsDashboardViewComponent), model);

        //public string[] GetWidgetZones() => ["admin_dashboard_bottom"];

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            await TrySaveSettingsAsync<Settings.PaymentStatsSettings>();
            await ImportLanguageResourcesAsync();
            await base.InstallAsync(context);
        }

        public override async Task UninstallAsync()
        {
            await DeleteLanguageResourcesAsync();
            await base.UninstallAsync();
        }
    }
}
