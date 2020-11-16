using HojaResumen.Modelo;
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
    public class PrinterNueveDiez : IPrinterNueveDiez
    {
        ILog _log = new ProductionLog();
        public void printNueveDiez(string impresora)
        {

           

               

                    using (var context = new CicloAutoclave())

                    {
                        var ListaAutoclaves = new List<IdAutoClaveSabiDos>
                        {
                             new IdAutoClaveSabiDos {Autoclave="0827J"},
                             new IdAutoClaveSabiDos {Autoclave="0828K"},
                             new IdAutoClaveSabiDos {Autoclave="1167L"},
                         };

                        //  foreach (var t in ListaAutoclaves)
                        foreach (var t in ListaAutoclaves)
                        {

                    foreach (var n in context.MaestroAutoclave)
                    {



                        try
                        {
                            var q = (from x in context.CiclosSabiDos.Where(x => x.IdAutoclave == t.Autoclave)
                                .OrderByDescending(s => s.Id)
                                     select x).First();


                            if (n.Estado == true)
                            {

                                if (q.Programa.Trim().Equals("9") || q.Programa.Trim().Equals("10"))
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


                                    _rect = new RectangleF(450, 755, 350, 300);
                                    _tf.Alignment = StringAlignment.Near;
                                    _td.FormatFlags = StringFormatFlags.LineLimit;
                                    _td.Trimming = StringTrimming.Word;

                                    _pr.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

                                    PrintDialog printDialog = new PrintDialog();
                                    printDialog.Document = _pr; //Document property must be set before ShowDialog()

                                    //DialogResult dialogResult = printDialog.ShowDialog();
                                    //if (dialogResult == DialogResult.OK)
                                    //{
                                    //try
                                    //{
                                    //    _pr.PrinterSettings.PrinterName = _newSettings.PrinterName;

                                    //    System.Threading.Thread.Sleep(2000);
                                    //    _pr.PrintController = _controller;
                                    //    _pr.Print(); //start the print
                                    //    _log.WriteLog("Imprimiendo directamente :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                    //    _pr.Dispose();
                                    //}
                                    //catch { _log.WriteLog("Fallo en impresora :" + impresora); }

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
                                               // _pr.PrinterSettings.PrinterName = _newSettings.PrinterName;
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
                                            if (n.Estado == false) { _log.WriteLog("AutoClave " + n.Matricula + " Desactivado"); }
                                            _log.WriteLog("Ya fue Impreso por automatico :" + q.IdAutoclave + q.NumeroCiclo + " " + "PROGRAMA" + " " + q.Programa);
                                           

                                        }
                                    }



















                                    void printDoc_PrintPage(object sender, PrintPageEventArgs e)
                                    {
                                        Graphics graph = e.Graphics;


                                        graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                                        graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("Impreso por: Automático ", _font, _solid, new RectangleF(580, 20, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("PROGRAMA:", _font, _solid, new RectangleF(20, 30, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Programa + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 30, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.IdSeccion, _font, _solid, new RectangleF(20, 45, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("PROGRAMADOR:", _font, _solid, new RectangleF(20, 70, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Programador, _font, _solid, new RectangleF(180, 70, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("OPERADOR:", _font, _solid, new RectangleF(20, 85, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Operador, _font, _solid, new RectangleF(180, 85, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("CODIGO PRODUCTO:", _font, _solid, new RectangleF(20, 100, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.CodigoProducto + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 100, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("N.LOTE:", _font, _solid, new RectangleF(20, 115, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Lote + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 115, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("ID. MAQUINA:", _font, _solid, new RectangleF(20, 130, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.IdAutoclave + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 130, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("NOTAS:", _font, _solid, new RectangleF(20, 145, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 145, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("MODELO:", _font, _solid, new RectangleF(20, 170, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.Modelo, _font, _solid, new RectangleF(180, 170, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("N.PROGRESIVO:", _font, _solid, new RectangleF(20, 185, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 185, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 1:  " + q.Fase1, _font, _solid, new RectangleF(20, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF1 + " " + "min.s", _font, _solid, new RectangleF(420, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 2:  " + q.Fase2, _font, _solid, new RectangleF(20, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF2, _negrita, _solid, new RectangleF(420, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("min.s", _font, _solid, new RectangleF(460, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("< --[  ]", _negrita, _solid, new RectangleF(495, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TInicio, _negrita, _solid, new RectangleF(600, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF2, _font, _solid, new RectangleF(300, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TISubF2, _font, _solid, new RectangleF(600, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF2.Substring(0, 6), _font, _solid, new RectangleF(300, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF2.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF2.Substring(12), _font, _solid, new RectangleF(370, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFSubF2, _font, _solid, new RectangleF(600, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);



                                        graph.DrawString("FASE 3:  " + q.Fase3, _font, _solid, new RectangleF(20, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF3, _negrita, _solid, new RectangleF(420, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("min.s", _font, _solid, new RectangleF(460, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("< --[  ]", _negrita, _solid, new RectangleF(495, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF3.Substring(0, 6), _font, _solid, new RectangleF(300, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF3.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF3.Substring(12), _font, _solid, new RectangleF(370, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFSubF2, _font, _solid, new RectangleF(600, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF3.Substring(0, 6), _font, _solid, new RectangleF(300, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF3.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF3.Substring(12), _font, _solid, new RectangleF(370, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFSubF3.Substring(0, 2), _font, _solid, new RectangleF(600, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFSubF3.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        graph.DrawString("FASE 4:  " + q.Fase4, _font, _solid, new RectangleF(20, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF4 + " " + "min.s", _font, _solid, new RectangleF(420, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF4, _font, _solid, new RectangleF(300, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TISubF4, _font, _solid, new RectangleF(600, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 5: " + q.Fase5, _font, _solid, new RectangleF(20, 405, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 405, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF5 + " " + "min.s", _font, _solid, new RectangleF(420, 405, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 6: " + q.Fase6, _font, _solid, new RectangleF(20, 430, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 430, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF6 + " " + "min.s", _font, _solid, new RectangleF(420, 430, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 7: " + q.Fase7A, _font, _solid, new RectangleF(20, 455, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 455, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF7A + " " + "min.s", _font, _solid, new RectangleF(420, 455, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 8: " + q.Fase8A, _font, _solid, new RectangleF(20, 480, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 480, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF8A + " " + "min.s", _font, _solid, new RectangleF(420, 480, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 7: " + q.Fase7B, _font, _solid, new RectangleF(20, 505, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 505, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF7B + " " + "min.s", _font, _solid, new RectangleF(420, 505, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 8: " + q.Fase8B, _font, _solid, new RectangleF(20, 530, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 530, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF8B + " " + "min.s", _font, _solid, new RectangleF(420, 530, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        graph.DrawString("FASE 9:  " + q.Fase9, _font, _solid, new RectangleF(20, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF9 + " " + "min.s", _font, _solid, new RectangleF(420, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 575, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF9, _font, _solid, new RectangleF(300, 575, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TISubF9, _font, _solid, new RectangleF(600, 575, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 595, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TFF9, _font, _solid, new RectangleF(300, 595, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 10: " + q.Fase10, _font, _solid, new RectangleF(20, 620, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 620, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF10 + " " + "min.s", _font, _solid, new RectangleF(420, 620, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 11: " + q.Fase11, _font, _solid, new RectangleF(20, 645, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 645, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotalF11 + " " + "min.s", _font, _solid, new RectangleF(420, 645, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("FASE 12: FIN DE CICLO         TIEMPO TP  TE2 TE3 TE4 TE9 TE10", _font, _solid, new RectangleF(20, 670, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString(q.TIF12.Substring(0, 6), _font, _solid, new RectangleF(20, 690, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TIF12.Substring(6), _font, _solid, new RectangleF(200, 690, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TISubF12.Substring(0, 2), _font, _solid, new RectangleF(600, 690, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TISubF12.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 690, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        graph.DrawString("HORA COMIEN.PROGR :", _font, _solid, new RectangleF(20, 735, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.HoraInicio, _negrita, _solid, new RectangleF(200, 735, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("HORA FIN.PROGR :", _font, _solid, new RectangleF(20, 755, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.HoraFin, _negrita, _solid, new RectangleF(200, 755, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                                        if (q.EsterilizacionN == "")
                                        {
                                            graph.DrawString("ESTERILIZACION:", _font, _solid, new RectangleF(20, 775, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                            graph.DrawString("FALLIDA", _font, _solid, new RectangleF(200, 775, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        }
                                        else
                                        {
                                            graph.DrawString("ESTERILIZACION N.:", _font, _solid, new RectangleF(20, 775, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                            graph.DrawString(q.EsterilizacionN, _font, _solid, new RectangleF(200, 775, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        }

                                        graph.DrawString("TEMP.MIN.ESTERILIZACION:", _font, _solid, new RectangleF(20, 795, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 795, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TMinima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 795, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("TEMP.MAX.ESTERILIZACION:", _font, _solid, new RectangleF(20, 815, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 815, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.TMaxima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 815, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        graph.DrawString("DURACION FASE DE ESTER.:", _font, _solid, new RectangleF(20, 835, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DuracionTotal + " " + "min.s", _font, _solid, new RectangleF(200, 835, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("F(T,z) MIN.:", _font, _solid, new RectangleF(20, 855, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.FtzMin, _font, _solid, new RectangleF(200, 855, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("F(T,z) MAX.:", _font, _solid, new RectangleF(20, 875, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.FtzMax, _font, _solid, new RectangleF(200, 875, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("Dif F(T,z):", _font, _solid, new RectangleF(20, 895, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.DifMaxMin, _font, _solid, new RectangleF(200, 895, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString(q.AperturaPuerta, _font, _solid, new RectangleF(20, 915, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("FIRMA OPERADOR        _______________________ ", _font, _solid, new RectangleF(20, 955, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                        graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", _font, _solid, new RectangleF(20, 1015, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        if (q.ErrorCiclo == "")
                                        {
                                            graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 735, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                                            graph.DrawString("* NO EXISTEN ALARMAS REGISTRADAS", _fontDos, _solid, new RectangleF(450, 755, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                        }
                                        else
                                        {
                                            graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 735, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                                            graph.DrawString(q.ErrorCiclo, _fontDos, _solid, _rect, _td);

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

