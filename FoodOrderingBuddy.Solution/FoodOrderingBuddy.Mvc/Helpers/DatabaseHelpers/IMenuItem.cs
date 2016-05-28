using FoodOrderingBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public interface IMenuItem
    {
        IEnumerable<MenuItemViewModel> GetAllMenuItems();
    }
}
