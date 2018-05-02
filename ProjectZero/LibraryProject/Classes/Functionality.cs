using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ProjectZero.Libraries.Interfaces;
using DataAccess;

namespace ProjectZero.Libraries.Classes
{
    public class Functionality : IFunctionality
    {
        private List<Restaurant> restaurants;
        private DataHandler DataHandler; // no longer needed
        private AccessHelper ah; // Connect to the database
        private Logger logger = LogManager.GetCurrentClassLogger();

        public Functionality()
        {
            // Grab data from a XMl file 
            //DataHandler = new DataHandler();
            //restaurants = DataHandler.Read();

            // Connect to the database through the AccessHelper object
            ah = new AccessHelper();
            restaurants = ah.GetAllRestaurants(); // Grab a list of all the restuarants from the database
        }

        // Returns a list of all of the Restaurants within the database
        public List<Restaurant> AllRestaurants(string order = "")
        {
            List<Restaurant> r = new List<Restaurant>();
            if (order != "")
            {
                order.ToLower();
                if (order.Contains("ascen"))
                    restaurants.Sort();
                else
                    restaurants = restaurants.OrderByDescending(x => x.Rating).ToList(); // In descending order based on average rating
                r = restaurants;
                restaurants = ah.GetAllRestaurants();
                return r;
            }
            return restaurants;
        }

        // Searches for a specified restaurant
        public Restaurant GetRestaurant(string restaurantName)
        {
            return SearchRestaurantsByName(restaurantName)[0];
        }

        // Returns a string output of the selected Restaurants Details
        public string RestaurantDetails(string restaurantName)
        {
            // Grab the restaurant
            var rest = SearchRestaurantsByName(restaurantName)[0];

            return rest.ToString();
        }

        // Returns a list of all restaurants that have full/partial match to the search string
        public List<Restaurant> SearchRestaurantsByName(string searchString)
        {
            List<Restaurant> r = new List<Restaurant>();
            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name.Contains(searchString))
                {
                    r.Add(restaurant);
                }
            }
            return r;
        }

        // Returns a list of the top 3 rated restaurant in the database
        public List<Restaurant> TopThreeRestaurants()
        {
            List<Restaurant> r = new List<Restaurant>();
            restaurants = restaurants.OrderByDescending(x => x.Rating).ToList(); // sort the restaurants in Descending order
            for (int i = 0; i < 3; i++)
            {
                r.Add(restaurants[i]);
            }
            // Grab the list of Restaurants from the database again so that it's unsorted again
            restaurants = ah.GetAllRestaurants();
            return r;
        }

        // Returns a string output of the top three rated restaurants in the database
        public string PrintTopThreeRestaurants()
        {
            var topThree = TopThreeRestaurants();
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (var rest in topThree)
            {
                sb.Append(count + ".)\t" + rest.ToString() + "\n");
                count++;
            }
            return sb.ToString();
        }

        // Grab all of the reviews for the selected Restaurant
        public List<Review> Reviews(int restID)
        {
            List<Review> reviews = ah.GetAllOfRestaurantsReviews(restID);

            return reviews;
        }
    }
}
