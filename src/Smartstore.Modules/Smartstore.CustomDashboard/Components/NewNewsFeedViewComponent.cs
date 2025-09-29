using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smartstore.Core.Data;
using Smartstore.Core.Web;
using Smartstore.CustomDashboard.Settings;
using Smartstore.Web.Components;
using Smartstore.CustomDashboard.Models;

namespace Smartstore.CustomDashboard.Components
{
    public class NewNewsFeedViewComponent : SmartViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHelper _webHelper;
        private readonly SmartDbContext _db;
        private readonly PaymentStatsSettings _settings;

        public NewNewsFeedViewComponent(
            IHttpClientFactory httpClientFactory,
            IWebHelper webHelper,
            SmartDbContext db,
            PaymentStatsSettings settings)
        {
            _httpClientFactory = httpClientFactory;
            _webHelper = webHelper;
            _db = db;
            _settings = settings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_settings.EnabledNewsFeed)
                return Empty();

            var lang = Services.WorkContext.WorkingLanguage;
            var result = await Services.Cache.GetAsync($"admin:newsfeed{lang.UniqueSeoCode}", async ctx =>
            {
                ctx.ExpiresIn(TimeSpan.FromHours(4));

                try
                {
                    var client = _httpClientFactory.CreateClient();
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("lang", lang.UniqueSeoCode),
                        new KeyValuePair<string,string>("ip", _webHelper.GetClientIpAddress().ToString()),
                        new KeyValuePair<string,string>("modules", string.Join(',', Services.ApplicationContext.ModuleCatalog.Modules.Select(x => x.Name))),
                        new KeyValuePair<string,string>("id", Services.ApplicationContext.RuntimeInfo.ApplicationIdentifier),
                        new KeyValuePair<string,string>("auth", Services.StoreContext.CurrentStore.GetBaseUrl().TrimEnd('/'))
                    });

                    var response = await client.PostAsync("https://smartstore.com/Plugins/NewsFeed/JsonFeed", formContent);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return new FeedModel { IsError = true, ErrorMessage = response.ReasonPhrase };
                    }

                    var channels = await response.Content.ReadFromJsonAsync<List<NewsFeedChannelModel>>();

                    // Bestimme für jedes Item die Ansicht (full/partial/minimized/hidden)
                    foreach (var channel in channels)
                    {
                        var (full, partial, minimized) = GetNewsFeedViewTypes(channel.NewsFeedItems.Count);
                        for (int i = 0; i < channel.NewsFeedItems.Count; i++)
                        {
                            if (i < full) channel.NewsFeedItems[i].ViewType = "full";
                            else if (i < full + partial) channel.NewsFeedItems[i].ViewType = "partial";
                            else if (i < full + partial + minimized) channel.NewsFeedItems[i].ViewType = "minimized";
                            else channel.NewsFeedItems[i].ViewType = "hidden";
                        }
                    }

                    return new FeedModel
                    {
                        NewsFeedCannels = channels
                    };
                }
                catch (Exception ex)
                {
                    return new FeedModel { IsError = true, ErrorMessage = ex.Message };
                }
            });

            if (!result.NewsFeedCannels.Any() && result.IsError)
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

            return View(result);
        }

        private static (int full, int partial, int minimized) GetNewsFeedViewTypes(int totalItems)
        {
            return totalItems switch
            {
                0 => (0, 0, 0),
                <= 4 => (totalItems, 0, 0),
                5 => (3, 2, 0),
                6 => (2, 3, 1),
                7 => (2, 3, 3),
                8 => (2, 2, 4),
                _ => (1, 3, 5)
            };
        }
    }
}
