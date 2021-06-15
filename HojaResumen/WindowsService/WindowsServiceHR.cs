using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.ApiConnect;
using HojaResumen.Servicios.Output;
using HojaResumen.Servicios.Parser;
using HojaResumen.Servicios.PDFCreator;
using HojaResumen.Servicios.PrinterProgramas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HojaResumen.WindowsService
{
    public class WindowsServiceHR
    {


        public bool Start()
        {
            // to do other config which run fast

            var myThread = new Thread(new ThreadStart(ForeverStart));
            myThread.IsBackground = true;  // This line will prevent thread from working after service stop.
            myThread.Start();
            return true;
        }

        public void ForeverStart()
        {
            ILog _log = new ProductionLog();
            IPrinterPrOchoVeinte _pr820 = new PrinterOchoVeinte();
            IPrinterDosTresCuatro _pr234 = new PrinterDosTresCuatro();
            IPrinterNueveDiez _pr910 = new PrinterNueveDiez();



             try {
               
                        //do {
                      

                  while (true) {
                     
                        using (var control = new CicloAutoclave()) {

                          foreach (var t in control.Parametros)  {

                            if (t.Reinicio.Value == true)
                            {


                                _log.WriteLog(t.Reinicio.Value.ToString());

                                using (var context = new CicloAutoclave())
                                {



                                    foreach (var p in context.Parametros)
                                    {




                                        string impresoraSabiUno = p.ImpresoraSabiUno;
                                        string impresoraSabiDos = p.ImpresoraSabiDos;
                                        int _timeOrigin = p.Tiempo;
                                        int _time = p.Tiempo * 60000;

                                        IApiConnect connect = new ApiConnect();
                                        connect.ConnectTHLog();


                                        IParser GetData = new Parser();
                                        GetData.ParserFile();

                                        IParserSabiDos GetDataSabiDos = new ParserSabiDos();
                                        GetDataSabiDos.ParserSabiDosFile();

                                        System.Threading.Thread.Sleep(500);
                                        IParserAgua agua = new ParserAgua();
                                        agua.ParserWater();

                                        System.Threading.Thread.Sleep(500);
                                        IParserVapor vapor = new ParserVapor();
                                        vapor.ParserVapor();

                                        _log.WriteLog("Impresion directa 8 y 20");
                                     //   _pr820.printOchoVeinte(impresoraSabiUno);
                                        System.Threading.Thread.Sleep(1000);
                                        _log.WriteLog("Impresion directa 2,3,4");
                                     //   _pr234.printDosTresCuatro(impresoraSabiUno);
                                        System.Threading.Thread.Sleep(1000);
                                        _log.WriteLog("Impresion directa 9 y 10");
                                      //  _pr910.printNueveDiez(impresoraSabiDos);
                                        System.Threading.Thread.Sleep(1000);

                                        System.Threading.Thread.Sleep(1000);
                                        ICreator Create = new Creator();
                                        Create.CreatePdf();

                                        System.Threading.Thread.Sleep(1000);
                                        ICreatorAmericano CreateAmericano = new CreatorAmericano();
                                        CreateAmericano.CreateAmericanoPdf();


                                        System.Threading.Thread.Sleep(1000);
                                        ICreatorSabiDos CreateDos = new CreatorSabiDos();
                                        CreateDos.CreateSabiDosPDF();



                                        _log.WriteLog("PDF Generados...");
                                        _log.WriteLog("Ciclo de recoleccion de datos Finalizado...");
                                        _log.WriteLog("Tiempo de Espera :" + _timeOrigin + "m");
                                        System.Threading.Thread.Sleep(_time); //1 MINUTOS
                                        _log.WriteLog("\n\n");
                                        System.Threading.Thread.Sleep(10000);

                                        try
                                        {
                                            //System.Threading.Thread.Sleep(1000);
                                            //Environment.Exit(0);
                                            Stop();
                                            System.Threading.Thread.Sleep(1000);
                                            Environment.Exit(0);
                                            // System.Threading.Thread.Sleep(10000);

                                            //Application.Restart();
                                            //System.Threading.Thread.Sleep(200);

                                        }
                                        catch { }
                                    }
                                }


                            }
                            else
                            {
                               
                                

                                //IApiConnect connect = null;
                                //_log.WriteLog("Sistema detenido..."); _log.WriteLog("Colocar reinicio en 1 para continuar...");
                                //_log.WriteLog(t.Reinicio.Value.ToString());
                                //System.Diagnostics.Process.Start(Application.ExecutablePath);
                                //System.Threading.Thread.Sleep(2000);
                                //Environment.Exit(0);

                            }







                            /*}*/
                        }
                       


                      }
                   }
            
             }         
              catch { _log.WriteLog("No se ha podido conectar a la base de datos"); }



         }

    

        public void Stop()
        {
            ILog _log = new ProductionLog();
              _log.WriteLog("Deteniendo Servicio de WindowsHojaResumen....");
            _log.WriteLog(" Servicio WindowsHojaResumen Detenido");
        }
    
    }
}
