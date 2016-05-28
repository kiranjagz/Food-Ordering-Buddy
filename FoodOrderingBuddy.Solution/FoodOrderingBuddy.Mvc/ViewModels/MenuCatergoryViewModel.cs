using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FoodOrderingBuddy.Models
{
    public class MenuCatergoryViewModel
    {
        public int MenuCatergoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [DataType(DataType.DateTime), Required(ErrorMessage = "DateCreated is required"), Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<MenuItemViewModel> MenuItemViewModels { get; set; }
    }
}