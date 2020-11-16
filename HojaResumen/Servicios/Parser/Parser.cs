using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Output;
using HojaResumen.Servicios.PrinterEx;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
//using HojaResumen.Modelo.BaseDatos;
//using HojaResumen.Modelo.BaseDatosT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Parser
{
    public class Parser : IParser
    {
        ILog _log = new ProductionLog();
        string impresora = "AdmiCopy_Local";
        IPrinterExc _p = new PrinterExc();
       
        public void ParserFile()
        {
            try
            {
                //string path = @"C:\Users\fuenteI3\Desktop\API\AutoClaveB\8388B24207.LOG";
                //string path = @"C:\Users\fuenteI3\Desktop\RegistrosAutoclaves\AutoClaveIP81000.txt";
                string Programa = "PROGRAMA";
                string Programador = "PROGRAMAD.";
                string Operador = "OPERADOR";
                string CodigoP = " CODIGO PROD.";
                string Lote = "N.LOTE";
                string IdMaquina = "ID.MAQUINA";
                string Notas = "NOTAS";
                string Modelo = " MODELO";
                string ProgresivoN = " N. PROGRESIVO";
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
                string EsterF = " ESTERILIZACION FALLIDA"; //11/11/2020 ´ñadido
                string TMin = " TEMP.MIN.ESTERILIZACION";
                string Tmax = " TEMP.MAX.ESTERILIZACION";
                string DFE = " DURACION FASE DE ESTER.";
                string Fmin = " F(T,z) MIN.:";
                string Fmax = " F(T,z) MAX.:";
                string ok = "OK APERTURA";


                using (var context = new CicloAutoclave()) //entidad de data entity

                {


                    using (var dr = new CicloAutoclave()) //Genero nuevo contexto para que no falle y me salto la captura del error

                    {


                        foreach (var n in dr.MaestroAutoclave)
                        {

                            if (n.Estado == true)
                            {
                                if (n.Matricula.Trim().Equals("NF8387A") || n.Matricula.Trim().Equals("8388B") || n.Matricula.Trim().Equals("8389C") || n.Matricula.Trim().Equals("8607D")
                                    || n.Matricula.Trim().Equals("NF1029E") || n.Matricula.Trim().Equals("NF1030F") || n.Matricula.Trim().Equals("NF1031G") || n.Matricula.Trim().Equals("NA0658EGH")
                                    || n.Matricula.Trim().Equals("NA0672EGI") || n.Matricula.Trim().Equals("NA0611EFM"))
                                {



                                    var actual = Regex.Replace(n.UltimoCiclo, "\\d+",
                               m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length))); // le resto uno al valor del ciclo actual y obtengo el anterior
                                    string ciclo = n.Matricula.Trim() + actual + ".LOG";
                                    string path = n.RutaSalida.Trim() + ciclo;


                                    if (File.Exists(path))
                                    {
                                        _log.WriteLog(ciclo + " " + "Archivo existente");

                                        string[] con = File.ReadAllLines(path, new UnicodeEncoding());


                                        var RegistroEncabezado = new List<string>();
                                        var RegistroProgresivo = new List<string>();
                                        var RegistroModelo = new List<string>();
                                        var RegistroCiclos = new List<string>();
                                        var RegistroDatosFF = new List<string>();
                                        var RegistroTF = new List<string>();
                                        var RegistroTFSub = new List<string>();
                                        var RegistroPie = new List<string>();
                                        var RegistroAlarma = new List<string>();
                                        var Alarmas = new List<string>();


                                        var texts = File.ReadAllLines(path, new UnicodeEncoding());

                                        RegistroEncabezado = texts.Where(lines => lines.Contains(IdMaquina) || (lines.Contains(Programa)) || (lines.Contains(Programador)
                                        || (lines.Contains(Operador)) || (lines.Contains(CodigoP)) || (lines.Contains(Lote)) || (lines.Contains(Modelo) || (lines.Contains(ProgresivoN))))).ToList();

                                        RegistroProgresivo = (texts.Where(lines => lines.Contains(ProgresivoN))).ToList();
                                        RegistroModelo = (texts.Where(lines => lines.Contains(Modelo))).ToList();

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

                                        RegistroPie = texts.Where(lines => lines.Contains(HoraI) || (lines.Contains(HoraF)) || (lines.Contains(EsterN) || (lines.Contains(EsterF)
                                       || (lines.Contains(TMin)) || (lines.Contains(Tmax)) || (lines.Contains(DFE)) || (lines.Contains(Fmin) || (lines.Contains(Fmax) || (lines.Contains(ok))))))).ToList();



                                        RegistroAlarma = texts.Where(lines => lines.StartsWith("*")).ToList();




                                        //Console.WriteLine(combin);


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


                                        // RegistroPie.ForEach(r => Console.WriteLine(r.ToArray()));

                                        //Console.WriteLine((TimeSpan.Parse(RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21))));



                                        List<ProgramaSabiUno> RegistroFinal = new List<ProgramaSabiUno>(); //declaro la lista que quiero cargarle
                                        try
                                        {
                                            string combin = string.Join("\n", RegistroAlarma);

                                            string spanTC = RegistroCiclos[63].Substring(2, 6).ToString().Trim();

                                            var td = TimeSpan.FromMinutes(Convert.ToDouble(spanTC.Split(':')[0])).Add(TimeSpan.FromSeconds(Convert.ToDouble((spanTC.Split(':')[1]))))
                                             - TimeSpan.Parse("00:" + RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21));             // resta timeSpan TFinalCiclo13 - Carga de Agua

                                            string TCicloCalculado = TimeSpan.Parse(td.ToString().Substring(0, 5)).TotalMinutes + td.ToString().Substring(5, 3).Trim();


                                            var Tinicio = RegistroPie[0].Substring(19, 12).Trim() + "  " + TimeSpan.Parse(RegistroPie[0].Substring(30).Trim()).Add(TimeSpan.Parse("00:" + RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21))
                                             + TimeSpan.Parse("00:" + RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse("00:" + RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21))).ToString();

                                            //// string Max = RegistroPie[7].Replace(" ", String.Empty).Substring(11, 4);
                                            // //string Min = RegistroPie[6].Replace(" ", String.Empty).Substring(11, 4);
                                            string Max = RegistroPie[7].Substring(22, 4).Trim();  //cambiado el 10/711/2020 
                                            string Min = RegistroPie[6].Substring(22, 4).Trim();

                                            var Dif = (Convert.ToDecimal(Max, CultureInfo.GetCultureInfo("en-US")) - Convert.ToDecimal(Min, CultureInfo.GetCultureInfo("en-US"))).ToString().Replace(",", ".");



                                            ProgramaSabiUno row = new ProgramaSabiUno
                                            {
                                                IdAutoclave = RegistroEncabezado[5].Replace(" ", String.Empty).Substring(10).Trim(),
                                                IdSeccion = texts[3].Trim(),
                                                IdUsuario = "SabiUno",
                                                TInicio = Tinicio, //Hora Inicio +f2+f3+f4
                                                                   // // NumeroCiclo = RegistroEncabezado[7].Replace(" ", String.Empty).Substring(12).Trim(),  ////problema 24231 autoB 11/11/2020
                                                NumeroCiclo = RegistroProgresivo[0].Replace(" ", String.Empty).Substring(12).Trim(),  ////problema 24231 autoB 11/11/2020
                                                Programa = RegistroEncabezado[0].Replace(" ", String.Empty).Substring(8).Trim(),
                                                //Modelo = RegistroEncabezado[6].Replace(" ", String.Empty).Substring(6).Trim(),  //modelo
                                                Modelo = RegistroModelo[0].Replace(" ", String.Empty).Substring(6).Trim(),  //modelo
                                                Programador = RegistroEncabezado[1].Substring(12).Trim(),
                                                Operador = RegistroEncabezado[2].Substring(10).Trim(),
                                                CodigoProducto = RegistroEncabezado[3].Replace(" ", String.Empty).Substring(11).Trim(),
                                                Lote = RegistroEncabezado[4].Replace(" ", String.Empty).Substring(6).Trim(),
                                                Notas = texts[12] + texts[13] + texts[14],

                                                Fase1 = RegistroCiclos[1].Substring(12).Trim(),
                                                Fase2 = RegistroCiclos[6].Substring(12).Trim(),
                                                Fase3 = RegistroCiclos[11].Substring(12).Trim(),
                                                Fase4 = RegistroCiclos[16].Substring(12).Trim(),
                                                Fase5 = RegistroCiclos[21].Substring(12).Trim(),
                                                Fase6 = RegistroCiclos[26].Substring(12).Trim(),
                                                Fase7 = RegistroCiclos[31].Substring(12).Trim(),
                                                Fase8 = RegistroCiclos[36].Substring(12).Trim(),
                                                Fase9 = RegistroCiclos[41].Substring(12).Trim(),
                                                Fase10 = RegistroCiclos[46].Substring(12).Trim(),
                                                Fase11 = RegistroCiclos[51].Substring(12).Trim(),
                                                Fase12 = RegistroCiclos[56].Substring(12).Trim(),
                                                Fase13 = RegistroCiclos[61].Substring(12).Trim(),  //revisar

                                                TIF5 = RegistroCiclos[23].Substring(2).Trim(),
                                                TIF6 = RegistroCiclos[28].Substring(2).Trim(),
                                                //  TIF7 = RegistroCiclos[33],
                                                TIF8 = RegistroCiclos[38].Substring(2).Trim(),
                                                TIF9 = RegistroCiclos[43].Substring(2).Trim(),
                                                TFF13 = RegistroCiclos[63].Substring(2).Trim(),   //revisar

                                                TISubF5 = RegistroCiclos[24].Substring(14).Trim(),
                                                TISubF6 = RegistroCiclos[29].Substring(14).Trim(),
                                                //  TISubF7 = RegistroCiclos[34],
                                                TISubF8 = RegistroCiclos[39].Substring(14).Trim(),
                                                TISubF9 = RegistroCiclos[44].Substring(14).Trim(),
                                                TFSubF13 = RegistroCiclos[64].Substring(14).Trim(),

                                                DuracionTotalF1 = RegistroDatosFF[0].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF2 = RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF3 = RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF4 = RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF5 = RegistroDatosFF[12].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF6 = RegistroDatosFF[15].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF7 = RegistroDatosFF[18].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF8 = RegistroDatosFF[21].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF9 = RegistroDatosFF[24].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF10 = RegistroDatosFF[27].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF11 = RegistroDatosFF[30].Replace(" ", String.Empty).Substring(21).Trim(),
                                                DuracionTotalF12 = RegistroDatosFF[33].Replace(" ", String.Empty).Substring(21).Trim(),


                                                TFF5 = RegistroTF[1].Substring(2).Trim(),//cambiado el 06/11/2020
                                                TFF6 = RegistroCiclos[33].Substring(2).Trim(),//Es el inicio de fase 7 TIF7
                                                TIF7 = RegistroTF[3].Substring(2).Trim(),  // TIF7 realmente es TFF7 // cambiado 06/11/2020
                                                                                           //TFF8 = RegistroDatosFF[26],
                                                TFF8 = RegistroCiclos[38].Substring(2).Trim(), //TFF8 = TIF8  //cambiado 06/11/2020
                                                TFF9 = RegistroDatosFF[26].Substring(2).Trim(), //cambie 29 a 25  06/11/2020



                                                TFSubF5 = RegistroTFSub[1].Substring(14).Trim(),
                                                TISubF7 = RegistroTFSub[3].Substring(14).Trim(), // TISubF7 es TFSubF7 
                                                TFSubF6 = RegistroCiclos[34].Substring(14).Trim(),  //Inicio Sbzero de la Fase siete TISubF7


                                                HoraInicio = RegistroPie[0].Substring(22).Trim(),
                                                HoraFin = RegistroPie[1].Substring(22).Trim(),
                                                EsterilizacionN = RegistroPie[2].Substring(26).Trim(),
                                                TMinima = RegistroPie[3].Replace(" ", String.Empty).Substring(25).Trim(),
                                                TMaxima = RegistroPie[4].Replace(" ", String.Empty).Substring(25).Trim(),
                                                DuracionTotal = RegistroPie[5].Replace(" ", String.Empty).Substring(25).Trim(),
                                                FtzMin = RegistroPie[6].Replace(" ", String.Empty).Substring(11).Trim(),
                                                FtzMax = RegistroPie[7].Replace(" ", String.Empty).Substring(11).Trim(),
                                                DifMaxMin = Dif,
                                                AperturaPuerta = RegistroPie[8].Trim(),
                                                TiempoCiclo = TCicloCalculado,
                                                FechaRegistro = DateTime.Now,

                                                ErrorCiclo = combin,

                                            }; RegistroFinal.Add(row); //añado elementos
                                        }
                                        catch (Exception e)
                                        {
                                            //Console.WriteLine(path +"  "+"Los campos No cumplen con el modelo");
                                            _log.WriteLog(ciclo + "  " + "El archivo contiene errores de origen Debe ser impreso Manualmente");
                                            //Console.WriteLine(e.Message.ToString());
                                            //Console.WriteLine(e.StackTrace);
                                            //Console.WriteLine(e.Source);
                                            //_log.WriteLog(ciclo + "  " + "Imprimiendo recordatorio de impresion manual");
                                            //_p.PrinterExc(ciclo + "  " + "Debe ser impreso Manualmente", impresora);


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
                                            ciclos.DifMaxMin = s.DifMaxMin;
                                            ciclos.AperturaPuerta = s.AperturaPuerta;
                                            ciclos.TiempoCiclo = s.TiempoCiclo;
                                            ciclos.ErrorCiclo = s.ErrorCiclo;
                                            ciclos.FechaRegistro = s.FechaRegistro;

                                        }
                                        var duplicado = context.CiclosAutoclaves.Count(a => a.NumeroCiclo == ciclos.NumeroCiclo && a.IdAutoclave == ciclos.IdAutoclave);


                                        // Console.WriteLine(control);
                                        //Console.WriteLine("DATOS SABI UNO------------------------------------------------------------------------");

                                        try
                                        {
                                            if (ciclos.Programa != null)
                                            {
                                                if (ciclos.Programa.Trim().Equals("2") || ciclos.Programa.Trim().Equals("3") || ciclos.Programa.Trim().Equals("4") || ciclos.Programa.Trim().Equals("8") || ciclos.Programa.Trim().Equals("20"))
                                                {
                                                    if (duplicado == 0)
                                                    {
                                                        context.CiclosAutoclaves.Add(ciclos);
                                                        context.SaveChanges();
                                                    }
                                                    else { /*Console.WriteLine("Registros Duplicados y Registros JKL");*/ }

                                                }
                                                else { _log.WriteLog("Pertenece a otro programa" + ciclos.IdAutoclave + ciclos.NumeroCiclo); }
                                            }

                                        }
                                        catch
                                        {
                                            _log.WriteLog(ciclo + "  " + "El archivo contiene errores de origen no puede ser resumido, debe ser impreso desde el Autoclave");
                                        }




                                        System.Threading.Thread.Sleep(2000);

                                    }
                                    else { _log.WriteLog(ciclo + " " + "El archivo todavia No ha sido generado por el Autoclave"); }
                                }
                                else { /*_log.WriteLog("");*/ }

                                // catch del nuevo contexto

                            }

                        }
                    }


                }
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

        }
    }
}
