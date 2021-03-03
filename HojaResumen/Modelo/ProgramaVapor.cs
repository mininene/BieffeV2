using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Modelo
{
    public class ProgramaVapor
    {
        public int Id { get; set; }
        public string idAutoclave { get; set; }
        public string NCarro { get; set; }
        public string NProgresivo { get; set; }
        public Nullable<int> NPrograma { get; set; }
        public string CodProducto { get; set; }
        public string Lote { get; set; }
        public Nullable<System.DateTime> HoraInicio { get; set; }
        public Nullable<System.DateTime> HoraFin { get; set; }
        public Nullable<double> DuracionF1 { get; set; }
        public Nullable<double> DuracionF2 { get; set; }
        public Nullable<double> DuracionF3 { get; set; }
        public Nullable<double> PInicialF3 { get; set; }
        public Nullable<double> TE2IF3 { get; set; }
        public Nullable<double> TE3IF3 { get; set; }
        public Nullable<double> TE4IF3 { get; set; }
        public Nullable<double> PFinalF3 { get; set; }
        public Nullable<double> TE2FF3 { get; set; }
        public Nullable<double> TE3FF3 { get; set; }
        public Nullable<double> TE4FF3 { get; set; }
        public Nullable<double> FoTE2FF3 { get; set; }
        public Nullable<double> FoTE3FF3 { get; set; }
        public Nullable<double> FoTE4FF3 { get; set; }
        public Nullable<double> TMinimaEst { get; set; }
        public Nullable<double> TMaximaEst { get; set; }
        public Nullable<double> DuracionF4 { get; set; }
        public Nullable<double> DuracionF5 { get; set; }
        public Nullable<double> DuracionF6 { get; set; }
        public Nullable<double> DuracionF7A { get; set; }
        public Nullable<double> DuracionF8A { get; set; }
        public Nullable<double> DuracionF7B { get; set; }
        public Nullable<double> DuracionF8B { get; set; }
        public Nullable<double> DuracionF9 { get; set; }
        public Nullable<double> DuracionF10 { get; set; }
        public Nullable<double> DuracionF11 { get; set; }
        public Nullable<double> TTotal { get; set; }
        public Nullable<double> FoTE2FF12 { get; set; }
        public Nullable<double> FoTE3FF12 { get; set; }
        public Nullable<double> FoTE4FF12 { get; set; }
        public Nullable<double> DifFoMaxFoMin { get; set; }
    }
}
