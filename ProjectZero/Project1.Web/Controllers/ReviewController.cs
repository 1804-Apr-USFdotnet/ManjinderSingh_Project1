using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLC = ProjectZero.Libraries.Classes;

namespace Project1.Web.Controllers
{
    public class ReviewController : Controller
    {
        private PLC.Functionality func;
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Review/Create
        public ActionResult Create(int restID)
        {
            PLC.Review review = new PLC.Review() { RestaurantID = restID };
            return View(review);
        }

        // POST: Review/Create
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

        // GET: Review/Edit/5
        public ActionResult Edit(PLC.Review review)
        {
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(PLC.Review rev, int restID)
        {
            PLC.Restaurant rest = new PLC.Restaurant();
            try
            {
                if (func == null)
                    func = new PLC.Functionality();

                rest = func.GetRestaurant(restID);

                func.UpdateReview(restID, rev);

                return RedirectToAction("Details", "Restaurant", rest);
            }
            catch
            {
                return RedirectToAction("Details", "Restaurant", rest);
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(PLC.Review rev)
        {
            if (func == null)
                func = new PLC.Functionality();

            func.DeleteReview(rev.RestaurantID, rev.ReviewID);

            var rest = func.GetRestaurant(rev.RestaurantID);

            return RedirectToAction("Details", "Restaurant", rest);
        }

        // POST: Review/Delete/5
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
