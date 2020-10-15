using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using HojaResumen.Modelo.BaseDatosT;
using System.IO;

namespace HojaResumen.Servicios.PDFCreator
{
    public class Creator : ICreator
    {
        public void CreatePdf()
        {
            using (var context = new CicloAutoclave())

            {
                var query = from d in context.CiclosAutoclaves.Where(x => x.IdAutoclave == "NA0672EGI").OrderByDescending(s => s.Id)
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




                    graph.DrawString("PROGRAMA: " + "                      " + q.Programa, font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("" + q.IdSeccion, font, XBrushes.Black, new XRect(20, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("PROGRAMADOR: " + "              " + q.Programador, font, XBrushes.Black, new XRect(20, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("OPERADOR: " + "                     " + q.Operador, font, XBrushes.Black, new XRect(20, 70, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("CODIGO PRODUCTO: " + "        " + q.CodigoProducto, font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.LOTE: " + "                           " + q.Lote, font, XBrushes.Black, new XRect(20, 100, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ID. MAQUINA: " + "                  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 115, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("NOTAS: " + "                           " + q.Notas, font, XBrushes.Black, new XRect(20, 130, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("MODELO: " + "                         " + q.Modelo, font, XBrushes.Black, new XRect(20, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("N.PROGRESIVO: " + "               " + q.NumeroCiclo, font, XBrushes.Black, new XRect(20, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FASE 1:  " + q.Fase1 + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF1 + " " + "min.s", font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 2:  " + q.Fase2 + "                                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 3:  " + q.Fase3 + "                        " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 4:  " + q.Fase4 + "                                      " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(20, 265, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 5:  " + q.Fase5 + "                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF5 + " " + "min.s" + "          " + q.TInicio, font, XBrushes.Black, new XRect(20, 280, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF5 + "      " + q.TISubF5, font, XBrushes.Black, new XRect(20, 295, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF5 + "      " + q.TFSubF5, font, XBrushes.Black, new XRect(20, 310, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 6:  " + q.Fase6 + "                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF6 + " " + "min.s", font, XBrushes.Black, new XRect(20, 325, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF6 + "      " + q.TISubF6, font, XBrushes.Black, new XRect(20, 340, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF6 + "      " + q.TFSubF6, font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 7:  " + q.Fase7 + "           " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7 + " " + "min.s", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF7 + "      " + q.TISubF7, font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 8:  " + q.Fase8 + "         " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8 + " " + "min.s", font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF8 + "      " + q.TISubF8, font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DATOS FINALES DE FASE: " + q.TFF8, font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 9:  " + q.Fase9 + "           " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF9 + "      " + q.TISubF9, font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DATOS FINALES DE FASE: " + q.TFF9, font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 10: " + q.Fase10 + "       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(20, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 11: " + q.Fase11 + "                               " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(20, 505, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FASE 12: " + q.Fase12 + "         " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF12 + " " + "min.s", font, XBrushes.Black, new XRect(20, 520, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10   " + q.TFF13.Substring(0, 6) + "   " + q.TiempoCiclo + "   " + q.TFF13.Substring(6) + "      " + q.TFSubF13, font, XBrushes.Black, new XRect(20, 535, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("HORA COMIEN.PROGR : " + "               " + q.HoraInicio, font, XBrushes.Black, new XRect(20, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("HORA FIN.PROGR : " + "                      " + q.HoraFin, font, XBrushes.Black, new XRect(20, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("ESTERILIZACION N. : " + "                   " + q.EsterilizacionN, font, XBrushes.Black, new XRect(20, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MIN.ESTERILIZACION: " + "         " + "°C  " + q.TMinima, font, XBrushes.Black, new XRect(20, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("TEMP.MAX.ESTERILIZACION: " + "         " + "°C  " + q.TMaxima, font, XBrushes.Black, new XRect(20, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("DURACION FASE DE ESTER.: " + "         " + q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(20, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("F(T,z) MIN.: " + "                                 " + q.FtzMin, font, XBrushes.Black, new XRect(20, 665, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("F(T,z) MAX.: " + "                                 " + q.FtzMax, font, XBrushes.Black, new XRect(20, 680, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 695, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 735, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 755, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString("Impreso  " + DateTime.Now, font, XBrushes.Black, new XRect(20, 790, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("LISTADO ALARMAS:  " + q.ErrorCiclo.Replace("\n", String.Empty), font, XBrushes.Black, new XRect(20, 820, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                    string nombreArchivo = @"HojaResumenAutoclaveI.pdf";
                    string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\";

                    nombreArchivo = Path.GetFileName(nombreArchivo);
                    rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);


                    // string pdfFilename = "C:\\Users\\fuenteI3\\Desktop\\PDFGenerados\\HojaResumenAutoclaveI.pdf ";

                    //Process.Start(pdfFilename);
                    pdf.Save(rutaAbsoluta);  //Guarda el PDF

                }

            }
        }
    }
}
