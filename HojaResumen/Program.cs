using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.ApiConnect;
using HojaResumen.Servicios.Output;
using HojaResumen.Servicios.Parser;
using HojaResumen.Servicios.PDFCreator;
using HojaResumen.Servicios.Printer;
using HojaResumen.Servicios.PrinterEx;
using HojaResumen.Servicios.PrinterProgramas;
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

namespace HojaResumen
{
    class Program
    {



        static void Main(string[] args)
        {
            ILog _log = new ProductionLog();
            IPrinterPrOchoVeinte _pr820 = new PrinterOchoVeinte();
            IPrinterDosTresCuatro _pr234 = new PrinterDosTresCuatro();
            IPrinterNueveDiez _pr910 = new PrinterNueveDiez();


            string impresora = "";

            using (var context = new CicloAutoclave())

            {
                foreach (var p in context.Parametros)
                {
                    int _time = p.Tiempo * 60000;


                    do
                    {

                        var connect = new ApiConnect();
                        connect.ConnectTHLog();


                        IParser GetData = new Parser();
                        GetData.ParserFile();

                        IParserSabiDos GetDataSabiDos = new ParserSabiDos();
                        GetDataSabiDos.ParserSabiDosFile();

                        _log.WriteLog("Impresion directa 8 y 20");
                        _pr820.printOchoVeinte(impresora);
                        System.Threading.Thread.Sleep(1000);
                        _log.WriteLog("Impresion directa 2,3,4");
                        _pr234.printDosTresCuatro(impresora);
                        System.Threading.Thread.Sleep(1000);
                        _log.WriteLog("Impresion directa 9 y 10");
                        _pr910.printNueveDiez(impresora);
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
                        System.Threading.Thread.Sleep(_time); //1 MINUTOS
                        _log.WriteLog("\n\n");



                    } while (true);
                }
            }
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