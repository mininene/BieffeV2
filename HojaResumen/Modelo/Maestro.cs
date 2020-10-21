using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Modelo
{
    public class Maestro
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }
        public string IP { get; set; }
        public string Seccion { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string UltimoCiclo { get; set; }
        public string RutaSalida { get; set; }
    }
}
