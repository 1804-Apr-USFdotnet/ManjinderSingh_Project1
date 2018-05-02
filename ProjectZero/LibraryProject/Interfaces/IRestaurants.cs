using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Libraries.Interfaces
{
    interface IRestaurants
    {
        int RestaurantID { get; set; }
        string Name { get; set; }       // The name of the restaurant
        string Address { get; set; }    // The address of the restaurant
        string City { get; set; }       // The city the restaurant is in
        string State { get; set; }      // The state the restaurant is in
        int Zipcode { get; set; }       // The zip code of the restaurant
        double Rating { get; set; }    // The average rating of the restaurant

        //void SortReviews(string criteria);    // Sort through the reviews using the criteria
        //double ComputeRating();        // Compute the average rating from the reviews
    }
}
