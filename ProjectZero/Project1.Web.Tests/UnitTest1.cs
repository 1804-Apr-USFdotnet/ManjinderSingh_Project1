using System;
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

            string expected = "Restaurant Reviews";

            var action = rc.Index() as ViewResult;

            string actual = action.ViewName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDetails()
        {
            RestaurantController rc = new RestaurantController();
            PLC.Functionality func = new PLC.Functionality();
            var rest = func.GetRestaurant(1);
            string expected = "All Reviews";

            var action = rc.Details(rest) as ViewResult;

            string actual = action.ViewName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEdit()
        {
            RestaurantController rc = new RestaurantController();

            string expected = "Edit Restaurant";

            var action = rc.Edit(1) as ViewResult;

            string actual = action.ViewName;

            Assert.AreEqual(expected, actual);
        }
    }
}
