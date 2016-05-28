using System;

namespace FoodOrderingBuddy.Models
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int MenuItemId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public MenuItemViewModel MenuItemViewModels { get; set; }
    }
}