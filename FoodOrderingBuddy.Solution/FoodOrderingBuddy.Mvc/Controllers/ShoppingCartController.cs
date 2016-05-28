using System.Web.Mvc;
using FoodOrderingBuddy.Filters;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Models;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;

namespace FoodOrderingBuddy.Controllers
{
    [LoggerAttribute]
    public class ShoppingCartController : Controller
    {
        private CartSummaryViewModel _cartSummaryViewModel;
        IShoppingCart shoppingCart = null;

        public ShoppingCartController(IShoppingCart concreteImplementation)
        {
            this.shoppingCart = concreteImplementation;
        }
        // GET: ShoppingCart
        [HttpGet]
        public ActionResult Index()
        {
            decimal total = 0;
            string cartId = null;

            _cartSummaryViewModel = new CartSummaryViewModel();

            cartId = shoppingCart.GetCartbyId(HttpContext);

            var model = shoppingCart.GetAllMenuItemsbyCartId(cartId);

            total = shoppingCart.CalculateShoppingCartTotal(cartId);
            ViewBag.Total = total;

            return View(model);
        }

        // POST: /ShoppingCart/RemovefromCart/9
        [HttpPost]
        public JsonResult RemovefromCart(int id)
        {
            string cartId = null, menuItemName = null;
            int itemCount = 0, shoppingCartCount = 0;
            decimal total = 0;

            _cartSummaryViewModel = new CartSummaryViewModel();
            shoppingCart = new ShoppingCartHelper();

            cartId = shoppingCart.GetCartbyId(HttpContext);

            menuItemName = shoppingCart.GetItemName(cartId, id);

            itemCount = shoppingCart.RemovefromShoppingCart(cartId, id);

            shoppingCartCount = shoppingCart.GetCount(cartId);

            total = shoppingCart.CalculateShoppingCartTotal(cartId);

            _cartSummaryViewModel.Count = shoppingCartCount;
            _cartSummaryViewModel.Message = "An Item has been deleted: [1] " + Server.HtmlEncode(menuItemName);
            _cartSummaryViewModel.Total = total;
            _cartSummaryViewModel.ItemCount = itemCount;
            _cartSummaryViewModel.DeleteId = id;

            ViewBag.Total = total;

            return Json(_cartSummaryViewModel);
        }

        // POST: /ShoppingCart/AddtoCart/9
        [HttpPost]
        public JsonResult AddtoCart(int id)
        {
            int itemCount = 0, shoppingCartCount = 0;

            CartSummaryViewModel cartSummaryViewModel = new CartSummaryViewModel();

            var cart = shoppingCart.GetCart(HttpContext);

            ShoppingCartViewModel model = new ShoppingCartViewModel();
            model.CartId = cart.CartId;
            model.MenuItemId = id;
            model.UserId = HttpContext.User.Identity.Name;

            itemCount = shoppingCart.AddtoShoppingCart(model);
            shoppingCartCount = shoppingCart.GetCount(model.CartId);

            var item = shoppingCart.GetItemName(model.CartId, id);

            cartSummaryViewModel.Count = shoppingCartCount;
            cartSummaryViewModel.ItemCount = itemCount;
            cartSummaryViewModel.Message = "An item has been successfully added to your cart: [1] " + item;

            return Json(cartSummaryViewModel);
        }

        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        [HttpGet]
        public ActionResult CartSummary()
        {
            string cartId = null;
            int shoppingCartCount = 0;

            _cartSummaryViewModel = new CartSummaryViewModel();

            cartId = shoppingCart.GetCartbyId(HttpContext);

            shoppingCartCount = shoppingCart.GetCount(cartId);

            _cartSummaryViewModel.Count = shoppingCartCount;

            return PartialView(_cartSummaryViewModel);
        }
    }
}