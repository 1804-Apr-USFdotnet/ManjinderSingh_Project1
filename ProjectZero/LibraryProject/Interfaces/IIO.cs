using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero.Libraries.Classes;

namespace ProjectZero.Libraries.Interfaces
{
    interface IIO
    {
        string MenuOptions();
        string Result(int input);
        List<Restaurant> Search(string searchString);
    }
}
