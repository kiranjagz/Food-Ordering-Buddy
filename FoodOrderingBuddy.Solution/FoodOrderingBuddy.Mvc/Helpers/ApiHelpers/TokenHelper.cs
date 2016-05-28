using FoodOrderingBuddy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;

namespace FoodOrderingBuddy.Helpers.ApiHelpers
{
    public class TokenHelper
    {
        public TokenModel GetToken(NameValueCollection collection)
        {
            TokenModel tokenModel;

            using (WebClient webClient = new WebClient())
            {
                var response = webClient.UploadValues("http://localhost:28340/token", collection);

                string result = System.Text.Encoding.UTF8.GetString(response);

                tokenModel = JsonConvert.DeserializeObject<TokenModel>(result);
            }

            return tokenModel;
        }
    }
}