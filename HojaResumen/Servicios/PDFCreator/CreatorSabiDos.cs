using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using HojaResumen.Modelo.BaseDatosT;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using HojaResumen.Modelo;

namespace HojaResumen.Servicios.PDFCreator
{
    public class CreatorSabiDos : ICreatorSabiDos
    {
        public void CreateSabiDosPDF()
        {
            using (var context = new CicloAutoclave())

            {

                var ListaAutoclaves = new List<IdAutoClaveSabiDos>
                        {
                             
                             new IdAutoClaveSabiDos {Autoclave="0827J"},
                             new IdAutoClaveSabiDos {Autoclave="0828K"},
                             new IdAutoClaveSabiDos {Autoclave="1167L"},

                         };

                foreach (var t in ListaAutoclaves)
                {

                    try { 
                    var q = (from x in context.CiclosSabiDos.Where(x => x.IdAutoclave == t.Autoclave)
                             .OrderByDescending(s => s.Id)
                            select x).First();



                        //foreach (var q in query)

                        //{


                        PdfDocument pdf = new PdfDocument();
                        pdf.Info.Title = "My First PDF";
                        PdfPage pdfPage = pdf.AddPage();
                        XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                        XFont font = new XFont("Verdana", 8, XFontStyle.Regular);
                        XFont fontDos = new XFont("Verdana", 7, XFontStyle.Regular);
                        XTextFormatter tf = new XTextFormatter(graph);
                        XRect rect = new XRect(40, 100, 250, 220);
                        //graph.DrawRectangle(XBrushes.SeaShell, rect);


                        rect = new XRect(360, 495, 250, 220);
                        graph.DrawRectangle(XBrushes.White, rect);
                        tf.Alignment = XParagraphAlignment.Justify;





                    graph.DrawString("PROGRAMA: " + "                      " + q.Programa, font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("" + q.IdSeccion, font, XBrushes.Black, new XRect(20, 35, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("PROGRAMADOR: " + "              " + q.Programador, font, XBrushes.Black, new XRect(20, 45, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("OPERADOR: " + "                     " + q.Operador, font, XBrushes.Black, new XRect(20, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("CODIGO PRODUCTO: " + "        " + q.CodigoProducto, font, XBrushes.Black, new XRect(20, 65, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.LOTE: " + "                           " + q.Lote, font, XBrushes.Black, new XRect(20, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ID. MAQUINA: " + "                  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("NOTAS: " + "                           " + q.Notas, font, XBrushes.Black, new XRect(20, 95, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("MODELO: " + "                         " + q.Modelo, font, XBrushes.Black, new XRect(20, 115, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.PROGRESIVO: " + "               " + q.NumeroCiclo, font, XBrushes.Black, new XRect(20, 125, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FASE 1:  " + q.Fase1 + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF1 + " " + "min.s", font, XBrushes.Black, new XRect(20, 145, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 2:  " + q.Fase2 + "                                            " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(20, 160, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF2 + "      " + q.TISubF2, font, XBrushes.Black, new XRect(20, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF2 + "      " + q.TFSubF2, font, XBrushes.Black, new XRect(20, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 3:  " + q.Fase3 + "                                            " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(20, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF3 + "      " + q.TISubF3, font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF3 + "      " + q.TFSubF3, font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 4:  " + q.Fase4 + "                                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF4 + "      " + q.TISubF4, font, XBrushes.Black, new XRect(20, 265, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FASE 5:  " + q.Fase5 + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF5 , font, XBrushes.Black, new XRect(20, 280, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 6:  " + q.Fase6 + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF6 + " " + "min.s", font, XBrushes.Black, new XRect(20, 295, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 7:  " + q.Fase7A + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(20, 310, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 8:  " + q.Fase8A + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(20, 325, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 7:  " + q.Fase7B + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(20, 340, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 8:  " + q.Fase8B + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FASE 9:  " + q.Fase9 + "                 " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF9 + "      " + q.TISubF9, font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DATOS FINALES DE FASE: " + q.TFF9, font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 10: " + q.Fase10 + "              " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 11: " + q.Fase11 + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 12: " + q.Fase12 , font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF12 + "      " + q.TISubF12, font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                    graph.DrawString("HORA COMIEN.PROGR : " + "               " + q.HoraInicio, font, XBrushes.Black, new XRect(20, 480, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("HORA FIN.PROGR : " + "                      " + q.HoraFin, font, XBrushes.Black, new XRect(20, 495, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ESTERILIZACION N. : " + "                   " + q.EsterilizacionN, font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MIN.ESTERILIZACION: " + "        " + "°C  " + q.TMinima, font, XBrushes.Black, new XRect(20, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MAX.ESTERILIZACION: " + "        " + "°C  " + q.TMaxima, font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DURACION FASE DE ESTER.: " + "        " + q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(20, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("F(T,z) MIN.: " + "                                " + q.FtzMin, font, XBrushes.Black, new XRect(20, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("F(T,z) MAX.: " + "                               " + q.FtzMax, font, XBrushes.Black, new XRect(20, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 660, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("Registrado  " + q.FechaRegistro, font, XBrushes.Black, new XRect(400, 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("Impreso  " + DateTime.Now, font, XBrushes.Black, new XRect(400, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);



                        string nombreArchivo = "AutoClave" + q.IdAutoclave + q.NumeroCiclo + ".pdf";
                       
                        if (q.IdAutoclave == "0827J")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveJ";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                        }

                        if (q.IdAutoclave == "0828K")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveK";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                        }

                        if (q.IdAutoclave == "1167L")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveL";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                        }


                        //string path = @"C:\Users\fuenteI3\Desktop\PDFGenerados\HojaResumenAutoclaveK.pdf";
                        //foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                        //{
                        //    var x = printer.ToString() + System.Environment.NewLine;
                        //    Console.WriteLine(x);
                        //}

                        //File.Copy(path, "Microsoft Print to PDF", true);

                        //Process printJob = new Process();
                        //printJob.StartInfo.FileName = path;
                        //printJob.StartInfo.UseShellExecute = true;
                        //printJob.StartInfo.Verb = "printto";
                        //printJob.StartInfo.CreateNoWindow = true;
                        //printJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        //printJob.StartInfo.Arguments = "\"" + "Microsoft XPS Document Writer" + "\"";
                        //printJob.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                        //printJob.Start();

                        //"Microsoft Print to PDF"
                        // "Microsoft XPS Document Writer"
                        // "Webex Document Loader"





                        //PrintDocument pd = new PrintDocument();
                        //pd.PrinterSettings.PrinterName = "#ADMICOLOR (ESSAFILEPRINT01)";

                        //}

                    }
                    catch { Console.WriteLine("Registros no existentes"); }

                }

            }



        }
    }
}
