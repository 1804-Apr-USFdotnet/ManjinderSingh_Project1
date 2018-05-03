using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero.Libraries.Classes;
using PLC = ProjectZero.Libraries.Classes;
using NLog;

namespace DataAccess
{
    public class AccessHelper
    {
        private RestaurantDBEntities db;
        private Logger logger = LogManager.GetCurrentClassLogger();

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

        // Update an existing restaurant in the database
        public void UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                // Search for the restaurant using it's ID
                using (db = new RestaurantDBEntities())
                {
                    var rest = db.Restaurants.Where(x => x.RestaurantID == restaurant.RestaurantID).FirstOrDefault();

                    // Update the restaurant
                    if (rest != null)
                    {
                        rest.Name = restaurant.Name;
                        rest.Address = restaurant.Address;
                        rest.City = restaurant.City;
                        rest.State = restaurant.State;
                        rest.ZipCode = restaurant.ZipCode;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    // Save the changes
                    db.SaveChanges();
                }
            }
            catch (NotImplementedException ne)
            {
                logger.Error(ne.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // Delete a selected Restaurant and all of it's corresponding reviews
        public void DeleteRestaurant(Restaurant restaurant)
        {
            try
            {
                using (db = new RestaurantDBEntities())
                {
                    if (restaurant != null)
                    {
                        // Remove the restaurant from the database
                        db.Restaurants.Remove(restaurant);

                        // Remove all of the it's reviews from the database
                        var reviewsToDelete = db.Reviews1.Where(x => x.RestaurantID == restaurant.RestaurantID).ToList();
                        if (reviewsToDelete != null)
                        {
                            foreach (var review in reviewsToDelete)
                            {
                                var r = db.Reviews.Where(x => x.ReviewID == review.ReviewID).FirstOrDefault();
                                db.Reviews.Remove(r); // Remove from the Review table
                                db.Reviews1.Remove(review); // Remove from the Reviews table
                            }
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    // Save the changes to the database
                    db.SaveChanges();
                }
            }
            catch (NotImplementedException ne)
            {
                logger.Error(ne.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // Insert a new review for a restaurant
        public void AddReview(Review review, int restID)
        {
            try
            {
                using (db = new RestaurantDBEntities())
                {
                    // Add the Review to the Review table
                    db.Reviews.Add(review);

                    var id = db.Reviews.Find(db.Reviews.Count() - 1).ReviewID;
                    // Add the reference to the Reviews table
                    var r = new Review1()
                    {
                        RestaurantID = restID,
                        ReviewID = id
                    };
                    db.Reviews1.Add(r);

                    // Update the Restaurant's average rating
                    UpdateRestaurantAverageRating(restID);

                    // Save the changes
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // Update a selected Review
        public void UpdateReview(Review review, int restID)
        {
            try
            {
                using (db = new RestaurantDBEntities())
                {
                    // Find the Review within the database and update it
                    var r = db.Reviews.Where(x => x.ReviewID == review.ReviewID).FirstOrDefault();
                    if (r != null)
                    {
                        r.Author = review.Author;
                        r.Rating = review.Rating;
                    }

                    // Update the Restaurant's average rating
                    UpdateRestaurantAverageRating(restID);

                    // Save the changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // Delete a selected restaurant review
        public void DeleteRestaurantReview(int revID, int restID)
        {
            try
            {
                using (db = new RestaurantDBEntities())
                {
                    // Delete the restaurant review
                    db.Reviews.Remove(db.Reviews.Find(revID));

                    // Delete the review from the Reviews table
                    var rev = db.Reviews1.Where(x => x.RestaurantID == restID && x.ReviewID == revID).FirstOrDefault();
                    db.Reviews1.Remove(rev);

                    // Update the restaurant's average rating
                    UpdateRestaurantAverageRating(restID);

                    // Save the changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // Update a given Restaurant's average rating
        public void UpdateRestaurantAverageRating(int restID)
        {
            try
            {
                using (db = new RestaurantDBEntities())
                {
                    // All Reviews for the restaurant
                    var rev = db.Reviews1.Where(x => x.RestaurantID == restID).ToList();

                    double rating = 0.0;
                    double averageRating = 0.0;

                    foreach (var r in rev)
                    {
                        rating += db.Reviews.Find(r.ReviewID).Rating;
                    }
                    averageRating = rating / rev.Count();

                    // Update the Restaurant's Average Rating
                    var rest = db.Restaurants.Find(restID);
                    rest.Rating = averageRating;

                    // Save the changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                            averageRating += reviewstable.ElementAt((int)rev.ReviewID - 1).Rating;
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
                RestaurantID = restaurant.RestaurantID,
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
