//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Smartstore.Admin.Components;
//using Smartstore.Core.Configuration;
//using Smartstore.Core.Data;
//using Smartstore.Core.Security;

//namespace Smartstore.Stats.Payment.Components
//{
//    public class NewContactDashboardViewComponent : DashboardViewComponentBase
//    {
//        private readonly SmartDbContext _db;
//        private readonly ISettingService _settingsService;


//        public NewContactDashboardViewComponent(
//            SmartDbContext db,
//            ISettingService settingService)
//        {
//            _db = db;
//            _settingService = settingService;
//        }

//        public override async Task<IViewComponentResult> InvokeAsync()
//        {
//            var enabled = _setting;

//            if (!enabled)
//                return Empty();
//            if (!await Services.Permissions.AuthorizeAsync(Permissions.Order.Read))
//                return Empty();
//        }
//    }
//}
