using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public int MenuItemsTotal { get; set; }
        public decimal Total { get; set; }
        public int? QueueId { get; set; }

        public IEnumerable<OrderItemViewModel> OrderItemViewModels { get; set; }
    }
}