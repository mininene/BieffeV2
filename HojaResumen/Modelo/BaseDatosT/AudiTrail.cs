//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HojaResumen.Modelo.BaseDatosT
{
    using System;
    using System.Collections.Generic;
    
    public partial class AudiTrail
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Tabla { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string Evento { get; set; }
        public string Campo { get; set; }
        public string Valor { get; set; }
        public string ValorActualizado { get; set; }
        public string Comentario { get; set; }
        public Nullable<System.DateTime> Tiempo { get; set; }
    }
}