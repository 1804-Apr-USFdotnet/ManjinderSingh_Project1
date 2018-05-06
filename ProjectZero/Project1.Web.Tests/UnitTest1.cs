using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project1.Web.Controllers;
using PLC = ProjectZero.Libraries.Classes;

namespace Project1.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region RestaurantController UnitTests
        [TestMethod]
        public void TestIndex()
        {
            RestaurantController rc = new RestaurantController();

            int expected = 31; // Expected number of Restaurants being passed to the Index View

            var action = rc.Index() as ViewResult;

            List<PLC.Restaurant> model = (List<PLC.Restaurant>)action.Model;

            int actual = model.Count; // Actual number of Restaurants being passed to the view

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDetails()
        {
            RestaurantController rc = new RestaurantController();
            PLC.Functionality func = new PLC.Functionality();
            var rest = func.GetRestaurant(1);

            int expected = 8; // Carl's Jr. Should have 8 reviews

            var action = rc.Details(rest) as ViewResult;

            var model = (List<PLC.Review>)action.Model;
            int actual = model.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEdit()
        {
            RestaurantController rc = new RestaurantController();
            PLC.Functionality func = new PLC.Functionality();
            var rest = func.GetRestaurant(1); // grab Carl's Jr.
            string expected = rest.ToString();

            var action = rc.Edit(1) as ViewResult;
            var r = (PLC.Restaurant)action.Model;

            string actual = r.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearch()
        {
            RestaurantController rc = new RestaurantController();

            int expected = 9; // Number of Restaurant's that have 'c' in it
            var input = new System.Web.Mvc.FormCollection();
            input.Add("search", "c");

            var action = rc.Search(input) as ViewResult;
            var r = (List<PLC.Restaurant>)action.Model;

            int actual = r.Count; 

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTopThree()
        {
            RestaurantController rc = new RestaurantController();

            List<double> expected = new List<double>() { 4.50, 3.67, 3.57 }; // Top 3 ratings

            var action = rc.TopThree() as ViewResult;
            var r = (List<PLC.Restaurant>)action.Model;

            var actual = new List<double>() { Math.Round(r[0].Rating, 2), Math.Round(r[1].Rating, 2), Math.Round(r[2].Rating, 2) };

            CollectionAssert.AreEqual(expected, actual);
        }
        #endregion

        #region ReviewController UnitTests
        [TestMethod]
        public void TestReviewDetails()
        {
            ReviewController rc = new ReviewController();
            PLC.Functionality func = new PLC.Functionality();
            var reviews = func.GetReviews(1); // Grab Carl's Jr. reviews

            string expected = "zschiesterl11"; // Reviewer that gave Carl's Jr. a rating of 4

            var action = rc.Edit(reviews[reviews.Count - 2]) as ViewResult;

            var actual = ((PLC.Review)action.Model).Author; // The actual reviewer that was passed to the view

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
