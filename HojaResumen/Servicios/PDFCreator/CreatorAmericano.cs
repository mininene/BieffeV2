using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Output;
using HojaResumen.Servicios.Printer;
using HojaResumen.Servicios.PrinterProgramas;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.PDFCreator
{
    public class CreatorAmericano : ICreatorAmericano
    {
        ILog _log = new ProductionLog();
        IPrint _print = new Print();
        IPrinterDosTresCuatro _pr234 = new PrinterDosTresCuatro();
        public void CreateAmericanoPdf()
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
                    foreach (var n in context.MaestroAutoclave)
                    {

                        try
                        {
                            var q = (from x in context.CiclosAutoclaves.Where(x => x.IdAutoclave == t.Autoclave)

                                     .OrderByDescending(s => s.Id)
                                     select x).First();

                            if (n.Estado == true)
                            {
                                if (q.Programa.Trim().Equals("2") || q.Programa.Trim().Equals("3") || q.Programa.Trim().Equals("4"))
                                {

                                    //  _log.WriteLog("IMPRIMIENDO :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);

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


                                    rect = new XRect(340, 540, 250, 220);
                                    graph.DrawRectangle(XBrushes.White, rect);
                                    tf.Alignment = XParagraphAlignment.Justify;
                                    // tf.DrawString(q.ErrorCiclo, font, XBrushes.Black, rect, XStringFormats.TopLeft);



                                    graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, font, XBrushes.Black, new XRect(140, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("Informe de ciclo de esterilización", font, XBrushes.Black, new XRect(270, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("Impreso: " + DateTime.Now, font, XBrushes.Black, new XRect(440, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("Impreso por: Automático ", font, XBrushes.Black, new XRect(440, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("PROGRAMA:", font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.Programa + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
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

                                    graph.DrawString("FASE 2:  " + q.Fase2, font, XBrushes.Black, new XRect(20, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(320, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 3:  " + q.Fase3, font, XBrushes.Black, new XRect(20, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(320, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("FASE 4:  " + q.Fase4, font, XBrushes.Black, new XRect(20, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(320, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                                    graph.DrawString("FASE 5:  " + q.Fase5, font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF5, negrita, XBrushes.Black, new XRect(320, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString(q.TInicio, negrita, XBrushes.Black, new XRect(460, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF5, font, XBrushes.Black, new XRect(230, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TISubF5, font, XBrushes.Black, new XRect(460, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    //graph.DrawString(q.TFF5, font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF5.Substring(0, 6), font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF5.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF5.Substring(11), font, XBrushes.Black, new XRect(275, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFSubF5, font, XBrushes.Black, new XRect(460, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 6:  " + q.Fase6, font, XBrushes.Black, new XRect(20, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF6, negrita, XBrushes.Black, new XRect(320, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    //graph.DrawString(q.TIF6, font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF6.Substring(0, 6), font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF6.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF6.Substring(11), font, XBrushes.Black, new XRect(275, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TISubF6, font, XBrushes.Black, new XRect(460, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF6.Substring(0, 6), font, XBrushes.Black, new XRect(230, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF6.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF6.Substring(11), font, XBrushes.Black, new XRect(275, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFSubF6.Substring(0, 2), font, XBrushes.Black, new XRect(460, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFSubF6.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                                    graph.DrawString("FASE 7:  " + q.Fase7, font, XBrushes.Black, new XRect(20, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF7, negrita, XBrushes.Black, new XRect(320, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF7.Substring(0, 6), font, XBrushes.Black, new XRect(230, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF7.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF7.Substring(11), font, XBrushes.Black, new XRect(275, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TISubF7, font, XBrushes.Black, new XRect(460, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);




                                    graph.DrawString("FASE 8:  " + q.Fase8, font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF8 + " " + "min.s", font, XBrushes.Black, new XRect(320, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF8, font, XBrushes.Black, new XRect(230, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TISubF8, font, XBrushes.Black, new XRect(460, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF8, font, XBrushes.Black, new XRect(230, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 9:  " + q.Fase9, font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(320, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TIF9, font, XBrushes.Black, new XRect(230, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TISubF9, font, XBrushes.Black, new XRect(460, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF9, font, XBrushes.Black, new XRect(230, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 10: " + q.Fase10, font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(320, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 11: " + q.Fase11, font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(320, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 12: " + q.Fase12, font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotalF12 + " " + "min.s", font, XBrushes.Black, new XRect(320, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("FASE 13: FIN DE CICLO         TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10", font, XBrushes.Black, new XRect(20, 495, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF13.Substring(0, 6), font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TiempoCiclo, font, XBrushes.Black, new XRect(80, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFF13.Substring(6), font, XBrushes.Black, new XRect(160, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFSubF13.Substring(0, 2), font, XBrushes.Black, new XRect(460, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TFSubF13.Substring(2), font, XBrushes.Black, new XRect(465, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);




                                    graph.DrawString("HORA COMIEN.PROGR :", font, XBrushes.Black, new XRect(20, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.HoraInicio, negrita, XBrushes.Black, new XRect(160, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("HORA FIN.PROGR :", font, XBrushes.Black, new XRect(20, 545, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.HoraFin, negrita, XBrushes.Black, new XRect(160, 545, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    // graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    // graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    if (q.EsterilizacionN == "")
                                    {
                                        graph.DrawString("ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        graph.DrawString("FALLIDA", font, XBrushes.Black, new XRect(160, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    }
                                    else
                                    {
                                        graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    }

                                    graph.DrawString("TEMP.MIN.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TMinima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("TEMP.MAX.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.TMaxima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                                    graph.DrawString("DURACION FASE DE ESTER.:", font, XBrushes.Black, new XRect(20, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(160, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("F(T,z) MIN.:", font, XBrushes.Black, new XRect(20, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.FtzMin, font, XBrushes.Black, new XRect(160, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("F(T,z) MAX.:", font, XBrushes.Black, new XRect(20, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.FtzMax, font, XBrushes.Black, new XRect(160, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("Dif F(T,z):", font, XBrushes.Black, new XRect(20, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.DifMaxMin, font, XBrushes.Black, new XRect(160, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 665, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 705, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                    graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 745, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    if (q.ErrorCiclo == "")
                                    {
                                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        graph.DrawString("* NO EXISTEN ALARMAS REGISTRADAS", fontDos, XBrushes.Black, new XRect(340, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                                    }
                                    else
                                    {
                                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);
                                    }



                                    if (q.IdAutoclave.Trim() == n.Matricula.Trim())
                                    {
                                        var actual = Regex.Replace(n.UltimoCiclo, "\\d+",
                                   m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length))); // le resto uno al valor del ciclo actual y obtengo el anterior
                                        string ciclo = n.Matricula.Trim() + actual + ".LOG";
                                        string cicloPDF = n.Matricula.Trim() + actual + ".PDF";
                                        string rutaAbsolutaLog = n.RutaSalida.Trim() + ciclo;
                                        string rutaAbsoluta = n.RutaSalidaPDF.Trim() + cicloPDF;

                                        if (File.Exists(rutaAbsoluta))
                                        {
                                            if (n.Estado == false) { _log.WriteLog("AutoClave " + n.Matricula + " Desactivado "); }
                                            _log.WriteLog("El archivo ya fue impreso en PDF por Automatico :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                        }

                                        else
                                        {
                                            _log.WriteLog("IMPRIMIENDO PDF :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);

                                            pdf.Save(rutaAbsoluta);
                                            System.Threading.Thread.Sleep(1000);
                                            // _print.Printer(rutaAbsoluta, impresora);

                                            System.Threading.Thread.Sleep(1000);
                                        }
                                    }







                                }

                            }
                            
                        }
                        catch { }

                    }




                }
            }
        }
    }
}
