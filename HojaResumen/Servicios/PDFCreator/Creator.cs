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
using System.Drawing;
using HojaResumen.Modelo;

namespace HojaResumen.Servicios.PDFCreator
{
    public class Creator : ICreator 
    {
        public void CreatePdf()
        {
            using (var context = new CicloAutoclave())

            {
                var ListaAutoclaves = new List<IdAutoclaveSabiUno>
                        {
                             new IdAutoclaveSabiUno {Autoclave="NF8387A"},
                             new IdAutoclaveSabiUno {Autoclave="8388B"},
                             new IdAutoclaveSabiUno {Autoclave="8389C"},
                             new IdAutoclaveSabiUno {Autoclave="8607D"},
                             new IdAutoclaveSabiUno {Autoclave="NF1029E"},
                             new IdAutoclaveSabiUno {Autoclave="NF1030F"},
                             new IdAutoclaveSabiUno {Autoclave="NF1031G"},
                             new IdAutoclaveSabiUno {Autoclave="NA0658EGH"},
                             new IdAutoclaveSabiUno {Autoclave="NA0672EGI"},
                             new IdAutoclaveSabiUno {Autoclave="NA0611EFM"},
                         };

                foreach (var t in ListaAutoclaves)
                {
                    try
                    {
                        var q = (from x in context.CiclosAutoclaves.Where(x => x.IdAutoclave == t.Autoclave)

                                 .OrderByDescending(s => s.Id)
                                 select x).First();


                        //foreach (var q in query)

                        //{

                        // Console.WriteLine("ID: " + q.IdAutoclave + " " + "Programador: " + q.Programador + " " + "Operador: " + " " + q.Operador);
                        PdfDocument pdf = new PdfDocument();
                        pdf.Info.Title = "My First PDF";
                        PdfPage pdfPage = pdf.AddPage();
                        XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                        XFont font = new XFont("Verdana", 8, XFontStyle.Regular);
                        XFont fontDos = new XFont("Verdana", 7, XFontStyle.Regular);
                        XFont negrita = new XFont("Verdana", 8, XFontStyle.Bold);

                        XTextFormatter tf = new XTextFormatter(graph);
                        XRect rect = new XRect(40, 100, 250, 220);
                        //graph.DrawRectangle(XBrushes.SeaShell, rect);


                        rect = new XRect(360, 525, 250, 220);
                        graph.DrawRectangle(XBrushes.White, rect);
                        tf.Alignment = XParagraphAlignment.Justify;
                        // tf.DrawString(q.ErrorCiclo, font, XBrushes.Black, rect, XStringFormats.TopLeft);



                        graph.DrawString("ID. MAQUINA: " + "   " + q.IdAutoclave+"             "+"N.PROGRESIVO: "+"  "+ q.NumeroCiclo +"        "+"Informe de ciclo de esterilización"+"      "+"Impreso: "+ DateTime.Now, font, XBrushes.Black, new XRect(20, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("PROGRAMA:", font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Programa, negrita, XBrushes.Black, new XRect(130, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.IdSeccion, font, XBrushes.Black, new XRect(20, 35, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("PROGRAMADOR:", font, XBrushes.Black, new XRect(20, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Programador, font, XBrushes.Black, new XRect(130, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("OPERADOR:", font, XBrushes.Black, new XRect(20, 65, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Operador, font, XBrushes.Black, new XRect(130, 65, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("CODIGO PRODUCTO:", font, XBrushes.Black, new XRect(20, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.CodigoProducto + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("N.LOTE:", font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Lote + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("ID. MAQUINA:", font, XBrushes.Black, new XRect(20, 95, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.IdAutoclave + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 95, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("NOTAS:", font, XBrushes.Black, new XRect(20, 105, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 105, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("MODELO:", font, XBrushes.Black, new XRect(20, 125, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.Modelo, font, XBrushes.Black, new XRect(130, 125, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("N.PROGRESIVO:", font, XBrushes.Black, new XRect(20, 135, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 135, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 1:  " + q.Fase1, font, XBrushes.Black, new XRect(20, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF1 + " " + "min.s", font, XBrushes.Black, new XRect(320, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 2:  " + q.Fase2 , font, XBrushes.Black, new XRect(20, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(320, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 3:  " + q.Fase3 , font, XBrushes.Black, new XRect(20, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(320, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 4:  " + q.Fase4, font, XBrushes.Black, new XRect(20, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(320, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                        graph.DrawString("FASE 5:  " + q.Fase5 , font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:" , font, XBrushes.Black, new XRect(230, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF5 + " " + "min.s" + " "+"<--[  ]", negrita, XBrushes.Black, new XRect(320, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString( q.TInicio, negrita, XBrushes.Black, new XRect(460, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TIF5, font, XBrushes.Black, new XRect(230, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TISubF5, font, XBrushes.Black, new XRect(460, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFF5, font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFSubF5, font, XBrushes.Black, new XRect(460, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 6:  " + q.Fase6 , font, XBrushes.Black, new XRect(20, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:" , font, XBrushes.Black, new XRect(230, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF6 + " " + "min.s" + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(320, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TIF6, font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TISubF6, font, XBrushes.Black, new XRect(460, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFF6, font, XBrushes.Black, new XRect(230, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFSubF6 + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(460, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 7:  " + q.Fase7 , font, XBrushes.Black, new XRect(20, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF7 + " " + "min.s", font, XBrushes.Black, new XRect(320, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TIF7, font, XBrushes.Black, new XRect(230, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TISubF7, font, XBrushes.Black, new XRect(460, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 8:  " + q.Fase8, font, XBrushes.Black, new XRect(20, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF8 + " " + "min.s", font, XBrushes.Black, new XRect(320, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10" , font, XBrushes.Black, new XRect(20, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TIF8, font, XBrushes.Black, new XRect(230, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TISubF8, font, XBrushes.Black, new XRect(460, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFF8, font, XBrushes.Black, new XRect(230, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 9:  " + q.Fase9, font, XBrushes.Black, new XRect(20, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(320, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TIF9, font, XBrushes.Black, new XRect(230, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TISubF9, font, XBrushes.Black, new XRect(460, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 425, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.TFF9, font, XBrushes.Black, new XRect(230, 425, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 10: " + q.Fase10, font, XBrushes.Black, new XRect(20, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(320, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 11: " + q.Fase11, font, XBrushes.Black, new XRect(20, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(320, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("FASE 12: " + q.Fase12, font, XBrushes.Black, new XRect(20, 470, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 470, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString(q.DuracionTotalF12 + " " + "min.s", font, XBrushes.Black, new XRect(320, 470, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FASE 13: FIN DE CICLO         TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10" , font, XBrushes.Black, new XRect(20, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString( q.TFF13.Substring(0, 6) + "   " + q.TiempoCiclo + "   " + q.TFF13.Substring(6) + "      " + q.TFSubF13 + "  " + "[  ]", font, XBrushes.Black, new XRect(20, 505, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                        graph.DrawString("HORA COMIEN.PROGR : " + "               " + q.HoraInicio, font, XBrushes.Black, new XRect(20, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("HORA FIN.PROGR : " + "                      " + q.HoraFin, font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("ESTERILIZACION N. : " + "                   " + q.EsterilizacionN, font, XBrushes.Black, new XRect(20, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TEMP.MIN.ESTERILIZACION: " + "         " + "°C  " + q.TMinima + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("TEMP.MAX.ESTERILIZACION: " + "         " + "°C  " + q.TMaxima + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("DURACION FASE DE ESTER.: " + "         " + q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(20, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("F(T,z) MIN.: " + "                                 " + q.FtzMin, font, XBrushes.Black, new XRect(20, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("F(T,z) MAX.: " + "                                 " + q.FtzMax, font, XBrushes.Black, new XRect(20, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("Dif F(T,z) : " + "                                   " + q.DifMaxMin, font, XBrushes.Black, new XRect(20, 645, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 660, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 690, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 720, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                       // graph.DrawString("Registrado  " + q.FechaRegistro, font, XBrushes.Black, new XRect(400, 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                       // graph.DrawString("Impreso  " + DateTime.Now, font, XBrushes.Black, new XRect(400, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        //graph.DrawString("Impreso  " +q.FechaRegistro , font, XBrushes.Black, new XRect(400, 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);


                        string nombreArchivo = "AutoClave" + q.IdAutoclave + q.NumeroCiclo + ".pdf";
                       
                        if (q.IdAutoclave == "NF8387A")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveA";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "8388B")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveB";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "8389C")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveC";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "8607D")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveD";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NF1029E")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveE";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NF1030F")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveF";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NF1031G")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveG";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NA0658EGH")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveH";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NA0672EGI")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveI";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (q.IdAutoclave == "NA0611EFM")
                        {
                            string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveM";
                            nombreArchivo = Path.GetFileName(nombreArchivo);
                            rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                            pdf.Save(rutaAbsoluta);
                            System.Threading.Thread.Sleep(1000);
                        }





                        // string pdfFilename = "C:\\Users\\fuenteI3\\Desktop\\PDFGenerados\\HojaResumenAutoclaveI.pdf ";

                        //Process.Start(pdfFilename);
                        //Guarda el PDF

                    }
                    catch { Console.WriteLine("Registros no existentes" ); }

                }




            }
        }


    }
}
