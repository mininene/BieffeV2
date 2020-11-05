using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.InfraestructuraTransversal.ServicesException
{
    class ParserException:Exception
    {
        public ParserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
