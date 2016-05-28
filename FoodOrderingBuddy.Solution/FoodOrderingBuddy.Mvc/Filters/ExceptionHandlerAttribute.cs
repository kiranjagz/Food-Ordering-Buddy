using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Models;

namespace FoodOrderingBuddy.Filters
{
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionViewModel exceptionViewModel = new ExceptionViewModel();
            exceptionViewModel.Exception = filterContext.Exception;
            exceptionViewModel.StackTrace = filterContext.Exception.StackTrace;
            exceptionViewModel.Message = filterContext.Exception.Message;

            LogFilterHelper.Log(exceptionViewModel);
        }
    }
}

