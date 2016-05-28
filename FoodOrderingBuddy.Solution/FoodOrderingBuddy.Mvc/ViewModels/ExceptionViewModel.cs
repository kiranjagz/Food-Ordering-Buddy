using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class ExceptionViewModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Exception Exception { get; set; }
    }
}