using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smartstore.Admin.Components;
using Smartstore.Core.Checkout.Orders;
using Smartstore.Core.Checkout.Payment;
using Smartstore.Core.Security;
using Smartstore.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartstore.Admin.Components
{
    public class DashboardPaymentMethodsViewComponent : DashboardViewComponentBase
    {
        private readonly SmartDbContext _db;
        private readonly IPaymentService _paymentService;

        
        private static readonly Dictionary<string, string> PaymentFriendlyNamesFallback = new()
        {
            ["Payments.PayInStore"] = "Barzahlung im Laden",
            ["Payments.Invoice"] = "Rechnung",
            ["Payments.Prepayment"] = "Vorrauszahlung",
            ["Payments.CashOnDelivery"] = "Nachname",
        };

        public DashboardPaymentMethodsViewComponent(
            SmartDbContext db,
            IPaymentService paymentService)
        {
            _db = db;
            _paymentService = paymentService;
        }

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            if (!await Services.Permissions.AuthorizeAsync(Permissions.Order.Read))
            {
                return Empty();
            }

            // Load all providers
            var allProviders = await _paymentService.LoadAllPaymentProvidersAsync(onlyEnabled: false);

            
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

            foreach (var stat in stats)
            {
                // check it gives a provider
                var provider = allProviders.FirstOrDefault(p => p.Metadata.SystemName == stat.MethodSystemName);

                
                stat.MethodFriendlyName = provider?.Metadata?.FriendlyName
                    ?? (PaymentFriendlyNamesFallback.ContainsKey(stat.MethodSystemName)
                        ? PaymentFriendlyNamesFallback[stat.MethodSystemName]
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
