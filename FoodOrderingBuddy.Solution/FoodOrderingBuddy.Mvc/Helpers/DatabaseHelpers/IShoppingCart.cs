using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public interface IShoppingCart
    {
        ShoppingCartViewModel GetCart(HttpContextBase context);
        string GetCartbyId(HttpContextBase context);
        ShoppingCart GetSingleShoppingCart(string cartId, int Id);
        IEnumerable<ShoppingCart> GetShoppingCarts();
        IEnumerable<ShoppingCart> GetShoppingCarts(string cartId);
        IEnumerable<ShoppingCartViewModel> GetAllMenuItemsbyCartId(string cartId);
        decimal CalculateShoppingCartTotal(string cartid);
        int RemovefromShoppingCart(string cartid, int id);
        int GetCount(string cartid);
        string GetItemName(string cartid, int id);
        int AddtoShoppingCart(ShoppingCartViewModel shoppingCartViewModel);
    }
}
