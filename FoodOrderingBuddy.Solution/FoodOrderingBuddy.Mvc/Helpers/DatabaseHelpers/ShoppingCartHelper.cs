using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Models;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public class ShoppingCartHelper : IShoppingCart
    {
        public const string SessionKey = "CART_ID";
        private ShoppingCart _shoppingCart;
        private FoodOrderingBuddyEntities _dbEntities;

        public ShoppingCartViewModel GetCart(HttpContextBase context)
        {
            ShoppingCartViewModel shoppingCartModel = new ShoppingCartViewModel();
            shoppingCartModel.CartId = GetCartbyId(context);

            return shoppingCartModel;
        }

        public string GetCartbyId(HttpContextBase context)
        {
            if (context.Session[SessionKey] == null)
            {
                Guid cartId = Guid.NewGuid();
                context.Session[SessionKey] = cartId.ToString();
            }

            return context.Session[SessionKey].ToString();
        }

        public ShoppingCart GetSingleShoppingCart(string cartId, int Id)
        {
            _shoppingCart = new ShoppingCart();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                _shoppingCart = _dbEntities.ShoppingCarts
                    .Where(y => y.CartId == cartId && y.MenuItemId == Id)
                    .FirstOrDefault();
            }

            return _shoppingCart;
        }

        public IEnumerable<ShoppingCart> GetShoppingCarts()
        {
            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                return _dbEntities.ShoppingCarts
                    .ToList();
            }
        }

        public IEnumerable<ShoppingCart> GetShoppingCarts(string cartId)
        {
            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                return _dbEntities.ShoppingCarts
                    .Where(y => y.CartId == cartId)
                    .ToList();
            }
        }

        public IEnumerable<ShoppingCartViewModel> GetAllMenuItemsbyCartId(string cartId)
        {
            IEnumerable<ShoppingCartViewModel> shoppingCarts;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                shoppingCarts = _dbEntities.ShoppingCarts
                    .Select(y => new ShoppingCartViewModel
                    {
                        CartId = y.CartId,
                        Count = y.Count,
                        DateCreated = y.DateCreated,
                        Id = y.Id,
                        MenuItemId = y.MenuItemId,
                        UserId = y.UserId,
                        MenuItemViewModels = new MenuItemViewModel
                        {
                            DateCreated = y.MenuItem.DateCreated,
                            Description = y.MenuItem.Description,
                            MenuCatergoryId = y.MenuItem.MenuCategoryId,
                            MenuItemId = y.MenuItem.MenuItemId,
                            Name = y.MenuItem.Name,
                            Price = y.MenuItem.Price,
                            Visible = y.MenuItem.Visible,
                            MenuCatergoryViewModel = new MenuCatergoryViewModel()
                            {
                                ImageByte = y.MenuItem.MenuCatergory.Image
                            }
                        }
                    })
                    .Where(y => y.CartId == cartId)
                    .ToList();

                foreach (var item in shoppingCarts)
                {
                    string imageContent = Convert.ToBase64String(item.MenuItemViewModels.MenuCatergoryViewModel.ImageByte);
                    item.MenuItemViewModels.MenuCatergoryViewModel.ImageUrl = String.Format("data:image/png;base64,{0}", imageContent);
                }
            }

            return shoppingCarts;
        }

        public decimal CalculateShoppingCartTotal(string cartid)
        {
            decimal total = 0;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                total = _dbEntities.ShoppingCarts.Where(y => y.CartId == cartid)
                    .ToList()
                    .Select(y => y.Count * y.MenuItem.Price)
                    .Sum();
            }

            return total;
        }

        public int RemovefromShoppingCart(string cartid, int id)
        {
            int itemCount = 0;

            _shoppingCart = new ShoppingCart();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                _shoppingCart = _dbEntities.ShoppingCarts
                   .Where(y => y.CartId == cartid && y.MenuItemId == id)
                   .FirstOrDefault();

                if (_shoppingCart != null)
                {
                    if (_shoppingCart.Count > 1)
                    {
                        _shoppingCart.Count--;
                        itemCount = _shoppingCart.Count;
                        _dbEntities.SaveChanges();
                    }
                    else
                    {
                        _dbEntities.ShoppingCarts.Remove(_shoppingCart);
                        _dbEntities.SaveChanges();
                    }
                }
            }

            return itemCount;
        }

        public int GetCount(string cartid)
        {
            int shoppingCartCount = 0;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                shoppingCartCount = _dbEntities.ShoppingCarts
                    .Where(y => y.CartId == cartid)
                    .ToList()
                    .Sum(c => c.Count);
            }

            return shoppingCartCount;
        }

        public string GetItemName(string cartid, int id)
        {
            string menuItemName = null;

            _shoppingCart = new ShoppingCart();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {

                _shoppingCart = _dbEntities.ShoppingCarts
                    .Where(y => y.CartId == cartid && y.MenuItemId == id)
                    .FirstOrDefault();

                menuItemName = _shoppingCart.MenuItem.Name;
            }

            return menuItemName;
        }

        public int AddtoShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {
            int count = 0;

            _shoppingCart = new ShoppingCart();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                _shoppingCart = _dbEntities.ShoppingCarts
                   .Where(y => y.CartId == shoppingCartViewModel.CartId && y.MenuItemId == shoppingCartViewModel.MenuItemId)
                   .FirstOrDefault();


                if (_shoppingCart == null)
                {
                    ShoppingCart newShoppingCart = new ShoppingCart();
                    newShoppingCart.CartId = shoppingCartViewModel.CartId;
                    newShoppingCart.MenuItemId = shoppingCartViewModel.MenuItemId;
                    newShoppingCart.UserId = shoppingCartViewModel.UserId;
                    newShoppingCart.Count = 1;
                    newShoppingCart.DateCreated = DateTime.Now;

                    _dbEntities.ShoppingCarts.Add(newShoppingCart);

                    count++;
                }
                else
                {
                    count = _shoppingCart.Count++;
                }

                _dbEntities.SaveChanges();
            }

            return count;
        }
    }
}