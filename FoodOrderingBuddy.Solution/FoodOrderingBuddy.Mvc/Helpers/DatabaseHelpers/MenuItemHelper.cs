using System.Collections.Generic;
using System.Linq;
using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Models;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public class MenuItemHelper : IMenuItem
    {
        private FoodOrderingBuddyEntities _dbEntities;
        public IEnumerable<MenuItemViewModel> GetAllMenuItems()
        {
            IEnumerable<MenuItemViewModel> menuItems;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                menuItems = _dbEntities.MenuItems
                   .Select(y => new MenuItemViewModel
                   {
                       Name = y.Name,
                       Description = y.Description,
                       Price = y.Price,
                       DateCreated = y.DateCreated,
                       MenuItemId = y.MenuItemId,
                       Visible = y.Visible,
                       MenuCatergoryId = y.MenuCategoryId
                   }).ToList();
            }

            return menuItems;
        }
    }
}