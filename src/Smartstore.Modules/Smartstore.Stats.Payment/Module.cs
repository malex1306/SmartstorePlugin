using System.Threading.Tasks;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace Smartstore.Paymentstats
{
    public class Module : ModuleBase, IConfigurable
    {
        public RouteInfo GetConfigurationRoute()
            => new RouteInfo("Configure", "PaymentStats", new { area = "Admin" });

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
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
