using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.InfraestructuraTransversal.ServicesException
{
    class PrintException:Exception
    {
        public PrintException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
