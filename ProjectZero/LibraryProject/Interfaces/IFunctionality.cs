using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero.Libraries.Classes;

namespace ProjectZero.Libraries.Interfaces
{
    interface IFunctionality
    {
        // This Interface will define functionality for searching through a List of Restaurants

        // The user should be able to look at the top 3 restaurants in the list
        List<Restaurant> TopThreeRestaurants();

        // The following function should allow the user to search for restaurants by name
        // It will return a list of restaurants that matched the search string
        List<Restaurant> SearchRestaurantsByName(string searchString);

        // The following function will return a string of the details of a selected restaurant
        // that can be used to display to the screen
        string RestaurantDetails(string restaurantName);

        // Return a Restaurant item
        Restaurant GetRestaurant(string restaurantName);

        // Return all Restaurants, and also allow ascending/descending order
        List<Restaurant> AllRestaurants(string order = "");

    }
}
