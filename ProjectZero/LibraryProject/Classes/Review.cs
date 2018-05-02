using ProjectZero.Libraries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Libraries.Classes
{
    public class Review : IReview, IComparable<Review>
    {
        private int _id;            // Review ID
        private string _author;     // Author of the review
        private int _rating;    // Given rating

        public int ReviewID { get { return _id; } set { _id = value; } }
        public string Author { get { return _author; } set { _author = value; } }
        public int Rating { get { return _rating; } set { _rating = value; } }

        public Review()
        {
            _id = 0;
            _author = "";
            _rating = 0;
        }

        public Review(int id, string author, int rating)
        {
            _id = id;
            _author = author;
            _rating = rating;
        }

        // This method is called by List().Sort() method to sort the list into Ascending Order
        public int CompareTo(Review other)
        {
            if (other == null)
                return 1;
            else
                return Rating.CompareTo(other.Rating);
        }

        public override string ToString()
        {
            StringBuilder revString = new StringBuilder();

            revString.Append(Author + " ");
            revString.Append(Rating.ToString());

            return revString.ToString();
        }
    }
}
