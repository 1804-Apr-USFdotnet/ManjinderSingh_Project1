using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectZero.Libraries.Interfaces;
using NLog;

namespace ProjectZero.Libraries.Classes
{
    class FileLogger : Interfaces.ILogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
