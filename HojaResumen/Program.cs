using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Parser;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen
{
    class Program
    {
        static void Main(string[] args)
        {

            //////////Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();

            //////////var ListaDatos = new List<ConnectionData>
            ////////// {
            //////////    //new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02443.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveA_02443.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.89", Remote="NA0672EGI08871.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveI_08871.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.94", Remote="NA0658EGH13977.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveH_13977.txt" },
            //////////    new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02436.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\AutoClaveAP8500.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.93", Remote="NA0611EFM05014.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveM_05014.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.87", Remote="NF1031G08838.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P20AutoClaveG_05014.txt" },

            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.90", Remote="0827J07176.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveJ.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.91", Remote="0828K10597.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveK.txt" },
            //////////    //new ConnectionData { Version=4001, Ip="10.109.80.92", Remote="1167L20752.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveL.txt" },
            //////////    //new ConnectionData { Version=37, Ip="10.109.80.83", Remote="8389C22567.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveC2.txt" },

            ////////// };



            //////////    foreach (var item in ListaDatos)
            //////////    {
            //////////        System.Threading.Thread.Sleep(1000);

            //////////        float version = con.get_version();
            //////////        Console.WriteLine(version);


            //////////        uint handle = con.ConnectSession(item.Version, item.Ip);


            //////////        Console.WriteLine("Valor de Sesion: " + handle);

            //////////        uint lhandle = con.ConnectApi(handle);
            //////////        Console.WriteLine("Valor de Conexion: " + lhandle);

            //////////        uint result = con.GetData(handle, item.Remote, item.Local);


            //////////        Console.WriteLine("Resultado de getData: " + result);

            //////////        uint lasterror = con.GetError();
            //////////        Console.WriteLine("Valor Error: " + lasterror);

            //////////        Console.WriteLine("Cerrar Conexion valor: " + con.CloseConnection(ref handle));
            //////////    }


            //IParser GetData = new Parser();
            //GetData.ParserFile();

            //IParserSabiDos GetDataSabiDos = new ParserSabiDos();
            //GetDataSabiDos.ParserSabiDosFile();


            //Console.WriteLine("Escribiendo en la base de datos");




            using (var context = new CicloAutoclave())

            {
                var query = from d in context.CiclosAutoclaves.Where(x => x.Id == 125)
                            select d;

                foreach (var q in query)

                {

                    // Console.WriteLine("ID: " + q.IdAutoclave + " " + "Programador: " + q.Programador + " " + "Operador: " + " " + q.Operador);
                    PdfDocument pdf = new PdfDocument();
                    pdf.Info.Title = "My First PDF";
                    PdfPage pdfPage = pdf.AddPage();
                    XGraphics graph = XGraphics.FromPdfPage(pdfPage);                              
                    XFont font = new XFont("Verdana", 8, XFontStyle.Regular);
                    //XTextFormatter tf = new XTextFormatter(graph);
                    //XRect rect = new XRect(40, 100, 250, 220);
                    //graph.DrawRectangle(XBrushes.SeaShell, rect);




                    graph.DrawString("PROGRAMA: " +"                      "+ q.Programa, font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("" + q.IdSeccion, font, XBrushes.Black, new XRect(20, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("PROGRAMADOR: " + "              " + q.Programador, font, XBrushes.Black, new XRect(20, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("OPERADOR: " + "                     " + q.Operador, font, XBrushes.Black, new XRect(20, 70, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("CODIGO PRODUCTO: " + "        " + q.CodigoProducto, font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.LOTE: " + "                           " + q.Lote, font, XBrushes.Black, new XRect(20, 100, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ID. MAQUINA: " + "                  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 115, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("NOTAS: " + "                           " + q.Notas, font, XBrushes.Black, new XRect(20, 130, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("MODELO: " + "                         " + q.Modelo, font, XBrushes.Black, new XRect(20, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.PROGRESIVO: " + "               " + q.NumeroCiclo, font, XBrushes.Black, new XRect(20, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                     
                    graph.DrawString("FASE 1:  " + q.Fase1 + "                       " +"DURAC.TOTAL FASE:  "+ q.DuracionTotalF1 +" "+"min.s", font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 2:  " + q.Fase2 + "                                   " +"DURAC.TOTAL FASE:  " + q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 3:  " + q.Fase3 + "                        " +"DURAC.TOTAL FASE:  " + q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 4:  " + q.Fase4 + "                                      " +"DURAC.TOTAL FASE:  " + q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(20, 265, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 5:  " + q.Fase5 + "                   " +"DURAC.TOTAL FASE:  " + q.DuracionTotalF5 + " " + "min.s" + "          "+ q.TInicio, font, XBrushes.Black, new XRect(20, 280, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF5 + "      " + q.TISubF5 , font, XBrushes.Black, new XRect(20, 295, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF5 + "      " + q.TFSubF5 , font, XBrushes.Black, new XRect(20, 310, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 6:  " + q.Fase6 + "                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF6 + " " + "min.s" , font, XBrushes.Black, new XRect(20, 325, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF6 + "      " + q.TISubF6, font, XBrushes.Black, new XRect(20, 340, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF6 + "      " + q.TFSubF6, font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 7:  " + q.Fase7 + "           " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7 + " " + "min.s", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF7 + "      " + q.TISubF7, font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 8:  " + q.Fase8 + "         " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8 + " " + "min.s", font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF8 + "      " + q.TISubF8, font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DATOS FINALES DE FASE: " + q.TFF8 , font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 9:  " + q.Fase9 + "           " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF9 + "      " + q.TISubF9, font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DATOS FINALES DE FASE: " + q.TFF9, font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 10: " + q.Fase10 + "       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(20, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 11: " + q.Fase11 + "                               " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(20, 505, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 12: " + q.Fase12 + "         " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF12 + " " + "min.s", font, XBrushes.Black, new XRect(20, 520, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10   " + q.TFF13.Substring(0,6)+"   "+q.TiempoCiclo +"   "+ q.TFF13.Substring(6)+ "      " + q.TFSubF13, font, XBrushes.Black, new XRect(20, 535, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("HORA COMIEN.PROGR : " + "               " + q.HoraInicio, font, XBrushes.Black, new XRect(20, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("HORA FIN.PROGR : " + "                      " + q.HoraFin, font, XBrushes.Black, new XRect(20, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ESTERILIZACION N. : " + "                   " + q.EsterilizacionN, font, XBrushes.Black, new XRect(20, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MIN.ESTERILIZACION: " + "         "+"°C  " + q.TMinima, font, XBrushes.Black, new XRect(20, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MAX.ESTERILIZACION: " + "         " + "°C  " + q.TMaxima, font, XBrushes.Black, new XRect(20, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString( q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 690, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 705, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("LISTADO ALARMAS:  " + q.ErrorCiclo.Replace("\n", String.Empty) , font, XBrushes.Black, new XRect(20, 745, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);






                    string pdfFilename = "Pagina 1 de 1.pdf";
                    pdf.Save(pdfFilename);
                    Process.Start(pdfFilename);

                }

            }





            //string span = "108:46";
            //string tf = "13:14";

            //var td = TimeSpan.FromMinutes(Convert.ToDouble(span.Split(':')[0])).Add(TimeSpan.FromSeconds(Convert.ToDouble((span.Split(':')[1]))))
            //    - TimeSpan.Parse("00:" + tf);             // resta timeSpan
            //Console.WriteLine(td);
            //var z = TimeSpan.Parse(td.ToString().Substring(0, 5)).TotalMinutes + td.ToString().Substring(5, 3);
            //Console.WriteLine(z);





            Console.ReadKey();
            }
        }
    }

