using FoodOrderingBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace FoodOrderingBuddy.Helpers.ApiHelpers
{
    public class ApiClientHelper
    {
        private const string CouponApi = "http://localhost:28340/";

        public static HttpClient GetClient(AuthenticationHeaderValue authenticationHeaderValue)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CouponApi);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            return client;
        }

        public static AuthenticationHeaderValue CreateOAuthCredentials(TokenModel tokenModel)
        {
            string toEncode = tokenModel.Access_Token;
            // The current HTTP specification says characters here are ISO-8859-1.
            // However, the draft specification for the next version of HTTP indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            byte[] toBase64 = encoding.GetBytes(toEncode);
            string parameter = Convert.ToBase64String(toBase64);

            //return new AuthenticationHeaderValue("Bearer", parameter);
            AuthenticationHeaderValue authHeader = new AuthenticationHeaderValue("Bearer", toEncode);

            return authHeader;
        }
    }
}