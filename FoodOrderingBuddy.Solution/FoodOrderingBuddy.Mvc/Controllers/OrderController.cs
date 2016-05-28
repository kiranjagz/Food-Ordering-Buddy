using System;
using System.Linq;
using System.Web.Mvc;
using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Filters;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Models;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;
using FoodOrderingBuddy.Helpers.NotifyMeServiceWcfHelpers;

namespace FoodOrderingBuddy.Controllers
{
    [LoggerAttribute]
    public class OrderController : Controller
    {
        IOrder order = null;
        NotifyMeServiceWcfHelper NotifyMeService;

        public OrderController(IOrder concreteImplementation)
        {
            this.order = concreteImplementation;
            NotifyMeService = new NotifyMeServiceWcfHelper();
        }

        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order//ProcessOrder
        [HttpGet]
        public ActionResult ProcessOrder()
        {
            int orderId = 0;
            bool isSent;

            orderId = order.CreateNewOrder(HttpContext);

            //if (orderId > 0)
            //    isSent = NotifyMeService.SendaSnailMail(orderId);

            return RedirectToAction("Summary", new { id = orderId });
        }

        // GET: Order/Summary/9
        [HttpGet]
        public ActionResult Summary(int id)
        {
            OrderViewModel orderViewModel = order.GetOrderbyId(id);

            return View(orderViewModel);
        }

        // GET: Order/ViewOrders/9
        [HttpGet]
        public ActionResult ViewOrders()
        {
            var orders = order.GetAllOrders();

            return View(orders);
        }
    }
}