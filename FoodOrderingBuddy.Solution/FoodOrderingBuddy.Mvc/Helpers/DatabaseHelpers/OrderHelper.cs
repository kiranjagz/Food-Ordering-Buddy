using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Models;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public class OrderHelper : IOrder
    {
        private FoodOrderingBuddyEntities _dbEntities;
        private ShoppingCartHelper _shoppingCartHelper;
        public struct OrderResponse
        {
            public int OrderId;
            public string Username;
            public decimal Total;
            public DateTime DateCreated;
            public int MenuItemsTotalCount;
        };
        public int CreateNewOrder(HttpContextBase context)
        {
            string cartId = null, orderResponse = null;
            int count = 0, orderId = 0;
            decimal total = 0;

            _shoppingCartHelper = new ShoppingCartHelper();

            cartId = _shoppingCartHelper.GetCartbyId(context);
            count = _shoppingCartHelper.GetCount(cartId);
            total = _shoppingCartHelper.CalculateShoppingCartTotal(cartId);

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                var shoppingCarts = _dbEntities.ShoppingCarts.Where(y => y.CartId == cartId).ToList();

                Order order = new Order();
                order.Username = context.User.Identity.Name;
                order.Total = total;
                order.DateCreated = DateTime.Now;
                order.MenuItemsTotal = count;

                _dbEntities.Orders.Add(order);
                int value = _dbEntities.SaveChanges();
                orderId = order.OrderId;

                if (value > 0)
                {
                    var savedOrder = _dbEntities.Orders.Where(y => y.OrderId == orderId).FirstOrDefault();

                    if (savedOrder != null)
                    {
                        #region struct response object
                        OrderResponse response;
                        response.OrderId = savedOrder.OrderId;
                        response.Username = savedOrder.Username;
                        response.Total = savedOrder.Total;
                        response.DateCreated = savedOrder.DateCreated;
                        response.MenuItemsTotalCount = savedOrder.MenuItemsTotal;
                        #endregion

                        orderResponse = CommonUtilityHelper.SerializeObject(response);

                        savedOrder.OrderResponse = orderResponse;
                    }

                    foreach (var item in shoppingCarts)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderId = orderId;
                        orderItem.Price = item.MenuItem.Price;
                        orderItem.Quantity = item.Count;
                        orderItem.MenuItemId = item.MenuItemId;

                        _dbEntities.OrderItems.Add(orderItem);
                    }

                    _dbEntities.SaveChanges();

                    EmptyCart(cartId);
                }
            }

            return orderId;
        }
        public void EmptyCart(string cartid)
        {
            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                var shoppingCarts = _dbEntities.ShoppingCarts.Where(y => y.CartId == cartid).ToList();

                foreach (var item in shoppingCarts)
                {
                    _dbEntities.ShoppingCarts.Remove(item);
                }

                _dbEntities.SaveChanges();
            }
        }
        public OrderViewModel GetOrderbyId(int id)
        {
            OrderViewModel orderViewModel = new OrderViewModel();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                orderViewModel = _dbEntities.Orders
                    .Where(y => y.OrderId == id)
                    .Select(y => new OrderViewModel()
                    {
                        OrderId = y.OrderId,
                        Username = y.Username,
                        DateCreated = y.DateCreated,
                        MenuItemsTotal = y.MenuItemsTotal,
                        Total = y.Total,
                        OrderItemViewModels = y.OrderItems.Select(x => new OrderItemViewModel()
                        {
                            OrderItemId = x.OrderItemId,
                            OrderId = x.OrderId,
                            MenuItemId = x.MenuItemId,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            MenuItemName = x.MenuItem.Name,
                            MenuItemDescription = x.MenuItem.Description,
                            MenuItemPrice = x.MenuItem.Price
                        })
                    }).FirstOrDefault();
            }

            return orderViewModel;
        }
        public List<OrderViewModel> GetAllOrders()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                orderViewModels = _dbEntities.Orders
                    .Select(y => new OrderViewModel()
                    {
                        OrderId = y.OrderId,
                        Username = y.Username,
                        DateCreated = y.DateCreated,
                        MenuItemsTotal = y.MenuItemsTotal,
                        Total = y.Total,
                        QueueId = y.QueueId,
                        OrderItemViewModels = y.OrderItems.Select(x => new OrderItemViewModel()
                        {
                            OrderItemId = x.OrderItemId,
                            OrderId = x.OrderId,
                            MenuItemId = x.MenuItemId,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            MenuItemName = x.MenuItem.Name,
                            MenuItemDescription = x.MenuItem.Description,
                            MenuItemPrice = x.MenuItem.Price
                        })
                    }).ToList();
            }

            return orderViewModels;
        }
    }
}