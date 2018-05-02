using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectZero.Libraries.Classes;

namespace ProjectZero.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RestaurantReadTestMethod()
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(List<Restaurant>));
            DataHandler dh = new DataHandler();
            List<Restaurant> restaurants = dh.Read();
            int expected = 1;
            int actual = restaurants.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RestaurantWriteTestMethod()
        {
            DataHandler dh = new DataHandler();
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant(1, "Carl's Jr", "678 Bearss St", "Tampa", "FL", 98765));

            dh.Write(restaurants);
            List<Restaurant> r = dh.Read();

            int expected = restaurants.Count;
            int actual = r.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateRestaurantListTestMethod()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            var r = new Restaurant(1, "Mission BBQ", "12434 Bruce B Downs", "Tampa", "FL", 90778);
            restaurants.Add(r);
            r.Name = "Happi Wok";
            r.RestaurantID = 2;
            r.Address = "12345 Fletcher Rd";
            restaurants.Add(r);

            int expected = 2;
            int actual = restaurants.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TopThreeTestMethod()
        {
            Functionality testSearch = new Functionality();

            var r = testSearch.TopThreeRestaurants();
            List<String> expectedTopThree = new List<string> { };
            List<String> actualTopThree = new List<string>();
            foreach (var item in r)
            {
                actualTopThree.Add(item.Name);
            }
            Assert.AreEqual(expectedTopThree, actualTopThree);
        }
    }
}
