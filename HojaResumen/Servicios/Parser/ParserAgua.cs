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
    public class ParserAgua : IParserAgua
    {
        ILog _log = new ProductionLog();
        public void ParserWater()
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
                                        // _log.WriteLog(ciclo + " " + "Archivo existente");
                                       
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

                                        //Console.WriteLine("Dentro4");

                                        List<ProgramaAgua> RegistroFinal = new List<ProgramaAgua>(); //declaro la lista que quiero cargarle
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

                                            var F1 = RegistroDatosFF[0].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F2 = RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F3 = RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F4 = RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F5 = RegistroDatosFF[12].Replace(" ", String.Empty).Substring(21).Trim().Split(':').ToArray();
                                            var F6 = RegistroDatosFF[15].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F7 = RegistroDatosFF[18].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F8 = RegistroDatosFF[21].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F9 = RegistroDatosFF[24].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F10 = RegistroDatosFF[27].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F11 = RegistroDatosFF[30].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F12 = RegistroDatosFF[33].Replace(" ", String.Empty).Substring(21).Split(':').ToArray();
                                            var F13 = RegistroCiclos[63].Substring(2,6).Trim().Split(':').ToArray();
                                            var Fc = TCicloCalculado.Split(':').ToArray();
                                          
                                            var DF1 = Math.Round((int.Parse(F1[0]) + double.Parse(F1[1]) / 60), 2);
                                           var DF2 = Math.Round((int.Parse(F2[0]) + double.Parse(F2[1]) / 60),2);
                                            var DF3 = Math.Round((int.Parse(F3[0]) + double.Parse(F3[1]) / 60), 2);
                                            var DF4 = Math.Round((int.Parse(F4[0]) + double.Parse(F4[1]) / 60), 2);
                                          
                                            var DF5= Math.Round((int.Parse(F5[0]) + double.Parse(F5[1]) / 60),2);
                                            var DF6 = Math.Round((int.Parse(F6[0]) + double.Parse(F6[1]) / 60), 2);
                                           
                                            var DF7 = Math.Round((int.Parse(F7[0]) + double.Parse(F7[1]) / 60), 2);
                                            var DF8 = Math.Round((int.Parse(F8[0]) + double.Parse(F8[1]) / 60), 2);
                                            var DF9 = Math.Round((int.Parse(F9[0]) + double.Parse(F9[1]) / 60), 2);
                                            var DF10 = Math.Round((int.Parse(F10[0]) + double.Parse(F10[1]) / 60), 2);
                                            var DF11 = Math.Round((int.Parse(F11[0]) + double.Parse(F11[1]) / 60), 2);
                                            var DF12 = Math.Round((int.Parse(F12[0]) + double.Parse(F12[1]) / 60), 2);
                                            var DF13 = Math.Round((int.Parse(F13[0]) + double.Parse(F13[1]) / 60), 2);
                                            var DF14 = Math.Round((int.Parse(Fc[0]) + double.Parse(Fc[1]) / 60), 2);
                                          
                                            var PiF5= double.Parse(RegistroTF[1].Substring(8, 9).Trim(), CultureInfo.InvariantCulture);
                                            var PiF6 = double.Parse(RegistroCiclos[28].Substring(8, 9).Trim(), CultureInfo.InvariantCulture);

                                            var FoTE4FF6 = RegistroCiclos[34].Substring(15).Trim().Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                                            var FoTE4FF6s3 = double.Parse(FoTE4FF6[2].Trim(), CultureInfo.InvariantCulture);

                                            // decimal DF13 = decimal.Round((int.Parse(F13[0]) + decimal.Parse(F13[1]) / 60), 2);
                                            //Console.WriteLine(decimal.Round(suma,2));
                                            // Console.WriteLine(RegistroCiclos[64].Substring(15).Trim().Substring(0, 5));

                                            ProgramaAgua row = new ProgramaAgua
                                            {
                                                
                                                idAutoclave = RegistroEncabezado[5].Replace(" ", String.Empty).Substring(10).Trim(),
                                                NProgresivo = RegistroProgresivo[0].Replace(" ", String.Empty).Substring(12).Trim(),  ////problema 24231 autoB 11/11/2020
                                                NPrograma = Convert.ToInt32(RegistroEncabezado[0].Replace(" ", String.Empty).Substring(8).Trim()),
                                               
                                                CodProducto = RegistroEncabezado[3].Replace(" ", String.Empty).Substring(11).Trim(),
                                                Lote = RegistroEncabezado[4].Replace(" ", String.Empty).Substring(6).Trim(),
                                                HoraInicio= Convert.ToDateTime(RegistroPie[0].Substring(22).Trim()),
                                                HoraFin =Convert.ToDateTime( RegistroPie[1].Substring(22).Trim()),
                                                NCarro = texts[12] + texts[13] + texts[14],

                                                DuracionF1 = DF1,
                                                DuracionF2=  DF2,
                                                DuracionF3 = DF3,
                                                DuracionF4 = DF4,
                                                DuracionF5 = DF5,
                                                DuracionF6 = DF6,
                                                DuracionF7 = DF7,
                                                DuracionF8 = DF8,
                                                DuracionF9 = DF9,
                                                DuracionF10 =DF10,
                                                DuracionF11 =DF11,
                                                DuracionF12 =DF12,
                                                TTotal=      DF13,
                                                TCalculado=  DF14,
                                                
                                              PInicialF5 = PiF5,  //PFinalF5
                                                PInicialF6 = PiF6,
                                                
                                                TE2IF6 = double.Parse(RegistroCiclos[28].Substring(2).Trim().Substring(14, 5), CultureInfo.InvariantCulture),
                                                TE3IF6 = double.Parse(RegistroCiclos[28].Substring(2).Trim().Substring(21, 5), CultureInfo.InvariantCulture),
                                                TE4IF6 = double.Parse(RegistroCiclos[28].Substring(2).Trim().Substring(28, 5), CultureInfo.InvariantCulture),

                                                PFinalF6 = double.Parse(RegistroCiclos[33].Substring(8, 9).Trim(), CultureInfo.InvariantCulture),
                                                TE2FF6 =   double.Parse(RegistroCiclos[33].Substring(2).Trim().Substring(14, 5), CultureInfo.InvariantCulture),
                                                TE3FF6 =   double.Parse(RegistroCiclos[33].Substring(2).Trim().Substring(21, 5), CultureInfo.InvariantCulture),
                                                TE4FF6 =   double.Parse(RegistroCiclos[33].Substring(2).Trim().Substring(28, 5), CultureInfo.InvariantCulture),

                                                FoTE2FF6 = double.Parse(RegistroCiclos[34].Substring(15).Trim().Substring(0, 5), CultureInfo.InvariantCulture),
                                                FoTE3FF6 = double.Parse(RegistroCiclos[34].Substring(15).Trim().Substring(5, 9), CultureInfo.InvariantCulture),
                                                //FoTE4FF6 = double.Parse(RegistroCiclos[34].Substring(15).Trim().Substring(11, 7), CultureInfo.InvariantCulture),
                                                FoTE4FF6=FoTE4FF6s3,

                                                TMinimaEst = double.Parse(RegistroPie[3].Replace(" ", String.Empty).Substring(25, 5).Trim(), CultureInfo.InvariantCulture),
                                                TMaximaEst = double.Parse(RegistroPie[4].Replace(" ", String.Empty).Substring(25, 5).Trim(), CultureInfo.InvariantCulture),

                                                PFinalF7 = double.Parse(RegistroTF[3].Substring(8, 9).Trim(), CultureInfo.InvariantCulture),
                                                
                                               FoTE2FF13 =  double.Parse(RegistroCiclos[64].Substring(15).Trim().Substring(0, 5), CultureInfo.InvariantCulture),
                                               FoTE3FF13 =  double.Parse(RegistroCiclos[64].Substring(15).Trim().Substring(5, 9), CultureInfo.InvariantCulture),
                                               FoTE4FF13 =  double.Parse(RegistroCiclos[64].Substring(15).Trim().Substring(11, 7), CultureInfo.InvariantCulture),

                                                DifFoMaxFoMin = double.Parse(Dif, CultureInfo.InvariantCulture)
                                              

                                            }; RegistroFinal.Add(row); //añado elementos
                                        }
                                        catch
                                        {

                                            _log.WriteLog("FallaParserAgua" + " " + ciclo);

                                        }


                                        var ciclos = new Agua();// Instancio objeto Ciclos

                                        foreach (var s in RegistroFinal)  //recorro la lista
                                        {


                                            ciclos.idAutoclave = s.idAutoclave;
                                            ciclos.NCarro = s.NCarro;
                                            ciclos.NProgresivo = s.NProgresivo;
                                            ciclos.NPrograma = s.NPrograma;
                                            ciclos.CodProducto = s.CodProducto;
                                            ciclos.Lote = s.Lote;
                                            ciclos.HoraInicio = s.HoraInicio;
                                            ciclos.HoraFin = s.HoraFin;

                                            ciclos.DuracionF1 = s.DuracionF1;
                                            ciclos.DuracionF2 = s.DuracionF2;
                                            ciclos.DuracionF3 = s.DuracionF3;
                                            ciclos.DuracionF4 = s.DuracionF4;
                                            ciclos.DuracionF5 = s.DuracionF5;
                                            ciclos.DuracionF6 = s.DuracionF6;
                                            ciclos.DuracionF7 = s.DuracionF7;
                                            ciclos.DuracionF8 = s.DuracionF8;
                                            ciclos.DuracionF9 = s.DuracionF9;
                                            ciclos.DuracionF10 = s.DuracionF10;
                                            ciclos.DuracionF11 = s.DuracionF11;
                                            ciclos.DuracionF12 = s.DuracionF12;

                                            ciclos.PInicialF5 = s.PInicialF5;
                                            ciclos.PInicialF6 = s.PInicialF6;
                                            ciclos.TE2IF6 = s.TE2IF6;
                                            ciclos.TE3IF6 = s.TE3IF6;
                                            ciclos.TE4IF6 = s.TE4IF6;

                                            ciclos.PFinalF6 = s.PFinalF6;
                                            ciclos.TE2FF6 = s.TE2FF6;
                                            ciclos.TE3FF6 = s.TE3FF6;
                                            ciclos.TE4FF6 = s.TE4FF6;
                                            ciclos.FoTE2FF6 = s.FoTE2FF6;
                                            ciclos.FoTE3FF6 = s.FoTE3FF6;
                                            ciclos.FoTE4FF6 = s.FoTE4FF6;
                                            ciclos.TMinimaEst = s.TMinimaEst;
                                            ciclos.TMaximaEst = s.TMaximaEst;
                                            ciclos.PFinalF7 = s.PFinalF7;

                                            ciclos.TTotal = s.TTotal;
                                            ciclos.TCalculado = s.TCalculado;

                                            ciclos.FoTE2FF13 = s.FoTE2FF13;
                                            ciclos.FoTE3FF13 = s.FoTE3FF13;
                                            ciclos.FoTE4FF13 = s.FoTE4FF13;

                                            ciclos.DifFoMaxFoMin = s.DifFoMaxFoMin;


                                        

                                          

                                        }
                                        var duplicado = context.Agua.Count(a => a.NProgresivo == ciclos.NProgresivo && a.idAutoclave == ciclos.idAutoclave);

                                       

                                        try
                                        {
                                           
                                            if (ciclos.NPrograma != null)
                                            {

                                                //if (ciclos.NPrograma.Equals(2) || ciclos.NPrograma.Equals(3) || ciclos.NPrograma.Equals(4) || ciclos.NPrograma.Equals(8) || ciclos.NPrograma.Equals(20))
                                                //if(ciclos.NPrograma>0)
                                                if (ciclos.NPrograma == 2 || ciclos.NPrograma == 3 || ciclos.NPrograma == 4 || ciclos.NPrograma == 8 || ciclos.NPrograma == 20)

                                                {

                                                    if (duplicado == 0)
                                                    {
                                                        _log.WriteLog("RegistroAgua" +" "+ ciclo);
                                                        context.Agua.Add(ciclos);
                                                        context.SaveChanges();
                                                    }
                                                    else { /*Console.WriteLine("Registros Duplicados y Registros JKL");*/ }

                                                }
                                                else { /*_log.WriteLog(ciclos.IdAutoclave + " " + "Ciclo: " + ciclos.NumeroCiclo + " " + "Pertenece a otro programa: " + ciclos.Programa);*/ }
                                            }

                                        }
                                        catch
                                        {
                                          


                                        }




                                        System.Threading.Thread.Sleep(2000);

                                    }
                                    else { /*_log.WriteLog(ciclo + " " + "El archivo todavia No ha sido generado por el Autoclave");*/ }
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

