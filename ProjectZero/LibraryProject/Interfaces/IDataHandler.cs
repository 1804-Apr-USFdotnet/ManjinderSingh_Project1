using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjectZero.Libraries.Classes;

namespace ProjectZero.Libraries.Interfaces
{
    interface IDataHandler
    {
        
        // Read data from source
        List<Restaurant> Read();

        // Write data to source
        void Write(List<Restaurant> restaurants);
    }
}
