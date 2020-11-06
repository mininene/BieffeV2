using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Printer
{
    public interface IPrint
    {
        void Printer(string archivo, string impresora);
    }
}
