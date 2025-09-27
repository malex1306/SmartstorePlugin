using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartstore.Admin.Components;
using Smartstore.Core.Checkout.Payment;
using Smartstore.Core.Data;
using Smartstore.Core.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smartstore.Core.Security;
using Smartstore.CustomDashboard.Settings;

namespace Smartstore.CustomDashboard.Components
{
    public class PaymentStatsDashboardViewComponent : DashboardViewComponentBase
    {
        private readonly SmartDbContext _db;
        private readonly IPaymentService _paymentService;
        private readonly ISettingService _settingService;
        private readonly PaymentStatsSettings _settings;

        private static readonly Dictionary<string, string> PaymentFriendlyNamesFallback = new()
        {
            ["Payments.PayInStore"] = "Barzahlung im Laden",
            ["Payments.Invoice"] = "Rechnung",
            ["Payments.Prepayment"] = "Vorauszahlung",
            ["Payments.CashOnDelivery"] = "Nachnahme"
        };

        public PaymentStatsDashboardViewComponent(
            SmartDbContext db,
            IPaymentService paymentService,
            ISettingService settingService,
            PaymentStatsSettings settings)
        {
            _db = db;
            _paymentService = paymentService;
            _settingService = settingService;
            _settings = settings;
        }

        public override async Task<IViewComponentResult> InvokeAsync()
        {

            var enabled = _settings.Enabled;


            if (!enabled)
                return Empty();

            if (!await Services.Permissions.AuthorizeAsync(Permissions.Order.Read))
                return Empty();

            var allProviders = await _paymentService.LoadAllPaymentProvidersAsync(onlyEnabled: false);
            var stats = await _db.Orders
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.PaymentMethodSystemName))
                .GroupBy(o => o.PaymentMethodSystemName)
                .Take(10)
                .Select(g => new PaymentMethodStat
                {
                    MethodSystemName = g.Key,
                    Count = g.Count(),
                    Total = g.Sum(x => x.OrderTotal)
                })
                .ToListAsync();

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
