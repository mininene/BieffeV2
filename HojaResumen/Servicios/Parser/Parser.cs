using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
//using HojaResumen.Modelo.BaseDatos;
//using HojaResumen.Modelo.BaseDatosT;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Parser
{
    public class Parser : IParser
    {
        public void ParserFile()
        {
            try
            {

                string path = @"C:\Users\fuenteI3\Desktop\RegistrosAutoclaves\AutoClaveAP8500.txt";
                string Programa = "PROGRAMA";
                string Programador = "PROGRAMAD.";
                string Operador = "OPERADOR";
                string CodigoP = " CODIGO PROD.";
                string Lote = "N.LOTE";
                string IdMaquina = "ID.MAQUINA";
                string Notas = "NOTAS";
                string Modelo = "MODELO";
                string ProgresivoN = "N. PROGRESIVO";
                string FaseUno = "FASE    1";
                string FaseDos = "FASE    2";
                string FaseTres = "FASE    3";
                string FaseCuatro = "FASE    4";
                string FaseCinco = "+FASE    5";
                string FaseSeis = "+FASE    6";
                string FaseSiete = "+FASE    7";
                string FaseOcho = "+FASE    8";
                string FaseNueve = "+FASE    9";
                string FaseDiez = "+FASE   10";
                string FaseOnce = "+FASE   11";
                string FaseDoce = "+FASE   12";
                string FaseTrece = "+FASE    13";
                string DuracionFases = "$DURAC.TOTAL FASE";
                string TiempoFases = "+";


                string HoraI = " HORA COMIEN.PROGR.";
                string HoraF = " HORA FIN PROGRAMA";
                string EsterN = " ESTERILIZACION N.";
                string TMin = " TEMP.MIN.ESTERILIZACION";
                string Tmax = " TEMP.MAX.ESTERILIZACION";
                string DFE = " DURACION FASE DE ESTER.";
                string Fmin = " F(T,z) MIN.:";
                string Fmax = " F(T,z) MAX.:";
                string ok = "OK APERTURA";


                using (var context = new CicloAutoclave()) //entidad de data entity

                {

                    string[] con = File.ReadAllLines(path, new UnicodeEncoding());

                    var RegistroEncabezado = new List<string>();
                    var RegistroCiclos = new List<string>();
                    var RegistroDatosFF = new List<string>();
                    var RegistroTF = new List<string>();
                    var RegistroTFSub = new List<string>();
                    var RegistroPie = new List<string>();
                    var RegistroAlarma = new List<string>();


                    var texts = File.ReadAllLines(path, new UnicodeEncoding());

                    RegistroEncabezado = texts.Where(lines => lines.Contains(IdMaquina) || (lines.Contains(Programa)) || (lines.Contains(Programador)
                    || (lines.Contains(Operador)) || (lines.Contains(CodigoP)) || (lines.Contains(Lote)) || (lines.Contains(Modelo) || (lines.Contains(ProgresivoN))))).ToList();


                    RegistroCiclos = texts.Where(lines => lines.StartsWith("+")).ToList();

                    RegistroDatosFF = texts.Where(lines => lines.StartsWith("$")).ToList();


                    for (int i = 0; i < con.Length; i++)
                    {
                        if (con[i].Contains(FaseCinco) || con[i].Contains(FaseSeis) || con[i].Contains(FaseSiete)
                            || con[i].Contains(FaseOcho) || con[i].Contains(FaseNueve) || con[i].Contains(FaseTrece) && i >= 0)
                        {
                            string TF = con[i - 6];
                            string TFo = con[i - 5];
                            //Console.WriteLine(res);
                            // Console.WriteLine(fo);
                            RegistroTF.Add(TF);
                            RegistroTFSub.Add(TFo);
                        }
                    }

                    RegistroPie = texts.Where(lines => lines.Contains(HoraI) || (lines.Contains(HoraF)) || (lines.Contains(EsterN)
                   || (lines.Contains(TMin)) || (lines.Contains(Tmax)) || (lines.Contains(DFE)) || (lines.Contains(Fmin) || (lines.Contains(Fmax) || (lines.Contains(ok)))))).ToList();



                    RegistroAlarma = texts.Where(lines => lines.StartsWith("*")).ToList();
                   

                    //for (int i = 0; i < con.Length; i++)
                    //{
                    //    if (con[i].Contains("+FASE    5") && i >= 0)
                    //    {
                    //        string g = con[i]+"\n" +  con[i+1]+"\n" + con[i+2];
                    //        //string TFo = con[i - 5];
                    //        //Console.WriteLine(g);
                    //        // Console.WriteLine(fo);
                    //        RegistroCiclo1.Add(g);
                    //        //RegistroTFSub.Add(TFo);
                    //    }
                    //}


                    //RegistroAlarma.ForEach(r => Console.WriteLine(r.ToArray()));
                    //Console.WriteLine((TimeSpan.Parse(RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21))));




                    List<ProgramaSabiUno> RegistroFinal = new List<ProgramaSabiUno>(); //declaro la lista que quiero cargar
                    try
                    {
                        ProgramaSabiUno row = new ProgramaSabiUno
                        {
                            IdAutoclave = RegistroEncabezado[5],
                            IdSeccion = RegistroPie[0].Replace(" ", String.Empty).Substring(25),
                            TInicio = "",
                            NumeroCiclo = RegistroEncabezado[7],
                            Programa = RegistroEncabezado[0],
                            Modelo = RegistroEncabezado[6],  //modelo
                            Programador = RegistroEncabezado[1],
                            Operador = RegistroEncabezado[2],
                            CodigoProducto = RegistroEncabezado[3],
                            Lote = RegistroEncabezado[4],
                            Notas = texts[12] + texts[13],

                            Fase1 = RegistroCiclos[1],
                            Fase2 = RegistroCiclos[6],
                            Fase3 = RegistroCiclos[11],
                            Fase4 = RegistroCiclos[16],
                            Fase5 = RegistroCiclos[21],
                            Fase6 = RegistroCiclos[26],
                            Fase7 = RegistroCiclos[31],
                            Fase8 = RegistroCiclos[36],
                            Fase9 = RegistroCiclos[41],
                            Fase10 = RegistroCiclos[46],
                            Fase11 = RegistroCiclos[51],
                            Fase12 = RegistroCiclos[56],
                            Fase13 = RegistroCiclos[61],  //revisar

                            TIF5 = RegistroCiclos[23],
                            TIF6 = RegistroCiclos[28],
                            //  TIF7 = RegistroCiclos[33],
                            TIF8 = RegistroCiclos[38],
                            TIF9 = RegistroCiclos[43],
                            TFF13 = RegistroCiclos[63],   //revisar

                            TISubF5 = RegistroCiclos[24],
                            TISubF6 = RegistroCiclos[29],
                            //  TISubF7 = RegistroCiclos[34],
                            TISubF8 = RegistroCiclos[39],
                            TISubF9 = RegistroCiclos[44],
                            TFSubF13 = RegistroCiclos[64],

                            DuracionTotalF1 = RegistroDatosFF[0],
                            DuracionTotalF2 = RegistroDatosFF[3],
                            DuracionTotalF3 = RegistroDatosFF[6],
                            DuracionTotalF4 = RegistroDatosFF[9],
                            DuracionTotalF5 = RegistroDatosFF[12],
                            DuracionTotalF6 = RegistroDatosFF[15],
                            DuracionTotalF7 = RegistroDatosFF[18],
                            DuracionTotalF8 = RegistroDatosFF[21],
                            DuracionTotalF9 = RegistroDatosFF[24],
                            DuracionTotalF10 = RegistroDatosFF[27],
                            DuracionTotalF11 = RegistroDatosFF[30],
                            DuracionTotalF12 = RegistroDatosFF[33],


                            TFF5 = RegistroTF[1],
                            TFF6 = RegistroCiclos[33],//Es el inicio de fase 7 TIF7
                            TIF7 = RegistroTF[3],  // TIF7 realmente es TFF7
                            //TFF8 = RegistroDatosFF[26],
                            TFF8 = RegistroCiclos[38], //TFF8 = TIF8
                            TFF9 = RegistroDatosFF[29],


                            TFSubF5 = RegistroTFSub[1],
                            TISubF7 = RegistroTFSub[3], // TISubF7 es TFSubF7
                            TFSubF6 = RegistroCiclos[34],  //Inicio Sbzero de la Fase siete TISubF7


                            HoraInicio = RegistroPie[0],
                            HoraFin = RegistroPie[1],
                            EsterilizacionN = RegistroPie[2],
                            TMinima = RegistroPie[3],
                            TMaxima = RegistroPie[4],
                            DuracionTotal = RegistroPie[5],
                            FtzMin = RegistroPie[6],
                            FtzMax = RegistroPie[7],
                            AperturaPuerta = RegistroPie[8],
                            TiempoCiclo = "h", //+ Convert.ToDateTime(TimeSpan.Parse(RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21))).ToString(),
                            //(TimeSpan.Parse(RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21)) +TimeSpan.Parse(RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21))).ToString() ,
                            //Convert.ToDateTime(RegistroPie[0].Replace(" ", String.Empty).Substring(25))
                            //Convert.ToDateTime(RegistroPie[0].Replace(" ", String.Empty).Substring(25)).ToString(),
                            ErrorCiclo= "" ,

                        }; RegistroFinal.Add(row); //añado elementos
                    }
                    catch
                    {
                        Console.WriteLine("Los campos No cumplen con el modelo");
                    }


                    var ciclos = new CiclosAutoclaves();// Instancio objeto Ciclos

                    foreach (var s in RegistroFinal)  //recorro la lista
                    {


                        ciclos.IdAutoclave = s.IdAutoclave;
                        ciclos.IdSeccion = s.IdSeccion;
                        ciclos.TInicio = s.TInicio;
                        ciclos.NumeroCiclo = s.NumeroCiclo;
                        ciclos.Programa = s.Programa;
                        ciclos.Modelo = s.Modelo;
                        ciclos.Programador = s.Programador;
                        ciclos.Operador = s.Operador;
                        ciclos.CodigoProducto = s.CodigoProducto;
                        ciclos.Lote = s.Lote;
                        ciclos.Notas = s.Notas;
                        ciclos.IdUsuario = s.IdUsuario;
                        ciclos.Fase1 = s.Fase1;
                        ciclos.DuracionTotalF1 = s.DuracionTotalF1;

                        ciclos.Fase2 = s.Fase2;
                        ciclos.DuracionTotalF2 = s.DuracionTotalF2;

                        ciclos.Fase3 = s.Fase3;
                        ciclos.DuracionTotalF3 = s.DuracionTotalF3;

                        ciclos.Fase4 = s.Fase4;
                        ciclos.DuracionTotalF4 = s.DuracionTotalF4;

                        ciclos.Fase5 = s.Fase5;
                        ciclos.DuracionTotalF5 = s.DuracionTotalF5;
                        ciclos.TIF5 = s.TIF5;
                        ciclos.TISubF5 = s.TISubF5;
                        ciclos.TFF5 = s.TFF5;
                        ciclos.TFSubF5 = s.TFSubF5;


                        ciclos.Fase6 = s.Fase6;
                        ciclos.DuracionTotalF6 = s.DuracionTotalF6;
                        ciclos.TIF6 = s.TIF6;
                        ciclos.TISubF6 = s.TISubF6;
                        ciclos.TFF6 = s.TFF6;
                        ciclos.TFSubF6 = s.TFSubF6;

                        ciclos.Fase7 = s.Fase7;
                        ciclos.DuracionTotalF7 = s.DuracionTotalF7;
                        ciclos.TIF7 = s.TIF7;
                        ciclos.TISubF7 = s.TISubF7;

                        ciclos.Fase8 = s.Fase8;
                        ciclos.DuracionTotalF8 = s.DuracionTotalF8;
                        ciclos.TIF8 = s.TIF8;
                        ciclos.TISubF8 = s.TISubF8;
                        ciclos.TFF8 = s.TFF8;

                        ciclos.Fase9 = s.Fase9;
                        ciclos.DuracionTotalF9 = s.DuracionTotalF9;
                        ciclos.TIF9 = s.TIF9;
                        ciclos.TISubF9 = s.TISubF9;
                        ciclos.TFF9 = s.TFF9;

                        ciclos.Fase10 = s.Fase10;
                        ciclos.DuracionTotalF10 = s.DuracionTotalF10;

                        ciclos.Fase11 = s.Fase11;
                        ciclos.DuracionTotalF11 = s.DuracionTotalF11;

                        ciclos.Fase12 = s.Fase12;
                        ciclos.DuracionTotalF12 = s.DuracionTotalF12;

                        ciclos.Fase13 = s.Fase13;
                        ciclos.TFF13 = s.TFF13;
                        ciclos.TFSubF13 = s.TFSubF13;

                        ciclos.HoraInicio = s.HoraInicio;
                        ciclos.HoraFin = s.HoraFin;

                        ciclos.EsterilizacionN = s.EsterilizacionN;
                        ciclos.TMinima = s.TMinima;
                        ciclos.TMaxima = s.TMaxima;
                        ciclos.DuracionTotal = s.DuracionTotal;
                        ciclos.FtzMin = s.FtzMin;
                        ciclos.FtzMax = s.FtzMax;
                        ciclos.AperturaPuerta = s.AperturaPuerta;
                        ciclos.TiempoCiclo = s.TiempoCiclo;
                        ciclos.ErrorCiclo = s.ErrorCiclo;

                    }
                    context.CiclosAutoclaves.Add(ciclos);
                    context.SaveChanges();



                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

        }
    }
}
