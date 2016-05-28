using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class TokenModel
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Expires_In { get; set; }
    }
}