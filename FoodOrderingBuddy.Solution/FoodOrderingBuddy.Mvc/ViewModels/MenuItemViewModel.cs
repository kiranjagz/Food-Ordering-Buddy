using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingBuddy.Models
{
    public class MenuItemViewModel
    {
        public int MenuItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool Visible { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Required]
        public int MenuCatergoryId { get; set; }

        public MenuCatergoryViewModel MenuCatergoryViewModel { get; set; }

        public IEnumerable<ShoppingCartViewModel> ShoppingCartViewModels { get; set; }
    }
}