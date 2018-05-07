using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLC = ProjectZero.Libraries.Classes;
using NLog;

namespace Project1.Web.Controllers
{
    public class ReviewController : Controller
    {
        private PLC.Functionality func;
        private Logger logger = LogManager.GetCurrentClassLogger();
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
        public ActionResult Create(PLC.Review review, int restID)
        {
            try
            {
                if (isValid(review))
                {
                    if (func == null)
                        func = new PLC.Functionality();

                    func.AddReview(restID, review);

                    var rest = func.GetRestaurant(restID);

                    return RedirectToAction("Details", "Restaurant", rest);
                }

                return RedirectToAction("Index", "Restaurant");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return RedirectToAction("Index", "Restaurant");
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
                if (isValid(rev))
                {
                    if (func == null)
                        func = new PLC.Functionality();

                    rest = func.GetRestaurant(restID);

                    func.UpdateReview(restID, rev);

                    return RedirectToAction("Details", "Restaurant", rest);
                }

                return RedirectToAction("Index", "Restaurant");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return RedirectToAction("Index", "Restaurant", rest);
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

        [NonAction]
        public bool isValid(PLC.Review rev)
        {
            if (rev.Author.Length > 15) // Review author's name should be 15 characters or less
                return false;
            else if (rev.Rating <= 0 || rev.Rating > 5)
                return false;

            return true;
        }
    }
}
