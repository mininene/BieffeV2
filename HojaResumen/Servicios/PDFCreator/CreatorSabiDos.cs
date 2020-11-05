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


                        if (q.Programa.Trim().Equals("9") || q.Programa.Trim().Equals("10"))
                        {

                            //foreach (var q in query)

                            //{


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


                            rect = new XRect(340, 520, 250, 220);
                            graph.DrawRectangle(XBrushes.White, rect);
                            tf.Alignment = XParagraphAlignment.Justify;




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

                            graph.DrawString("FASE 2:  " + q.Fase2, font, XBrushes.Black, new XRect(20, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF2, negrita, XBrushes.Black, new XRect(320, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TInicio, negrita, XBrushes.Black, new XRect(460, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF2, font, XBrushes.Black, new XRect(230, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TISubF2, font, XBrushes.Black, new XRect(460, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF2.Substring(0, 6), font, XBrushes.Black, new XRect(230, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF2.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF2.Substring(11), font, XBrushes.Black, new XRect(275, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFSubF2, font, XBrushes.Black, new XRect(460, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 3:  " + q.Fase3, font, XBrushes.Black, new XRect(20, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF3, negrita, XBrushes.Black, new XRect(320, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF3.Substring(0, 6), font, XBrushes.Black, new XRect(230, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF3.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF3.Substring(11), font, XBrushes.Black, new XRect(275, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TISubF3, font, XBrushes.Black, new XRect(460, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF3.Substring(0, 6), font, XBrushes.Black, new XRect(230, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF3.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFF3.Substring(11), font, XBrushes.Black, new XRect(275, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFSubF3.Substring(0, 2), font, XBrushes.Black, new XRect(460, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TFSubF3.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 4:  " + q.Fase4, font, XBrushes.Black, new XRect(20, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(320, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF4, font, XBrushes.Black, new XRect(230, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TISubF4, font, XBrushes.Black, new XRect(460, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 5:  " + q.Fase5, font, XBrushes.Black, new XRect(20, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF5 + " " + "min.s", font, XBrushes.Black, new XRect(320, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 6:  " + q.Fase6, font, XBrushes.Black, new XRect(20, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF6 + " " + "min.s", font, XBrushes.Black, new XRect(320, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 7:  " + q.Fase7A, font, XBrushes.Black, new XRect(20, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(320, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 8:  " + q.Fase8A, font, XBrushes.Black, new XRect(20, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(320, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 7:  " + q.Fase7B, font, XBrushes.Black, new XRect(20, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(320, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 8:  " + q.Fase8B, font, XBrushes.Black, new XRect(20, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(320, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("FASE 9:  " + q.Fase9, font, XBrushes.Black, new XRect(20, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(320, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
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

                            graph.DrawString("FASE 12: FIN DE CICLO         TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF12.Substring(0, 6) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(20, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TIF12.Substring(6), font, XBrushes.Black, new XRect(160, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TISubF12.Substring(0, 2), font, XBrushes.Black, new XRect(460, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TISubF12.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                            graph.DrawString("HORA COMIEN.PROGR :", font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.HoraInicio, negrita, XBrushes.Black, new XRect(160, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("HORA FIN.PROGR :", font, XBrushes.Black, new XRect(20, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.HoraFin, negrita, XBrushes.Black, new XRect(160, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("TEMP.MIN.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TMinima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("TEMP.MAX.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.TMaxima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                            graph.DrawString("DURACION FASE DE ESTER.:", font, XBrushes.Black, new XRect(20, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(160, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("F(T,z) MIN.:", font, XBrushes.Black, new XRect(20, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.FtzMin, font, XBrushes.Black, new XRect(160, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("F(T,z) MAX.:", font, XBrushes.Black, new XRect(20, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.FtzMax, font, XBrushes.Black, new XRect(160, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("Dif F(T,z):", font, XBrushes.Black, new XRect(20, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.DifMaxMin, font, XBrushes.Black, new XRect(160, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 645, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 685, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 725, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //graph.DrawString("FASE 1:  " + q.Fase1 + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF1 + " " + "min.s", font, XBrushes.Black, new XRect(20, 145, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 2:  " + q.Fase2 + "                                            " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF2 + " " + "min.s" + "          " + q.TInicio + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 160, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF2 + "      " + q.TISubF2, font, XBrushes.Black, new XRect(20, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF2 + "      " + q.TFSubF2, font, XBrushes.Black, new XRect(20, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("FASE 3:  " + q.Fase3 + "                                            " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF3 + " " + "min.s" + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF3 + "      " + q.TISubF3, font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TFF3 + "      " + q.TFSubF3, font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 4:  " + q.Fase4 + "                                   " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF4 + "      " + q.TISubF4, font, XBrushes.Black, new XRect(20, 265, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //graph.DrawString("FASE 5:  " + q.Fase5 + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF5 + " " + "min.s", font, XBrushes.Black, new XRect(20, 280, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 6:  " + q.Fase6 + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF6 + " " + "min.s", font, XBrushes.Black, new XRect(20, 295, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("FASE 7:  " + q.Fase7A + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(20, 310, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("FASE 8:  " + q.Fase8A + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(20, 325, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 7:  " + q.Fase7B + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF7A + " " + "min.s", font, XBrushes.Black, new XRect(20, 340, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 8:  " + q.Fase8B + "                       " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF8A + " " + "min.s", font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //graph.DrawString("FASE 9:  " + q.Fase9 + "                 " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF9 + "      " + q.TISubF9, font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("DATOS FINALES DE FASE: " + q.TFF9, font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("FASE 10: " + q.Fase10 + "              " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("FASE 11: " + q.Fase11 + "                " + "DURAC.TOTAL FASE:  " + q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FASE 12: " + q.Fase12 , font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10   " + q.TIF12 + "      " + q.TISubF12 + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                            //    graph.DrawString("HORA COMIEN.PROGR : " + "               " + q.HoraInicio, font, XBrushes.Black, new XRect(20, 480, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("HORA FIN.PROGR : " + "                      " + q.HoraFin, font, XBrushes.Black, new XRect(20, 495, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("ESTERILIZACION N. : " + "                   " + q.EsterilizacionN, font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TEMP.MIN.ESTERILIZACION: " + "        " + "°C  " + q.TMinima + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("TEMP.MAX.ESTERILIZACION: " + "        " + "°C  " + q.TMaxima + "     " + "[  ]", font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("DURACION FASE DE ESTER.: " + "        " + q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(20, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("F(T,z) MIN.: " + "                                " + q.FtzMin, font, XBrushes.Black, new XRect(20, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("F(T,z) MAX.: " + "                               " + q.FtzMax, font, XBrushes.Black, new XRect(20, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("Dif F(T,z) : " + "                                  " + q.DifMaxMin, font, XBrushes.Black, new XRect(20, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 645, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 675, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            //  graph.DrawString("Registrado  " + q.FechaRegistro, font, XBrushes.Black, new XRect(400, 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            // graph.DrawString("Impreso  " + DateTime.Now, font, XBrushes.Black, new XRect(400, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            //tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);

                            if (q.ErrorCiclo == "")
                            {
                                graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                graph.DrawString("* NO EXISTEN ALARMAS REGISTRADAS", fontDos, XBrushes.Black, new XRect(340, 520, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                            }
                            else
                            {
                                graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);
                            }



                            string nombreArchivo = "AutoClave" + q.IdAutoclave + q.NumeroCiclo + ".pdf";

                            if (q.IdAutoclave == "0827J")
                            {
                                string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveJ";
                                nombreArchivo = Path.GetFileName(nombreArchivo);
                                rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                                if (File.Exists(rutaAbsoluta))
                                {
                                    Console.WriteLine("No sobreescribir archivo");
                                }

                                else
                                {
                                    pdf.Save(rutaAbsoluta);
                                    System.Threading.Thread.Sleep(1000);
                                }
                            }

                            if (q.IdAutoclave == "0828K")
                            {
                                string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveK";
                                nombreArchivo = Path.GetFileName(nombreArchivo);
                                rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                                if (File.Exists(rutaAbsoluta))
                                {
                                    Console.WriteLine("No sobreescribir archivo");
                                }

                                else
                                {
                                    pdf.Save(rutaAbsoluta);
                                    System.Threading.Thread.Sleep(1000);
                                }
                            }

                            if (q.IdAutoclave == "1167L")
                            {
                                string rutaAbsoluta = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveL";
                                nombreArchivo = Path.GetFileName(nombreArchivo);
                                rutaAbsoluta = Path.Combine(rutaAbsoluta, nombreArchivo);
                                if (File.Exists(rutaAbsoluta))
                                {
                                    Console.WriteLine("No sobreescribir archivo");
                                }

                                else
                                {
                                    pdf.Save(rutaAbsoluta);
                                    System.Threading.Thread.Sleep(1000);
                                }
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

                    }
                    catch { Console.WriteLine("Registros no existentes"); }

                }

            }



        }
    }
}
