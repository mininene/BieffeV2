using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Parser
{
    public class ParserSabiDos : IParserSabiDos
    {
        public void ParserSabiDosFile()
        {
            try
            {

                string path = @"C:\Users\fuenteI3\Desktop\RegistrosAutoclaves\AutoClaveJP10100.txt";
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
                        if (con[i].Contains(FaseDos) || con[i].Contains(FaseTres) || con[i].Contains(FaseCuatro) || con[i].Contains(FaseCinco)  && i >= 0)
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

                    string combin = string.Join("\n", RegistroAlarma);
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




                    List<ProgramaSabiDos> RegistroFinal = new List<ProgramaSabiDos>(); //declaro la lista que quiero cargar
                    try
                    {
                        ProgramaSabiDos row = new ProgramaSabiDos
                        {
                            IdAutoclave = RegistroEncabezado[5].Replace(" ", String.Empty).Substring(10).Trim(),
                            IdSeccion = texts[4].Trim(),
                            IdUsuario = "SabiDos",
                            TInicio = RegistroPie[0].Substring(19).Trim(),
                            NumeroCiclo = RegistroEncabezado[7].Replace(" ", String.Empty).Substring(12).Trim(),
                            Programa = RegistroEncabezado[0].Replace(" ", String.Empty).Substring(8).Trim(),
                            Modelo = RegistroEncabezado[6].Replace(" ", String.Empty).Substring(6).Trim(),  //modelo
                            Programador = RegistroEncabezado[1].Substring(11).Trim(),
                            Operador = RegistroEncabezado[2].Substring(10).Trim(),
                            CodigoProducto = RegistroEncabezado[3].Replace(" ", String.Empty).Substring(11).Trim(),
                            Lote = RegistroEncabezado[4].Replace(" ", String.Empty).Substring(6).Trim(),
                            Notas = texts[12] + texts[13],

                            Fase1 = RegistroCiclos[1].Substring(12).Trim(),
                            Fase2 = RegistroCiclos[6].Substring(12).Trim(),
                            Fase3 = RegistroCiclos[11].Substring(12).Trim(),
                            Fase4 = RegistroCiclos[16].Substring(12).Trim(),
                            Fase5 = RegistroCiclos[21].Substring(12).Trim(),
                            Fase6 = RegistroCiclos[26].Substring(12).Trim(),
                            Fase7A = RegistroCiclos[31].Substring(12).Trim(),
                            Fase7B = RegistroCiclos[41].Substring(12).Trim(),
                            Fase8A = RegistroCiclos[36].Substring(12).Trim(),
                            Fase8B = RegistroCiclos[46].Substring(12).Trim(),
                            Fase9 = RegistroCiclos[51].Substring(12).Trim(),
                            Fase10 = RegistroCiclos[56].Substring(12).Trim(),
                            Fase11 = RegistroCiclos[61].Substring(12).Trim(),
                            Fase12 = RegistroCiclos[66].Substring(12).Trim(),

                            TIF2 = RegistroCiclos[8].Substring(2).Trim(),
                            TIF3 = RegistroCiclos[13].Substring(2).Trim(),
                            TIF9 = RegistroCiclos[53].Substring(2).Trim(),
                            TIF12 = RegistroCiclos[68].Substring(2).Trim(),

                            TISubF2 = RegistroCiclos[9].Substring(14).Trim(),
                            TISubF3 = RegistroCiclos[14].Substring(14).Trim(),
                            TISubF9 = RegistroCiclos[54].Substring(14).Trim(),
                            TISubF12 = RegistroCiclos[69].Substring(14).Trim(),

                            DuracionTotalF1 = RegistroDatosFF[0].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF2 = RegistroDatosFF[3].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF3 = RegistroDatosFF[6].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF4 = RegistroDatosFF[9].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF5 = RegistroDatosFF[12].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF6 = RegistroDatosFF[15].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF7A = RegistroDatosFF[18].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF7B = RegistroDatosFF[24].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF8A = RegistroDatosFF[21].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF8B = RegistroDatosFF[27].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF9 = RegistroDatosFF[30].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF10 = RegistroDatosFF[33].Replace(" ", String.Empty).Substring(21).Trim(),
                            DuracionTotalF11 = RegistroDatosFF[36].Replace(" ", String.Empty).Substring(21).Trim(),

                            TFF2 = RegistroTF[1].Substring(3).Trim(),  //RegistroTF
                            TFF3 = RegistroTF[2].Substring(3).Trim(), //RegistroTF
                            TIF4 = RegistroTF[3].Substring(3).Trim(), //es Igual TFF4
                            TFF9 = RegistroDatosFF[32].Substring(2).Trim(),

                            TFSubF2 = RegistroTFSub[1].Substring(14).Trim(),
                            TFSubF3 = RegistroTFSub[2].Substring(14).Trim(), // TISubF7 es TFSubF7
                            TISubF4 = RegistroTFSub[3].Substring(14).Trim(), // es igual TFSubF4

                            HoraInicio = RegistroPie[0].Substring(22).Trim(),
                            HoraFin = RegistroPie[1].Substring(22).Trim(),
                            EsterilizacionN = RegistroPie[2].Substring(26).Trim(),
                            TMinima = RegistroPie[3].Replace(" ", String.Empty).Substring(25).Trim(),
                            TMaxima = RegistroPie[4].Replace(" ", String.Empty).Substring(25).Trim(),
                            DuracionTotal = RegistroPie[5].Replace(" ", String.Empty).Substring(25).Trim(),
                            FtzMin = RegistroPie[6].Replace(" ", String.Empty).Substring(11).Trim(),
                            FtzMax = RegistroPie[7].Replace(" ", String.Empty).Substring(11).Trim(),
                            AperturaPuerta = RegistroPie[8].Trim(),
                            TiempoCiclo = RegistroCiclos[68].Substring(2,6).ToString().Trim(), 
                            ErrorCiclo = combin,
                            FechaRegistro = DateTime.Now,
                            

                        }; RegistroFinal.Add(row); //añado elementos
                    }
                    catch
                    {
                        Console.WriteLine("Los campos No cumplen con el modelo");
                    }


                    var ciclos = new CiclosSabiDos();// Instancio objeto Ciclos

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
                        ciclos.TIF2 = s.TIF2;
                        ciclos.TISubF2 = s.TISubF2;
                        ciclos.TFF2 = s.TFF2;
                        ciclos.TFSubF2 = s.TFSubF2;

                        ciclos.Fase3 = s.Fase3;
                        ciclos.DuracionTotalF3 = s.DuracionTotalF3;
                        ciclos.TIF3 = s.TIF3;
                        ciclos.TISubF3 = s.TISubF3;
                        ciclos.TFF3 = s.TFF3;
                        ciclos.TFSubF3 = s.TFSubF3;

                        ciclos.Fase4 = s.Fase4;
                        ciclos.DuracionTotalF4 = s.DuracionTotalF4;
                        ciclos.TIF4 = s.TIF4;
                        ciclos.TISubF4 = s.TISubF4;

                        ciclos.Fase5 = s.Fase5;
                        ciclos.DuracionTotalF5 = s.DuracionTotalF5;
                        
                        ciclos.Fase6 = s.Fase6;
                        ciclos.DuracionTotalF6 = s.DuracionTotalF6;
                        
                        ciclos.Fase7A = s.Fase7A;
                        ciclos.DuracionTotalF7A = s.DuracionTotalF7A;

                        ciclos.Fase7B = s.Fase7B;
                        ciclos.DuracionTotalF7B = s.DuracionTotalF7B;

                        ciclos.Fase8A = s.Fase8A;
                        ciclos.DuracionTotalF8A = s.DuracionTotalF8A;

                        ciclos.Fase8B = s.Fase8B;
                        ciclos.DuracionTotalF8B = s.DuracionTotalF8B;
                                                            
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
                        ciclos.TIF12 = s.TIF12;
                        ciclos.TISubF12 = s.TISubF12;


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
                        ciclos.FechaRegistro = s.FechaRegistro;

                    }
                    context.CiclosSabiDos.Add(ciclos);
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
