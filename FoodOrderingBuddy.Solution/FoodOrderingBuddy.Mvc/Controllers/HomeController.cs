using System;
using System.Web.Mvc;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Models;
using FoodOrderingBuddy.Filters;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;
using PagedList;
using System.Linq;

namespace FoodOrderingBuddy.Controllers
{
    [LoggerAttribute]
    public class HomeController : Controller
    {
        IMenuCatergory menuCatergory = null;

        public HomeController(IMenuCatergory concreteImplementation)
        {
            this.menuCatergory = concreteImplementation;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            var menuItems = menuCatergory.GetAllMenuCatergories();

            menuItems = menuItems.Where(x => x.MenuItemViewModels.Count() > 0);

            return View(menuItems);
        }

    }
}