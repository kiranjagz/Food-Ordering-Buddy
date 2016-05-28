using FoodOrderingBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public interface IOrder
    {
        int CreateNewOrder(HttpContextBase context);
        void EmptyCart(string cartid);
        OrderViewModel GetOrderbyId(int id);
        List<OrderViewModel> GetAllOrders();
    }
}
