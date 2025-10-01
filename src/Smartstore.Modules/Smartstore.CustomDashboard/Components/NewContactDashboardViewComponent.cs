using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Smartstore;
using Smartstore.Admin.Components;
using Smartstore.Core.Data;
using Smartstore.Core.Messaging;
using Smartstore.Core.Security;
using Smartstore.CustomDashboard.Settings;

namespace Smartstore.CustomDashboard.Components
{
    public class NewContactDashboardViewComponent : DashboardViewComponentBase
    {
        private readonly SmartDbContext _db;
        private readonly PaymentStatsSettings _settings;

        public NewContactDashboardViewComponent(
            SmartDbContext db,
            PaymentStatsSettings settings)
        {
            _db = db;
            _settings = settings;
        }

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_settings.EnabledEmail)
                return Empty();

            //if (!await Services.Permissions.AuthorizeAsync(Permissions.Order.Read))
            //    return Empty();

            var contacts = await _db.QueuedEmails
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.Subject)
                && x.Subject.Contains("Kontaktanfrage"))
                .OrderByDescending(x => x.CreatedOnUtc)
                .Take(5)
                .Select(x => new ContactEmail
                {
                    Email = x.ReplyTo,
                    Subject = x.Subject,
                    CreatedOn = x.CreatedOnUtc
                })
                .ToListAsync();

            return View("~/Modules/Smartstore.CustomDashboard/Views/Shared/Components/NewContactDashboard/Default.cshtml", contacts);
        }
    }




    public class ContactEmail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
