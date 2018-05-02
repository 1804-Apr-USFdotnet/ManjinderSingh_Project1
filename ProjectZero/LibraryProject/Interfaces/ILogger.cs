using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ProjectZero.Libraries.Interfaces
{
    interface ILogger
    {
        void Warning(string message);
        void Error(string message);
        void Fatal(string message);

    }
}
