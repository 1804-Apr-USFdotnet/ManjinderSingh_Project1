using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero.Libraries.Classes;
using PLC = ProjectZero.Libraries.Classes;

namespace DataAccess
{
    public class AccessHelper
    {
        private RestaurantDBEntities db;

        // Return a list of all of the restaurants in the database
        public List<PLC.Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants;
            List<PLC.Restaurant> rests = new List<PLC.Restaurant>();
            using (db = new RestaurantDBEntities())
            {
                restaurants = db.Restaurants.ToList();
                foreach (var r in restaurants)
                {
                    rests.Add(DataToLibrary(r));
                }
            }

            return rests;
        }

        // Return a list of all the reviews for the specified restaurant
        public List<PLC.Review> GetAllOfRestaurantsReviews(int restaurantID)
        {
            List<PLC.Review> reviews = new List<PLC.Review>();

            using (db = new RestaurantDBEntities())
            {
                var rvs = db.Reviews.ToList();
                foreach (var record in db.Reviews1.ToList())
                {
                    if (record.RestaurantID == restaurantID)
                    {
                        reviews.Add(ReviewDataToLibraryReview(rvs.ElementAt((int)record.ReviewID - 1)));
                    }
                }
            }

            return reviews;
        }

        // Add a Restaurant to the database
        public void AddRestaurant(PLC.Restaurant restaurant)
        {
            using (db = new RestaurantDBEntities())
            {
                db.Restaurants.Add(LibraryToData(restaurant));
                db.SaveChanges();
            }
        }

        // Update all Restaurant's records Rating to contain its average rating from the reviews
        public void UpdateAverageRating()
        {
            using (db = new RestaurantDBEntities())
            {
                foreach (var rest in db.Restaurants)
                {
                    double averageRating = 0.0;
                    double reviewCount = 0;
                    foreach (var rev in db.Reviews1)
                    {
                        if (rev.RestaurantID == rest.RestaurantID)
                        {
                            var reviewstable = db.Reviews.ToList();
                            averageRating += reviewstable.ElementAt((int)rev.ReviewID -1).Rating;
                            reviewCount++;
                        }
                    }
                    averageRating = averageRating / reviewCount; // average rating
                    rest.Rating = averageRating;
                }
                db.SaveChanges(); // save the changes made
            }
        }

        // Convert a PLC.Restaurant object to a Restuarant.Restuarant table object
        public Restaurant LibraryToData(PLC.Restaurant restaurant)
        {
            Restaurant rest = new Restaurant()
            {
                Name = restaurant.Name,
                Address = restaurant.Address,
                City = restaurant.City,
                State = restaurant.State,
                ZipCode = restaurant.Zipcode,
                Rating = restaurant.Rating,
            };

            return rest;
        }

        // Convert a PLC.Review object to a Restaurant.Review table object
        public Review LibraryReviewToDataReview(PLC.Review review)
        {
            Review r = new Review()
            {
                ReviewID = review.ReviewID,
                Author = review.Author,
                Rating = review.Rating
            };

            return r;
        }

        // Convert a Restaurant.Restaurant table object to a PLC.Restaurant object
        public PLC.Restaurant DataToLibrary(Restaurant rest)
        {
            PLC.Restaurant r = new PLC.Restaurant()
            {
                RestaurantID = rest.RestaurantID,
                Name = rest.Name,
                Address = rest.Address,
                City = rest.City,
                State = rest.State,
                Zipcode = rest.ZipCode,
                Rating = (double)rest.Rating,
            };

            return r;
        }

        // Convert a Restaurant.Review table object to a PLC.Review object
        public PLC.Review ReviewDataToLibraryReview(Review review)
        {
            PLC.Review r = new PLC.Review()
            {
                ReviewID = review.ReviewID,
                Author = review.Author,
                Rating = review.Rating
            };

            return r;
        }
    }
}
