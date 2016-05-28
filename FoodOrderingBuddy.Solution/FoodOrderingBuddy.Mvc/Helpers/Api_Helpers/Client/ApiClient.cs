using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace FoodOrderingBuddy.Helpers.Api_Helpers.Client
{
    public class ApiClient
    {
        private const string ApiUrl = "http://localhost:1754/";
        private HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
        }

        public HttpClient GetApiClient()
        {
            _client.BaseAddress = new Uri(ApiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }
    }
}