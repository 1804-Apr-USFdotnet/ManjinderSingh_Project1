using ProjectZero.Libraries.Interfaces;
using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Libraries.Classes
{
    public class ConsoleIO : IIO
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        private Functionality func = new Functionality();

        public string MenuOptions()
        {
            string options = "1 - Show all restaurants  2 - Show top three  3 - Search by name   9 - Quit";
            return options;
        }

        public string RestaurantOptions()
        {
            return "1 - Show all reviews in ascending order   2 - Show all reviews in descending order 9 - Quit";
        }

        // Returns a list of all of the selected restaurant's reviews in either ascending or descending order
        public List<Review> AllReviews(int restID, int order)
        {
            List<Review> reviews = func.Reviews(restID);
            if (order == 1)
                reviews.Sort(); // Ascending order
            else
                reviews.OrderByDescending(x => x.Rating); // Descending order

            return reviews;
        }

        // Returns a list of all of the restaurants in the specified order
        public List<Restaurant> GetAll(string order = "")
        {
            return func.AllRestaurants(order);
        }

        // Returns a list of all restaurants that matched the search string
        public List<Restaurant> Search(string searchString)
        {
            return func.SearchRestaurantsByName(searchString);
        }

        public string Result(int input)
        {
            throw new NotImplementedException();
        }
    }
}
