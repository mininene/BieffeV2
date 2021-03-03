using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.LogRecord
{
    public interface ILogRecord
    {
        void Writer(string Usuario, DateTime fechaHora, string Evento, string comentario);
    }
}
