using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLC = ProjectZero.Libraries.Classes;
using NLog;

namespace Project1.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private PLC.Functionality func;
        private Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Restaurants
        public ActionResult Index()
        {
            List<PLC.Restaurant> restaurants = new List<PLC.Restaurant>(); ;
            try
            {
                if (func == null)
                {
                    func = new PLC.Functionality();
                }

                // Grab all of the restaurants from the database
                restaurants = func.AllRestaurants();
                return View(restaurants);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return View();
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurant/Edit/5
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

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurant/Delete/5
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
