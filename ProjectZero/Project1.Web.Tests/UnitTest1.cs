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
    }
}
