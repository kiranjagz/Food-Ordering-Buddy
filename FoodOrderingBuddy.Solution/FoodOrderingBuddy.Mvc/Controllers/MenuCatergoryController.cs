using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrderingBuddy.Helpers;
using FoodOrderingBuddy.Helpers.DatabaseHelpers;
using FoodOrderingBuddy.Models;
using PagedList;

namespace FoodOrderingBuddy.Controllers
{
    public class MenuCatergoryController : Controller
    {
        IMenuCatergory menuCatergory = null;

        public MenuCatergoryController(IMenuCatergory concreteImplementation)
        {
            this.menuCatergory = concreteImplementation;
        }

        // GET: Catergory
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var catergories = menuCatergory.GetAllMenuCatergories();

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("Index", catergories.ToPagedList(pageNumber, pageSize));
        }

        // GET: Catergory/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var catergory = menuCatergory.GetMenuCatergory(id);

            return View("Details", catergory);
        }

        // GET: Catergory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catergory/Create
        [HttpPost]
        public ActionResult Create(MenuCatergoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var saved = menuCatergory.SaveCatergory(model);

                if (saved)
                    return RedirectToAction("Index");
                else
                    return View();
            }

            return View();
        }

        // GET: Catergory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Catergory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Catergory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Catergory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
