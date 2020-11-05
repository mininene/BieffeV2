using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.InfraestructuraTransversal.ServicesException
{
    class LogException:Exception
    {
        public LogException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
