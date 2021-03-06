﻿using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Output;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HojaResumen.Servicios.PrinterProgramas
{
    public class PrinterDosTresCuatro : IPrinterDosTresCuatro
    {
        ILog _log = new ProductionLog();
        public void printDosTresCuatro(string impresora)
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

                        //  foreach (var t in ListaAutoclaves)
                        foreach (var i in ListaAutoclaves)
                        {

                    foreach (var n in context.MaestroAutoclave)
                    {


                        try
                        {
                            var actualx = Regex.Replace(n.UltimoCiclo, "\\d+",
                              m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length))); // le resto uno al valor del ciclo actual y obtengo el anterior
                            var num = Convert.ToInt32(actualx).ToString();

                            var q = (from x in context.CiclosAutoclaves.Where(x => x.IdAutoclave == i.Autoclave && x.NumeroCiclo==num)

                                         .OrderByDescending(s => s.Id)
                                     select x).First();

                            if (n.Estado == true)
                            {
                                if (q.Programa.Trim().Equals("2") || q.Programa.Trim().Equals("3") || q.Programa.Trim().Equals("4"))
                                {



                                    PrintDocument _pr = new PrintDocument();
                                    PrintController _controller = new StandardPrintController();
                                    PrinterSettings _newSettings = new System.Drawing.Printing.PrinterSettings();

                                    Font _font = new Font("Verdana", 8, FontStyle.Regular);
                                    Font _fontDos = new Font("verdana", 7, FontStyle.Regular);
                                    Font _negrita = new Font("Verdana", 8, FontStyle.Bold);
                                    SolidBrush _solid = new SolidBrush(Color.Black);

                                    StringFormat _tf = new StringFormat();
                                    StringFormat _td = new StringFormat();
                                    RectangleF _rect = new RectangleF();


                                    _rect = new RectangleF(450, 760, 350, 300);
                                    _tf.Alignment = StringAlignment.Near;
                                    _td.FormatFlags = StringFormatFlags.LineLimit;
                                    _td.Trimming = StringTrimming.Word;
                                    int page = 1;
                                    var width = _pr.DefaultPageSettings.PrintableArea.Width;
                                    var height = _pr.DefaultPageSettings.PrintableArea.Height;

                                    _pr.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

                                    PrintDialog printDialog = new PrintDialog();
                                    printDialog.Document = _pr; //Document property must be set before ShowDialog()

                                    //DialogResult dialogResult = printDialog.ShowDialog();
                                    //if (dialogResult == DialogResult.OK)
                                    //{
                                    //try
                                    //{
                                    //      _pr.PrinterSettings.PrinterName = _newSettings.PrinterName;

                                    //      System.Threading.Thread.Sleep(2000);
                                    //     _pr.PrintController = _controller;
                                    //     _pr.Print(); //start the print
                                    //    _log.WriteLog("Imprimiendo directamente :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                    //    _pr.Dispose();
                                    //}
                                    //catch { _log.WriteLog("Fallo en impresora :" +impresora); }

                                    //}




                                    if (q.IdAutoclave.Trim() == n.Matricula.Trim())
                                    {
                                        var actual = Regex.Replace(n.UltimoCiclo, "\\d+",
                                   m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length))); // le resto uno al valor del ciclo actual y obtengo el anterior
                                        string ciclo = n.Matricula.Trim() + actual + ".LOG";
                                        string cicloPDF = n.Matricula.Trim() + actual + ".PDF";
                                        string rutaAbsolutaLog = n.RutaSalida.Trim() + ciclo;
                                        string rutaAbsoluta = n.RutaSalidaPDF.Trim() + cicloPDF;


                                        if (File.Exists(rutaAbsolutaLog) && !File.Exists(rutaAbsoluta))
                                        {
                                            try
                                            {
                                                //_pr.PrinterSettings.PrinterName = _newSettings.PrinterName;
                                                _pr.PrinterSettings.PrinterName = impresora;

                                                System.Threading.Thread.Sleep(2000);
                                                _pr.PrintController = _controller;
                                                _pr.Print(); //start the print
                                                _log.WriteLog("Imprimiendo directamente :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                                _pr.Dispose();
                                            }
                                            catch { _log.WriteLog("Fallo en impresora :" + impresora); }
                                        }

                                        if (!File.Exists(rutaAbsolutaLog) && !File.Exists(rutaAbsoluta))
                                        {
                                            _log.WriteLog("El Archivo no ha sido Generado por el Autoclave :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                        }

                                        else
                                        {
                                            if (n.Estado == false) { _log.WriteLog("AutoClave " + n.Matricula + " Desactivado "); }
                                            _log.WriteLog("Ya fue Impreso por automatico :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);

                                        }
                                    }













                                    void printDoc_PrintPage(object sender, PrintPageEventArgs e)
                                    {
                                        Graphics graph = e.Graphics;


                                        graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                                        graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, width, height), _tf);
                                        graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, width, height), _tf);
                                        graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, width, height), _tf);
                                        graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, width, height), _tf);
                                        graph.DrawString("Por: Automático ", _font, _solid, new RectangleF(580, 20, width, height), _tf);

                                        graph.DrawString("PROGRAMA:", _font, _solid, new RectangleF(20, 30, width, height), _tf);
                                        graph.DrawString(q.Programa + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 30, width, height), _tf);
                                        graph.DrawString(q.IdSeccion, _font, _solid, new RectangleF(20, 45, width, height), _tf);

                                        graph.DrawString("PROGRAMADOR:", _font, _solid, new RectangleF(20, 70, width, height), _tf);
                                        graph.DrawString(q.Programador, _font, _solid, new RectangleF(180, 70, width, height), _tf);
                                        graph.DrawString("OPERADOR:", _font, _solid, new RectangleF(20, 85, width, height), _tf);
                                        graph.DrawString(q.Operador, _font, _solid, new RectangleF(180, 85, width, height), _tf);
                                        graph.DrawString("CODIGO PRODUCTO:", _font, _solid, new RectangleF(20, 100, width, height), _tf);
                                        graph.DrawString(q.CodigoProducto + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 100, width, height), _tf);
                                        graph.DrawString("N.LOTE:", _font, _solid, new RectangleF(20, 115, width, height), _tf);
                                        graph.DrawString(q.Lote + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 115, width, height), _tf);
                                        graph.DrawString("ID. MAQUINA:", _font, _solid, new RectangleF(20, 130, width, height), _tf);
                                        graph.DrawString(q.IdAutoclave + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 130, width, height), _tf);
                                        graph.DrawString("NOTAS:", _font, _solid, new RectangleF(20, 145, width, height), _tf);
                                        graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 145, width, height), _tf);

                                        graph.DrawString("MODELO:", _font, _solid, new RectangleF(20, 170, width, height), _tf);
                                        graph.DrawString(q.Modelo, _font, _solid, new RectangleF(180, 170, width, height), _tf);
                                        graph.DrawString("N.PROGRESIVO:", _font, _solid, new RectangleF(20, 185, width, height), _tf);
                                        graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 185, width, height), _tf);

                                        graph.DrawString("FASE 1:  " + q.Fase1, _font, _solid, new RectangleF(20, 210, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 210, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF1 + " " + "min.s", _font, _solid, new RectangleF(420, 210, width, height), _tf);

                                        graph.DrawString("FASE 2:  " + q.Fase2, _font, _solid, new RectangleF(20, 230, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 230, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF2 + " " + "min.s", _font, _solid, new RectangleF(420, 230, width, height), _tf);

                                        graph.DrawString("FASE 3:  " + q.Fase3, _font, _solid, new RectangleF(20, 250, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 250, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF3 + " " + "min.s", _font, _solid, new RectangleF(420, 250, width, height), _tf);

                                        graph.DrawString("FASE 4:  " + q.Fase4, _font, _solid, new RectangleF(20, 270, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 270, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF4 + " " + "min.s", _font, _solid, new RectangleF(420, 270, width, height), _tf);

                                        graph.DrawString("FASE 5:  " + q.Fase5, _font, _solid, new RectangleF(20, 295, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 295, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF5, _negrita, _solid, new RectangleF(420, 295, width, height), _tf);
                                        graph.DrawString("min.s", _font, _solid, new RectangleF(460, 295, width, height), _tf);
                                        graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 295, width, height), _tf);

                                        graph.DrawString("I " + " " + q.TInicio, _negrita, _solid, new RectangleF(600, 295, width, height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 315, width, height), _tf);
                                        graph.DrawString(q.TIF5, _font, _solid, new RectangleF(300, 315, width, height), _tf);
                                        graph.DrawString(q.TISubF5, _font, _solid, new RectangleF(600, 315, width, height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 335, width, height), _tf);
                                        //graph.DrawString(q.TFF5, font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        graph.DrawString(q.TFF5.Substring(0, 6), _font, _solid, new RectangleF(300, 335, width, height), _tf);
                                        graph.DrawString(q.TFF5.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 335, width, height), _tf);
                                        graph.DrawString(q.TFF5.Substring(12), _font, _solid, new RectangleF(370, 335, width, height), _tf);
                                        graph.DrawString(q.TFSubF5, _font, _solid, new RectangleF(600, 335, width, height), _tf);

                                        graph.DrawString("FASE 6:  " + q.Fase6, _font, _solid, new RectangleF(20, 360, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 360, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF6, _negrita, _solid, new RectangleF(420, 360, width, height), _tf);
                                        graph.DrawString("min.s", _font, _solid, new RectangleF(460, 360, width, height), _tf);
                                        graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 360, width, height), _tf);

                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 380, width, height), _tf);
                                        //graph.DrawString(q.TIF6, font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                                        graph.DrawString(q.TIF6.Substring(0, 6), _font, _solid, new RectangleF(300, 380, width, height), _tf);
                                        graph.DrawString(q.TIF6.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 380, width, height), _tf);
                                        graph.DrawString(q.TIF6.Substring(12), _font, _solid, new RectangleF(370, 380, width, height), _tf);
                                        graph.DrawString(q.TISubF6, _font, _solid, new RectangleF(600, 380, width, height), _tf);

                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 400, width, height), _tf);
                                        graph.DrawString(q.TFF6.Substring(0, 6), _font, _solid, new RectangleF(300, 400, width, height), _tf);
                                        graph.DrawString(q.TFF6.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 400, width, height), _tf);
                                        graph.DrawString(q.TFF6.Substring(12), _font, _solid, new RectangleF(370, 400, width, height), _tf);
                                        graph.DrawString(q.TFSubF6.Substring(0, 2), _font, _solid, new RectangleF(600, 400, width, height), _tf);
                                        graph.DrawString(q.TFSubF6.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 400, width, height), _tf);

                                        graph.DrawString("FASE 7:  " + q.Fase7, _font, _solid, new RectangleF(20, 425, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 425, width, height), _tf);

                                        graph.DrawString(q.DuracionTotalF7, _font, _solid, new RectangleF(420, 425, width, height), _tf);
                                        graph.DrawString("min.s", _font, _solid, new RectangleF(460, 425, width, height), _tf);
                                        graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 425, width, height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 445, width, height), _tf);
                                        graph.DrawString(q.TIF7.Substring(0, 6), _font, _solid, new RectangleF(300, 445, width, height), _tf);
                                        graph.DrawString(q.TIF7.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 445, width, height), _tf);
                                        graph.DrawString(q.TIF7.Substring(12), _font, _solid, new RectangleF(370, 445, width, height), _tf);
                                        graph.DrawString(q.TISubF7, _font, _solid, new RectangleF(600, 445, width, height), _tf);


                                        graph.DrawString("FASE 8:  " + q.Fase8, _font, _solid, new RectangleF(20, 470, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 470, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF8 + " " + "min.s", _font, _solid, new RectangleF(420, 470, width, height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 490, width, height), _tf);
                                        graph.DrawString(q.TIF8, _font, _solid, new RectangleF(300, 490, width, height), _tf);
                                        graph.DrawString(q.TISubF8, _font, _solid, new RectangleF(600, 490, width, height), _tf);
                                        graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 510, width, height), _tf);
                                        graph.DrawString(q.TFF8, _font, _solid, new RectangleF(300, 510, width, height), _tf);

                                        graph.DrawString("FASE 9:  " + q.Fase9, _font, _solid, new RectangleF(20, 535, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 535, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF9 + " " + "min.s", _font, _solid, new RectangleF(420, 535, width, height), _tf);

                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 555, width, height), _tf);
                                        graph.DrawString(q.TIF9, _font, _solid, new RectangleF(300, 555, width, height), _tf);
                                        graph.DrawString(q.TISubF9, _font, _solid, new RectangleF(600, 555, width, height), _tf);

                                        graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 575, width, height), _tf);
                                        graph.DrawString(q.TFF9, _font, _solid, new RectangleF(300, 575, width, height), _tf);

                                        graph.DrawString("FASE 10: " + q.Fase10, _font, _solid, new RectangleF(20, 600, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 600, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF10 + " " + "min.s", _font, _solid, new RectangleF(420, 600, width, height), _tf);


                                        graph.DrawString("FASE 11: " + q.Fase11, _font, _solid, new RectangleF(20, 625, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 625, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF11 + " " + "min.s", _font, _solid, new RectangleF(420, 625, width, height), _tf);

                                        graph.DrawString("FASE 12: " + q.Fase12, _font, _solid, new RectangleF(20, 650, width, height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 650, width, height), _tf);
                                        graph.DrawString(q.DuracionTotalF12 + " " + "min.s", _font, _solid, new RectangleF(420, 650, width, height), _tf);

                                        graph.DrawString("FASE 13: FIN DE CICLO         TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10", _font, _solid, new RectangleF(20, 675, width, height), _tf);
                                        graph.DrawString(q.TFF13.Substring(0, 6), _font, _solid, new RectangleF(20, 695, width, height), _tf);
                                        graph.DrawString(q.TiempoCiclo, _font, _solid, new RectangleF(80, 695, width, height), _tf);
                                        graph.DrawString(q.TFF13.Substring(6), _font, _solid, new RectangleF(200, 695, width, height), _tf);
                                        graph.DrawString(q.TFSubF13.Substring(0, 2), _font, _solid, new RectangleF(600, 695, width, height), _tf);
                                        graph.DrawString(q.TFSubF13.Substring(2), _font, _solid, new RectangleF(605, 695, width, height), _tf);

                                        graph.DrawString("HORA COMIEN.PROGR :", _font, _solid, new RectangleF(20, 740, width, height), _tf);
                                        graph.DrawString(q.HoraInicio, _negrita, _solid, new RectangleF(200, 740, width, height), _tf);
                                        graph.DrawString("HORA FIN.PROGR :", _font, _solid, new RectangleF(20, 760, width, height), _tf);
                                        graph.DrawString(q.HoraFin, _negrita, _solid, new RectangleF(200, 760, width, height), _tf);


                                        if (q.EsterilizacionN == "")
                                        {
                                            graph.DrawString("ESTERILIZACION:", _font, _solid, new RectangleF(20, 780, width, height), _tf);
                                            graph.DrawString("FALLIDA", _font, _solid, new RectangleF(200, 780, width, height), _tf);
                                        }
                                        else
                                        {
                                            graph.DrawString("ESTERILIZACION N.:", _font, _solid, new RectangleF(20, 780, width, height), _tf);
                                            graph.DrawString(q.EsterilizacionN, _font, _solid, new RectangleF(200, 780, width, height), _tf);

                                        }

                                        graph.DrawString("TEMP.MIN.ESTERILIZACION:", _font, _solid, new RectangleF(20, 800, width, height), _tf);
                                        graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 800, width, height), _tf);
                                        graph.DrawString(q.TMinima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 800, width, height), _tf);
                                        graph.DrawString("TEMP.MAX.ESTERILIZACION:", _font, _solid, new RectangleF(20, 820, width, height), _tf);
                                        graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 820, width, height), _tf);
                                        graph.DrawString(q.TMaxima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 820, width, height), _tf);

                                        graph.DrawString("DURACION FASE DE ESTER.:", _font, _solid, new RectangleF(20, 840, width, height), _tf);
                                        graph.DrawString(q.DuracionTotal + " " + "min.s", _font, _solid, new RectangleF(200, 840, width, height), _tf);
                                        graph.DrawString("F(T,z) MIN.:", _font, _solid, new RectangleF(20, 860, width, height), _tf);
                                        graph.DrawString(q.FtzMin, _font, _solid, new RectangleF(200, 860, width, height), _tf);
                                        graph.DrawString("F(T,z) MAX.:", _font, _solid, new RectangleF(20, 880, width, height), _tf);
                                        graph.DrawString(q.FtzMax, _font, _solid, new RectangleF(200, 880, width, height), _tf);
                                        graph.DrawString("Dif F(T,z):", _font, _solid, new RectangleF(20, 900, width, height), _tf);
                                        graph.DrawString(q.DifMaxMin, _font, _solid, new RectangleF(200, 900, width, height), _tf);
                                        graph.DrawString(q.AperturaPuerta, _font, _solid, new RectangleF(20, 920, width, height), _tf);
                                        graph.DrawString("FIRMA OPERADOR        _______________________ ", _font, _solid, new RectangleF(20, 960, width, height), _tf);
                                        graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", _font, _solid, new RectangleF(20, 1020, width, height), _tf);
                                        // graph.DrawString("Pág 1 de 1  ", _font, _solid, new RectangleF(640, 1120, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("Pág 1 de 1  ", _font, _solid, new RectangleF(710, 1050, width, height), _tf);

                                        if (q.ErrorCiclo == "")
                                        {
                                            //graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                            //graph.DrawString("* NO EXISTEN ALARMAS REGISTRADAS", _fontDos, _solid, new RectangleF(450, 760, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        }
                                        else
                                        {
                                            string[] error = q.ErrorCiclo.Split('\n');
                                            if (error.Count() > 24)
                                            {
                                                if (page == 1)
                                                {
                                                    graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, width, height), _tf);
                                                    graph.DrawString("Pág 1 de 2  ", _font, _solid, new RectangleF(710, 1050, width, height), _tf);
                                                    e.HasMorePages = true;
                                                    for (int y = 0; y < error.Length; y++)
                                                    {
                                                        if (y >= 0 && y <= 24)
                                                        {
                                                            graph.DrawString(error[y], _fontDos, _solid, new RectangleF(450, 750 + y * 10, width, height), _tf);

                                                        }
                                                    }

                                                }
                                                if (page == 2)
                                                {
                                                    e.Graphics.Clear(Color.White);
                                                    graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, width, height), _tf);
                                                    graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, width, height), _tf);
                                                    graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, width, height), _tf);
                                                    graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, width, height), _tf);
                                                    graph.DrawString("Por: Automático ", _font, _solid, new RectangleF(580, 20, width, height), _tf);

                                                    graph.DrawString("Pág 2 de 2  ", _font, _solid, new RectangleF(710, 1050, width, height), _tf);
                                                    e.HasMorePages = false;
                                                    for (int y = 0; y < error.Length; y++)
                                                    {

                                                        if (y > 24)
                                                        {

                                                            graph.DrawString(error[y], _fontDos, _solid, new RectangleF(50, 30 + (y - 25) * 10, width, height), _tf);

                                                        }
                                                    }
                                                }
                                                page++;
                                            }
                                            else
                                            {
                                                graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, width, height), _tf);

                                                graph.DrawString(q.ErrorCiclo, _fontDos, _solid, _rect, _td);
                                            }

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

