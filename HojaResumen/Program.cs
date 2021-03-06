﻿using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.ApiConnect;
using HojaResumen.Servicios.Output;
using HojaResumen.Servicios.Parser;
using HojaResumen.Servicios.PDFCreator;
using HojaResumen.Servicios.Printer;
using HojaResumen.Servicios.PrinterEx;
using HojaResumen.Servicios.PrinterProgramas;
using HojaResumen.WindowsService;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Topshelf;

namespace HojaResumen
{
    class Program
    {












        static void Main(string[] args)
        {
            ILog _log = new ProductionLog();
            try
            {
                ConfigureService.Configure();
            }
            catch { _log.WriteLog("No puede COnectar a la base de datos" ); }

            //HostFactory.Run(hostConfig =>
            //   {
            //       hostConfig.Service<WindowsServiceHR>(serviceConfig =>
            //       {
            //           serviceConfig.ConstructUsing(() => new WindowsServiceHR());
            //           serviceConfig.WhenStarted(s => s.Start());
            //           serviceConfig.WhenStopped(s => s.Stop());
            //           serviceConfig.WhenPaused(s => s.Pause());
            //           serviceConfig.WhenContinued(s => s.Continue());
            //           //hostConfig.StartAutomatically();
            //       });

            //       hostConfig.RunAsLocalSystem();
            //       hostConfig.SetServiceName("MyWindowServiceWithTopshelf");
            //       hostConfig.SetDisplayName("MyWindowServiceWithTopshelf");
            //       hostConfig.SetDescription("My .Net windows service with Topshelf");

            //   });









            //    ILog _log = new ProductionLog();
            //    IPrinterPrOchoVeinte _pr820 = new PrinterOchoVeinte();
            //    IPrinterDosTresCuatro _pr234 = new PrinterDosTresCuatro();
            //    IPrinterNueveDiez _pr910 = new PrinterNueveDiez();




            //using (var context = new CicloAutoclave())

            //{
            //    foreach (var p in context.Parametros)
            //    {



            //        do
            //        {
            //            try
            //            {
            //                //string impresoraSabiUno = p.ImpresoraSabiUno;
            //                //string impresoraSabiDos = p.ImpresoraSabiDos;
            //                //int _timeOrigin = p.Tiempo;
            //                //int _time = p.Tiempo * 60000;

            //                //var connect = new ApiConnect();
            //                //connect.ConnectTHLog();
            //                //Console.WriteLine("Iniciado agua");
            //                //IParserAgua agua = new ParserAgua();
            //                //agua.ParserWater();

            //                //System.Threading.Thread.Sleep(3000);
            //                //Console.WriteLine("Iniciado vapor");
            //                //IParserVapor vapor = new ParserVapor();
            //                //vapor.ParserVapor();

            //                //IParser GetData = new Parser();
            //                //GetData.ParserFile();

            //                //IParserSabiDos GetDataSabiDos = new ParserSabiDos();
            //                //GetDataSabiDos.ParserSabiDosFile();

            //                //_log.WriteLog("Impresion directa 8 y 20");
            //                //_pr820.printOchoVeinte(impresoraSabiUno);
            //                //System.Threading.Thread.Sleep(1000);
            //                //_log.WriteLog("Impresion directa 2,3,4");
            //                //_pr234.printDosTresCuatro(impresoraSabiUno);
            //                //System.Threading.Thread.Sleep(1000);
            //                //_log.WriteLog("Impresion directa 9 y 10");
            //                //_pr910.printNueveDiez(impresoraSabiDos);
            //                //System.Threading.Thread.Sleep(1000);

            //                //System.Threading.Thread.Sleep(1000);
            //                //ICreator Create = new Creator();
            //                //Create.CreatePdf();

            //                //System.Threading.Thread.Sleep(1000);
            //                //ICreatorAmericano CreateAmericano = new CreatorAmericano();
            //                //CreateAmericano.CreateAmericanoPdf();


            //                //System.Threading.Thread.Sleep(1000);
            //                //ICreatorSabiDos CreateDos = new CreatorSabiDos();
            //                //CreateDos.CreateSabiDosPDF();



            //                //_log.WriteLog("PDF Generados...");
            //                //_log.WriteLog("Ciclo de recoleccion de datos Finalizado...");
            //                //_log.WriteLog("Tiempo de Espera :" + _timeOrigin + "m");
            //                //System.Threading.Thread.Sleep(_time); //1 MINUTOS
            //                //_log.WriteLog("\n\n");
            //            }
            //            catch { /*_log.WriteLog("No se ha podido conectar a la base de datos");*/ }


            //        } while (true);
            //    }
            //}
        }

    }

}
    











//            using (var context = new CicloAutoclave()) //entidad de data entity

//            {

//               Console.WriteLine( context.Database.Connection.ConnectionString);

//}


//string span = "108:46";
//string tf = "13:14";

//var td = TimeSpan.FromMinutes(Convert.ToDouble(span.Split(':')[0])).Add(TimeSpan.FromSeconds(Convert.ToDouble((span.Split(':')[1]))))
//    - TimeSpan.Parse("00:" + tf);             // resta timeSpan
//Console.WriteLine(td);
//var z = TimeSpan.Parse(td.ToString().Substring(0, 5)).TotalMinutes + td.ToString().Substring(5, 3);
//Console.WriteLine(z);