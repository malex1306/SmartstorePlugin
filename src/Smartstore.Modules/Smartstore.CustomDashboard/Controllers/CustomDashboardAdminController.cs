
using Microsoft.AspNetCore.Mvc;
using Smartstore.CustomDashboard.Models;
using Smartstore.Core.Configuration;
using Smartstore.Web.Controllers;
using Smartstore.Core.Security;
using System.Threading.Tasks;

namespace Smartstore.CustomDashboard.Controllers
{
    [Area("Admin")]
    public class CustomDashboardAdminController : AdminController
    {
        private readonly ISettingService _settingService;

        public CustomDashboardAdminController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [Permission(Permissions.Configuration.Setting.Read)]
        public IActionResult Configure()
        {
            var model = new ConfigurationModel
            {
                IncompleteOrdersPosition = _settingService.GetSettingByKey<string>("IncompleteOrdersPosition", "grid-column: 1/13; grid-row: 1/2;"),
                PaymentsPosition = _settingService.GetSettingByKey<string>("PaymentsPosition", "grid-column: 1/13; grid-row: 2/3;"),
                LastContactsPosition = _settingService.GetSettingByKey<string>("LastContactsPosition", ""),
                NewsFeedPosition = _settingService.GetSettingByKey<string>("NewsFeedPosition", "grid-column: 1/13; grid-row: 8/9;"),
                BestsellersPosition = _settingService.GetSettingByKey<string>("BestsellersPosition", "grid-column: 1/13; grid-row: 3/4;"),
                TopCustomersPosition = _settingService.GetSettingByKey<string>("TopCustomersPosition", "grid-column: 1/13; grid-row: 4/5;"),
                CustomerRegistrationsPosition = _settingService.GetSettingByKey<string>("CustomerRegistrationsPosition", "grid-column: 1/13; grid-row: 5/6;"),
                LatestOrdersPosition = _settingService.GetSettingByKey<string>("LatestOrdersPosition", "grid-column: 1/13; grid-row: 6/7;"),
                StoreStatisticsPosition = _settingService.GetSettingByKey<string>("StoreStatisticsPosition", "grid-column: 1/13; grid-row: 7/8;")
            };

            return View(model);
        }

        [HttpPost, Permission(Permissions.Configuration.Setting.Update)]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            await _settingService.ApplySettingAsync("CustomDashboard.Enabled", model.Enabled);
            await _settingService.ApplySettingAsync("IncompleteOrdersPosition", model.IncompleteOrdersPosition);
            await _settingService.ApplySettingAsync("PaymentsPosition", model.PaymentsPosition);
            await _settingService.ApplySettingAsync("LastContactsPosition", model.LastContactsPosition);
            await _settingService.ApplySettingAsync("NewsFeedPosition", model.NewsFeedPosition);
            await _settingService.ApplySettingAsync("BestsellersPosition", model.BestsellersPosition);
            await _settingService.ApplySettingAsync("TopCustomersPosition", model.TopCustomersPosition);
            await _settingService.ApplySettingAsync("CustomerRegistrationsPosition", model.CustomerRegistrationsPosition);
            await _settingService.ApplySettingAsync("LatestOrdersPosition", model.LatestOrdersPosition);
            await _settingService.ApplySettingAsync("StoreStatisticsPosition", model.StoreStatisticsPosition);

            NotifySuccess(T("Admin.Common.DataSuccessfullySaved"));

            return RedirectToAction(nameof(Configure));
        }
    }
}
