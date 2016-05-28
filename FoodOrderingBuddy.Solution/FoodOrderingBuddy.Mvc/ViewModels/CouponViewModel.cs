using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class CouponViewModel
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateRedeemed { get; set; }
        public bool IsRedeemed { get; set; }
    }
}