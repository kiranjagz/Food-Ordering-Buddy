using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Models;
using Newtonsoft.Json;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace FoodOrderingBuddy.Filters
{
    public class LoggerAttribute : ActionFilterAttribute
    {
        private ActionLogViewModel _actionLogViewModel;
        private string _serializedJsonActionLog = null;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actionLogViewModel = new ActionLogViewModel();
            _actionLogViewModel.Method = "OnActionExecuting";
            _actionLogViewModel.Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _actionLogViewModel.Action = filterContext.ActionDescriptor.ActionName;
            _actionLogViewModel.DateCreated = filterContext.HttpContext.Timestamp;
            _actionLogViewModel.Ip = filterContext.HttpContext.Request.UserHostAddress;

            _serializedJsonActionLog = JsonConvert.SerializeObject(_actionLogViewModel);
            LogFilterHelper.Log(_serializedJsonActionLog);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _actionLogViewModel = new ActionLogViewModel();
            _actionLogViewModel.Method = "OnActionExecuted";
            _actionLogViewModel.Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _actionLogViewModel.Action = filterContext.ActionDescriptor.ActionName;
            _actionLogViewModel.DateCreated = filterContext.HttpContext.Timestamp;
            _actionLogViewModel.Ip = filterContext.HttpContext.Request.UserHostAddress;

            _serializedJsonActionLog = JsonConvert.SerializeObject(_actionLogViewModel);
            LogFilterHelper.Log(_serializedJsonActionLog);
        }
    }
}