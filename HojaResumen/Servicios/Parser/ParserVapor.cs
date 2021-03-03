using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Output;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Parser
{
    public class ParserVapor : IParserVapor
    {
        ILog _log = new ProductionLog();
        void IParserVapor.ParserVapor()
        {
            try
            {

                // string path = @"C:\Users\fuenteI3\Desktop\original y pdf\AutoClaveLP10.txt";
                string Programa = "PROGRAMA" ;
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
                                if (n.Matricula.Trim().Equals("0827J") || n.Matricula.Trim().Equals("0828K") || n.Matricula.Trim().Equals("1167L"))
                                {




                                    var actual = Regex.Replace(n.UltimoCiclo, "\\d+",
                                   m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length))); // le resto uno al valor del ciclo actual y obtengo el anterior
                                    string ciclo = n.Matricula.Trim() + actual + ".LOG";
                                    string path = n.RutaSalida.Trim() + ciclo;


                                    if (File.Exists(path))
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
                                            if (con[i].Contains(FaseDos) || con[i].Contains(FaseTres) || con[i].Contains(FaseCuatro) || con[i].Contains(FaseCinco) && i >= 0)
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


                                        // Console.WriteLine(combin);


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


                                        //RegistroDatosFF.ForEach(r => Console.WriteLine(r.ToArray()));
                                        //Console.WriteLine((TimeSpan.Parse(RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21)) + TimeSpan.Parse(RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21))));




                                        List<ProgramaVapor> RegistroFinal = new List<ProgramaVapor>(); //declaro la lista que quiero cargar
                                        try
                                        {
                                            string combin = string.Join("\n", RegistroAlarma);
                                            //string Max = RegistroPie[7].Replace(" ", String.Empty).Substring(11, 4);
                                            //string Min = RegistroPie[6].Replace(" ", String.Empty).Substring(11, 4);
                                            string Max = RegistroPie[7].Substring(22, 4).Trim();  //cambiado el 10/711/2020 
                                            string Min = RegistroPie[6].Substring(22, 4).Trim();

                                            var Dif = (Convert.ToDecimal(Max, CultureInfo.GetCultureInfo("en-US")) - Convert.ToDecimal(Min, CultureInfo.GetCultureInfo("en-US"))).ToString().Replace(",", ".");

                                            var F1 = RegistroDatosFF[0].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F2 = RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F3 = RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F4 = RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F5 = RegistroDatosFF[12].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F6 = RegistroDatosFF[15].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F7A = RegistroDatosFF[18].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F7B = RegistroDatosFF[24].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F8A = RegistroDatosFF[21].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F8B = RegistroDatosFF[27].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();

                                            var F9 = RegistroDatosFF[30].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F10 = RegistroDatosFF[33].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F11 = RegistroDatosFF[36].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F12 = RegistroCiclos[68].Substring(2,6).Trim().Split(':').ToArray();
                                           // Console.WriteLine(RegistroCiclos[68].Substring(2,6).Trim());
                                           // var F13 = RegistroCiclos[63].Substring(2, 6).Trim().Split(':').ToArray();
                                           // var Fc = TCicloCalculado.Split(':').ToArray();

                                            var DF1 = Math.Round((int.Parse(F1[0]) + double.Parse(F1[1]) / 60), 2);
                                            var DF2 = Math.Round((int.Parse(F2[0]) + double.Parse(F2[1]) / 60), 2);
                                            var DF3 = Math.Round((int.Parse(F3[0]) + double.Parse(F3[1]) / 60), 2);
                                            var DF4 = Math.Round((int.Parse(F4[0]) + double.Parse(F4[1]) / 60), 2);

                                            var DF5 = Math.Round((int.Parse(F5[0]) + double.Parse(F5[1]) / 60), 2);
                                            var DF6 = Math.Round((int.Parse(F6[0]) + double.Parse(F6[1]) / 60), 2);

                                            var DF7A = Math.Round((int.Parse(F7A[0]) + double.Parse(F7A[1]) / 60), 2);
                                            var DF7B = Math.Round((int.Parse(F7B[0]) + double.Parse(F7B[1]) / 60), 2);
                                            var DF8A = Math.Round((int.Parse(F8A[0]) + double.Parse(F8A[1]) / 60), 2);
                                            var DF8B = Math.Round((int.Parse(F8B[0]) + double.Parse(F8B[1]) / 60), 2);
                                            var DF9 = Math.Round((int.Parse(F9[0]) + double.Parse(F9[1]) / 60), 2);
                                            var DF10 = Math.Round((int.Parse(F10[0]) + double.Parse(F10[1]) / 60), 2);
                                            var DF11 = Math.Round((int.Parse(F11[0]) + double.Parse(F11[1]) / 60), 2);
                                            var DF12 = Math.Round((int.Parse(F12[0]) + double.Parse(F12[1]) / 60), 2);
                                            // var DF13 = Math.Round((int.Parse(F13[0]) + double.Parse(F13[1]) / 60), 2);
                                            // var DF14 = Math.Round((int.Parse(Fc[0]) + double.Parse(Fc[1]) / 60), 2);
                                            Console.WriteLine(RegistroPie[3].Replace(" ", String.Empty).Substring(25, 5).Trim());
                                            

                                            ProgramaVapor row = new ProgramaVapor
                                            {
                                                idAutoclave = RegistroEncabezado[5].Replace(" ", String.Empty).Substring(10).Trim(),
                                                NProgresivo = RegistroEncabezado[7].Replace(" ", String.Empty).Substring(12).Trim(),
                                                NPrograma = Convert.ToInt32(RegistroEncabezado[0].Replace(" ", String.Empty).Substring(8).Trim()),
                                               
                                                CodProducto = RegistroEncabezado[3].Replace(" ", String.Empty).Substring(11).Trim(),
                                                Lote = RegistroEncabezado[4].Replace(" ", String.Empty).Substring(6).Trim(),
                                                HoraInicio= Convert.ToDateTime(RegistroPie[0].Substring(22).Trim()),
                                                HoraFin= Convert.ToDateTime(RegistroPie[1].Substring(22).Trim()),
                                                NCarro = texts[12] + texts[13] + texts[14],

                                                DuracionF1 = DF1,
                                                DuracionF2 = DF2,
                                                DuracionF3 = DF3,
                                                DuracionF4 = DF4,
                                                DuracionF5 = DF5,
                                                DuracionF6 = DF6,
                                                DuracionF7A = DF7A,
                                                DuracionF7B = DF7B,
                                                DuracionF8A = DF8A,
                                                DuracionF8B = DF8B,
                                                DuracionF9 = DF9,
                                                DuracionF10 = DF10,
                                                DuracionF11 = DF11,
                                                TTotal = DF12,

                                                PInicialF3 = double.Parse(RegistroCiclos[13].Substring(2).Trim().Substring(8, 5).Trim(), CultureInfo.InvariantCulture),
                                                TE2IF3 = double.Parse(RegistroCiclos[13].Substring(2).Trim().Substring(14, 5), CultureInfo.InvariantCulture),
                                                TE3IF3 = double.Parse(RegistroCiclos[13].Substring(2).Trim().Substring(21, 5), CultureInfo.InvariantCulture),
                                                TE4IF3 = double.Parse(RegistroCiclos[13].Substring(2).Trim().Substring(28, 5), CultureInfo.InvariantCulture),

                                                PFinalF3 = double.Parse(RegistroCiclos[18].Substring(2).Trim().Substring(8, 5).Trim(), CultureInfo.InvariantCulture),
                                                TE2FF3 = double.Parse(RegistroCiclos[18].Substring(2).Trim().Substring(14, 5), CultureInfo.InvariantCulture),
                                                TE3FF3 = double.Parse(RegistroCiclos[18].Substring(2).Trim().Substring(21, 5), CultureInfo.InvariantCulture),
                                                TE4FF3 = double.Parse(RegistroCiclos[18].Substring(2).Trim().Substring(28, 5), CultureInfo.InvariantCulture),

                                                FoTE2FF3 = double.Parse(RegistroCiclos[19].Substring(15).Trim().Substring(0, 5).Trim(), CultureInfo.InvariantCulture),
                                                FoTE3FF3 = double.Parse(RegistroCiclos[19].Substring(15).Trim().Substring(5, 9).Trim(), CultureInfo.InvariantCulture),
                                                FoTE4FF3 = double.Parse(RegistroCiclos[19].Substring(15).Trim().Substring(11, 7).Trim(), CultureInfo.InvariantCulture),

                                                TMinimaEst = double.Parse(RegistroPie[3].Replace(" ", String.Empty).Substring(25, 5).Trim(), CultureInfo.InvariantCulture),
                                                TMaximaEst = double.Parse(RegistroPie[4].Replace(" ", String.Empty).Substring(25, 5).Trim(), CultureInfo.InvariantCulture),

                                                FoTE2FF12 = double.Parse(RegistroCiclos[69].Substring(15).Trim().Substring(0, 5).Trim(), CultureInfo.InvariantCulture),
                                                FoTE3FF12 = double.Parse(RegistroCiclos[69].Substring(15).Trim().Substring(5, 9).Trim(), CultureInfo.InvariantCulture),
                                                FoTE4FF12 = double.Parse(RegistroCiclos[69].Substring(15).Trim().Substring(11, 7).Trim(), CultureInfo.InvariantCulture),

                                                DifFoMaxFoMin = double.Parse(Dif, CultureInfo.InvariantCulture)
                                              


                                            }; RegistroFinal.Add(row); //añado elementos
                                        }
                                        catch
                                        {
                                            _log.WriteLog("FallaParserVapor" + " " + ciclo);

                                        }


                                        var ciclos = new Vapor();// Instancio objeto Ciclos

                                        foreach (var s in RegistroFinal)  //recorro la lista
                                        {


                                            ciclos.idAutoclave = s.idAutoclave;
                                            ciclos.NProgresivo = s.NProgresivo;
                                            ciclos.NPrograma = s.NPrograma;
                                        
                                            ciclos.CodProducto = s.CodProducto;
                                            ciclos.Lote = s.Lote;
                                            ciclos.HoraInicio = s.HoraInicio;
                                            ciclos.HoraFin = s.HoraFin;
                                            ciclos.NCarro = s.NCarro;

                                            ciclos.DuracionF1  =s.DuracionF1 ;
                                            ciclos.DuracionF2  =s.DuracionF2 ;
                                            ciclos.DuracionF3  =s.DuracionF3 ;
                                            ciclos.DuracionF4  =s.DuracionF4 ;
                                            ciclos.DuracionF5  =s.DuracionF5 ;
                                            ciclos.DuracionF6  =s.DuracionF6 ;
                                            ciclos.DuracionF7A =s.DuracionF7A;
                                            ciclos.DuracionF7B =s.DuracionF7B;
                                            ciclos.DuracionF8A =s.DuracionF8A;
                                            ciclos.DuracionF8B =s.DuracionF8B;
                                            ciclos.DuracionF9  =s.DuracionF9 ;
                                            ciclos.DuracionF10 =s.DuracionF10;
                                            ciclos.DuracionF11 = s.DuracionF11;
                                            ciclos.TTotal = s.TTotal;

                                            ciclos.PInicialF3 = s.PInicialF3;
                                            ciclos.TE2IF3 = s.TE2IF3;
                                            ciclos.TE3IF3 = s.TE3IF3;
                                            ciclos.TE4IF3 = s.TE4IF3;

                                            ciclos.PFinalF3 = s.PFinalF3;
                                            ciclos.TE2FF3 = s.TE2FF3;
                                            ciclos.TE3FF3 = s.TE3FF3;
                                            ciclos.TE4FF3 = s.TE4FF3;

                                            ciclos.FoTE2FF3 = s.FoTE2FF3 ;
                                            ciclos.FoTE3FF3 = s.FoTE3FF3 ;
                                            ciclos.FoTE4FF3 = s.FoTE4FF3;
                                            ciclos.TMinimaEst= s.TMinimaEst;
                                            ciclos.TMaximaEst = s.TMaximaEst;

                                           ciclos.FoTE2FF12 =s.FoTE2FF12 ;
                                           ciclos.FoTE3FF12 =s.FoTE3FF12 ;
                                           ciclos.FoTE4FF12 = s.FoTE4FF12;
                                         
                                           ciclos.DifFoMaxFoMin = s.DifFoMaxFoMin;
                                           

                                        }
                                        var duplicado = context.Vapor.Count(a => a.NProgresivo == ciclos.NProgresivo);

                                        try
                                        {
                                            if (ciclos.NPrograma != null)
                                            {
                                                if (ciclos.NPrograma>=9)
                                                {

                                                    if (duplicado == 0)
                                                    {
                                                        _log.WriteLog("RegistroVapor" + " " + ciclo);
                                                        context.Vapor.Add(ciclos);
                                                        context.SaveChanges();
                                                    }
                                                    else { /*Console.WriteLine("Duplicado");*/ }


                                                }
                                                else { }

                                                System.Threading.Thread.Sleep(2000);
                                            }
                                            else { }

                                        }
                                        catch
                                        {
                                          


                                            
                                            

                                        }


                                    }
                                    else { /*_log.WriteLog("");*/ }
                                }

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

