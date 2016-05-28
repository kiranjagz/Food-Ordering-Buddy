using System.Collections.Generic;
using System.Linq;
using FoodOrderingBuddy.Data;
using FoodOrderingBuddy.Models;
using System;

namespace FoodOrderingBuddy.Helpers.DatabaseHelpers
{
    public class MenuCatergoryHelper : IMenuCatergory
    {
        private FoodOrderingBuddyEntities _dbEntities;
        public IEnumerable<MenuCatergoryViewModel> GetAllMenuCatergories()
        {
            IEnumerable<MenuCatergoryViewModel> menuCatergoryViewModels;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                menuCatergoryViewModels = _dbEntities.MenuCatergories
                    .Select(y => new MenuCatergoryViewModel
                    {
                        Name = y.Name,
                        DateCreated = y.DateCreated,
                        MenuCatergoryId = y.MenuCatergoryId,
                        ImageByte = y.Image,
                        MenuItemViewModels = y.MenuItems.Select(x => new MenuItemViewModel
                        {
                            Name = x.Name,
                            DateCreated = x.DateCreated,
                            Description = x.Description,
                            Visible = x.Visible,
                            MenuCatergoryId = x.MenuCategoryId,
                            MenuItemId = x.MenuItemId,
                            Price = x.Price
                        })
                    }).ToList();

                foreach (var item in menuCatergoryViewModels)
                {
                    string imageContent = Convert.ToBase64String(item.ImageByte);
                    item.ImageUrl = String.Format("data:image/png;base64,{0}", imageContent);
                }
            }

            return menuCatergoryViewModels;
        }

        public MenuCatergoryViewModel GetMenuCatergory(int id)
        {
            MenuCatergoryViewModel menuCatergoryViewModel;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                menuCatergoryViewModel = _dbEntities.MenuCatergories
                    .Where(y => y.MenuCatergoryId == id)
                    .Select(y => new MenuCatergoryViewModel()
                    {
                        MenuCatergoryId = y.MenuCatergoryId,
                        Name = y.Name,
                        DateCreated = y.DateCreated,
                        ImageByte = y.Image
                    }).FirstOrDefault();

                string imageContent = Convert.ToBase64String(menuCatergoryViewModel.ImageByte);
                menuCatergoryViewModel.ImageUrl = String.Format("data:image/png;base64,{0}", imageContent);
            }

            return menuCatergoryViewModel;
        }

        public bool SaveCatergory(MenuCatergoryViewModel menuCatergoryViewModel)
        {
            bool isSaved = true;

            using (_dbEntities = new FoodOrderingBuddyEntities())
            {
                MenuCatergory menuCatergory = new MenuCatergory();
                menuCatergory.Name = menuCatergoryViewModel.Name;
                menuCatergory.DateCreated = menuCatergoryViewModel.DateCreated;

                var content = new byte[menuCatergoryViewModel.Image.ContentLength];
                menuCatergoryViewModel.Image.InputStream.Read(content, 0, menuCatergoryViewModel.Image.ContentLength);

                menuCatergory.Image = content;

                _dbEntities.MenuCatergories.Add(menuCatergory);
                int value = _dbEntities.SaveChanges();

                if (value < 0)
                    isSaved = false;
            }

            return isSaved;
        }
    }
}