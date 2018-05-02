using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Libraries.Classes
{
    public class Reviews
    {
        private int _id;
        private int _reviewID;
        private Nullable<int> _reviews;

        public int ID { get { return _id; } set { _id = value; } }
        public int ReviewID { get { return _reviewID; } set { _reviewID = value; } }
        public Nullable<int> Revs { get { return _reviews; } set { _reviews = value; } }
    }
}
