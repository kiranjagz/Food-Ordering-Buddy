using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using FoodOrderingBuddy.Helpers.Api_Helpers.Client;
using FoodOrderingBuddy.ViewModels;
using Newtonsoft.Json;

namespace FoodOrderingBuddy.Helpers.Api_Helpers.Functions
{
    public class MenuItemApiHelper
    {
        private Api_Helpers.Client.ApiClient _apiClient;

        public MenuItemApiHelper()
        {
            _apiClient = new ApiClient();
        }

        public async Task<List<MenuItemViewModel>> GetMenuItems()
        {
            var client = _apiClient.GetApiClient();

            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/menuitems");

            string content = await httpResponseMessage.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<IEnumerable<MenuItemViewModel>>(content).ToList();

            return menuItems;

        }
    }
}