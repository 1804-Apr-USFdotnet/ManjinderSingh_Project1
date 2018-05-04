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
                return View("Error", ex);
            }
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(PLC.Restaurant rest)
        {
            if (func == null)
                func = new PLC.Functionality();
            // Grab all of the selected Restaurant's reviews
            var reviews = func.GetReviews(rest.RestaurantID);
            
            // Add the RestaurantID to each of the review objects for future reference
            foreach (var review in reviews)
            {
                review.RestaurantID = rest.RestaurantID;
            }

            return View(reviews);
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(PLC.Restaurant Model)
        {
            try
            {
                // Add the Restaurant into the database

                if (func == null)
                    func = new PLC.Functionality();

                func.AddRestaurant(Model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(PLC.Restaurant rest)
        {
            return View(rest);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(PLC.Restaurant rest, int id)
        {
            try
            {
                if (func == null)
                    func = new PLC.Functionality();
                // Make that an Edit did take place
                var original = func.GetRestaurant(id);
                if (original.Equals(rest))
                {
                    // It's the same so just go back to the Index without updating the database
                    return RedirectToAction("Index");
                }
                else
                {
                    // The Restaurant's details have been changed so we need to update the database
                    func.UpdateRestaurant(rest);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(PLC.Restaurant rest)
        {
            try
            {
                if (func == null)
                    func = new PLC.Functionality();

                func.DeleteRestaurant(rest);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return RedirectToAction("Index");
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
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}
