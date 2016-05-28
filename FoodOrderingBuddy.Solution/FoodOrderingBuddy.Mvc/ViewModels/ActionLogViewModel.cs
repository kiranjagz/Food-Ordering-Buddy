using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class ActionLogViewModel
    {
        public string Method { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Ip { get; set; }
        public DateTime DateCreated { get; set; }
    }
}