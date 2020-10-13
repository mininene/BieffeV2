using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Modelo
{
    public class ConnectionData
    {
        public int Version { get; set; }
        public string Ip { get; set; }
        public string Remote { get; set; }
        public string Local { get; set; }
    }
}
