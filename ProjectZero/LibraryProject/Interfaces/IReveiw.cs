using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Libraries.Interfaces
{
    interface IReview
    {
        int ReviewID { get; set; }
        string Author { get; set; }
        int Rating { get; set; }
    }
}
