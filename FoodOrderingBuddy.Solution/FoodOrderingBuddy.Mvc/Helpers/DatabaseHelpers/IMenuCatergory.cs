using FoodOrderingBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public interface IMenuCatergory
    {
        IEnumerable<MenuCatergoryViewModel> GetAllMenuCatergories();
        MenuCatergoryViewModel GetMenuCatergory(int id);
        bool SaveCatergory(MenuCatergoryViewModel menuCatergoryViewModel);
    }
}
