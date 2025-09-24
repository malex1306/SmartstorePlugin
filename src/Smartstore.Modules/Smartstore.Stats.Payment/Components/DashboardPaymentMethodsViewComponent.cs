using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartstore.Core.Checkout.Payment;
using Smartstore.Core.Configuration;
using Smartstore.Core.Data;
using Smartstore.Core.Security;
using Smartstore.Data;
using Smartstore.Web.Components; 

namespace Smartstore.Stats.Payment.Components
{
    public class DashboardPaymentMethodsViewComponent : SmartViewComponent
    {
        private readonly SmartDbContext _db;
        private readonly IPaymentService _paymentService;
        private readonly ISettingService _settingService;

        
        private static readonly Dictionary<string, string> PaymentFriendlyNamesFallback = new()
        {
            ["Payments.PayInStore"] = "Barzahlung im Laden",
            ["Payments.Invoice"] = "Rechnung",
            ["Payments.Prepayment"] = "Vorauszahlung",
            ["Payments.CashOnDelivery"] = "Nachnahme"
        };

        public DashboardPaymentMethodsViewComponent(
            SmartDbContext db,
            IPaymentService paymentService,
            ISettingService settingService)
        {
            _db = db;
            _paymentService = paymentService;
            _settingService = settingService;
        }

        public override async Task<IViewComponentResult> InvokeAsync(object args)
        {
            // Nur anzeigen, wenn das Plugin aktiviert ist
            var enabled = await _settingService.GetSettingByKeyAsync("Smartstore.Stats.Payment.Enabled", true);
            if (!enabled)
                return Content(string.Empty);

            // Permission prüfen
            if (!await Services.Permissions.AuthorizeAsync(Permissions.Order.Read))
                return Empty();

            // Alle Payment-Provider laden
            var allProviders = await _paymentService.LoadAllPaymentProvidersAsync(onlyEnabled: false);

            // Stats aus DB laden
            var stats = await _db.Orders
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.PaymentMethodSystemName))
                .GroupBy(o => o.PaymentMethodSystemName)
                .Select(g => new PaymentMethodStat
                {
                    MethodSystemName = g.Key,
                    Count = g.Count(),
                    Total = g.Sum(x => x.OrderTotal)
                })
                .ToListAsync();

            // FriendlyName ermitteln
            foreach (var stat in stats)
            {
                var provider = allProviders.FirstOrDefault(p => p.Metadata.SystemName == stat.MethodSystemName);

                stat.MethodFriendlyName = provider?.Metadata?.FriendlyName
                    ?? (PaymentFriendlyNamesFallback.TryGetValue(stat.MethodSystemName, out var friendly)
                        ? friendly
                        : stat.MethodSystemName);
            }

            return View(stats);
        }
    }

    public class PaymentMethodStat
    {
        public string MethodSystemName { get; set; }
        public string MethodFriendlyName { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
    }
}
