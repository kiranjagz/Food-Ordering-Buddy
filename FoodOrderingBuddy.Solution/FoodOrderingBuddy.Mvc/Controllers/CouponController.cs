using FoodOrderingBuddy.Helpers.ApiHelpers;
using FoodOrderingBuddy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingBuddy.Controllers
{
    public class CouponController : Controller
    {
        private CouponViewModel _couponViewModel;
        private TokenModel _tokenModel;

        private TokenHelper _tokenHelper;

        // GET: Coupon
        public ActionResult Index()
        {
            return View();
        }

        // POST: Coupon/GetCoupon/Qwe123
        [HttpPost]
        public async Task<JsonResult> GetCoupon(string code)
        {
            _tokenHelper = new TokenHelper();
            _tokenModel = new TokenModel();
            _couponViewModel = new CouponViewModel();

            NameValueCollection collection = new NameValueCollection();
            collection.Add("grant_type", "password");
            collection.Add("username", "Kiran");
            collection.Add("password", "Kiran1234!");

            _tokenModel = _tokenHelper.GetToken(collection);

            var authHeaders = ApiClientHelper.CreateOAuthCredentials(_tokenModel);

            var apiClient = ApiClientHelper.GetClient(authHeaders);

            HttpResponseMessage couponResponse = await apiClient.GetAsync("api/coupon/" + code);

            if (couponResponse.IsSuccessStatusCode)
            {
                string content = await couponResponse.Content.ReadAsStringAsync();

                _couponViewModel = JsonConvert.DeserializeObject<CouponViewModel>(content);
            }
            else
            {
                _couponViewModel.IsRedeemed = true;
            }

            return Json(_couponViewModel);
        }
    }
}