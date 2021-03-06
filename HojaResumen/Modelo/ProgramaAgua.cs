﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Modelo
{
    public class ProgramaAgua
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
        public Nullable<double> DuracionF4 { get; set; }
        public Nullable<double> DuracionF5 { get; set; }
        public Nullable<double> PInicialF5 { get; set; }
        public Nullable<double> DuracionF6 { get; set; }
        public Nullable<double> PInicialF6 { get; set; }
        public Nullable<double> TE2IF6 { get; set; }
        public Nullable<double> TE3IF6 { get; set; }
        public Nullable<double> TE4IF6 { get; set; }
        public Nullable<double> PFinalF6 { get; set; }
        public Nullable<double> TE2FF6 { get; set; }
        public Nullable<double> TE3FF6 { get; set; }
        public Nullable<double> TE4FF6 { get; set; }
        public Nullable<double> FoTE2FF6 { get; set; }
        public Nullable<double> FoTE3FF6 { get; set; }
        public Nullable<double> FoTE4FF6 { get; set; }
        public Nullable<double> TMinimaEst { get; set; }
        public Nullable<double> TMaximaEst { get; set; }
        public Nullable<double> DuracionF7 { get; set; }
        public Nullable<double> PFinalF7 { get; set; }
        public Nullable<double> DuracionF8 { get; set; }
        public Nullable<double> DuracionF9 { get; set; }
        public Nullable<double> DuracionF10 { get; set; }
        public Nullable<double> DuracionF11 { get; set; }
        public Nullable<double> DuracionF12 { get; set; }
        public Nullable<double> TTotal { get; set; }
        public Nullable<double> TCalculado { get; set; }
        public Nullable<double> FoTE2FF13 { get; set; }
        public Nullable<double> FoTE3FF13 { get; set; }
        public Nullable<double> FoTE4FF13 { get; set; }
        public Nullable<double> DifFoMaxFoMin { get; set; }
    }
}
