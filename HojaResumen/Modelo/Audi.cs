using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Modelo
{
    public class Audi
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Tabla { get; set; }
        public DateTime? FechaHora { get; set; }
        public string Evento { get; set; }
        public string Campo { get; set; }
        public string Valor { get; set; }
        public string ValorActualizado { get; set; }
        public string Comentario { get; set; }
        public DateTime? Tiempo { get; set; }
    }
}
