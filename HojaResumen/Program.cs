using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Parser;
using HojaResumen.Servicios.PDFCreator;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen
{
    class Program
    {
        static void Main(string[] args)
        {

            //////////Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();

            //////////var ListaDatos = new List<ConnectionData>
            ////////// {
            //////////    //new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02443.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveA_02443.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.89", Remote="NA0672EGI08871.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveI_08871.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.94", Remote="NA0658EGH13977.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveH_13977.txt" },
            //////////    new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02436.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\AutoClaveAP8500.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.93", Remote="NA0611EFM05014.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveM_05014.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.87", Remote="NF1031G08838.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P20AutoClaveG_05014.txt" },

            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.90", Remote="0827J07176.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveJ.txt" },
            //////////    //new ConnectionData { Version=4002, Ip="10.109.80.91", Remote="0828K10597.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveK.txt" },
            //////////    //new ConnectionData { Version=4001, Ip="10.109.80.92", Remote="1167L20752.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveL.txt" },
            //////////    //new ConnectionData { Version=37, Ip="10.109.80.83", Remote="8389C22567.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveC2.txt" },

            ////////// };



            //////////    foreach (var item in ListaDatos)
            //////////    {
            //////////        System.Threading.Thread.Sleep(1000);

            //////////        float version = con.get_version();
            //////////        Console.WriteLine(version);


            //////////        uint handle = con.ConnectSession(item.Version, item.Ip);


            //////////        Console.WriteLine("Valor de Sesion: " + handle);

            //////////        uint lhandle = con.ConnectApi(handle);
            //////////        Console.WriteLine("Valor de Conexion: " + lhandle);

            //////////        uint result = con.GetData(handle, item.Remote, item.Local);


            //////////        Console.WriteLine("Resultado de getData: " + result);

            //////////        uint lasterror = con.GetError();
            //////////        Console.WriteLine("Valor Error: " + lasterror);

            //////////        Console.WriteLine("Cerrar Conexion valor: " + con.CloseConnection(ref handle));
            //////////    }


            IParser GetData = new Parser();
            GetData.ParserFile();

            IParserSabiDos GetDataSabiDos = new ParserSabiDos();
            GetDataSabiDos.ParserSabiDosFile();


            Console.WriteLine("Escribiendo en la base de datos");
            System.Threading.Thread.Sleep(1000);
            ICreator Create = new Creator();
            Create.CreatePdf();
            System.Threading.Thread.Sleep(1000);
            ICreatorSabiDos CreateDos = new CreatorSabiDos();
            CreateDos.CreateSabiDosPDF();

            Console.WriteLine("PDF Generado");








            //string span = "108:46";
            //string tf = "13:14";

            //var td = TimeSpan.FromMinutes(Convert.ToDouble(span.Split(':')[0])).Add(TimeSpan.FromSeconds(Convert.ToDouble((span.Split(':')[1]))))
            //    - TimeSpan.Parse("00:" + tf);             // resta timeSpan
            //Console.WriteLine(td);
            //var z = TimeSpan.Parse(td.ToString().Substring(0, 5)).TotalMinutes + td.ToString().Substring(5, 3);
            //Console.WriteLine(z);





            Console.ReadKey();
            }
        }
    }

