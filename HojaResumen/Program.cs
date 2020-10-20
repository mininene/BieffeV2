using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.ApiConnect;
using HojaResumen.Servicios.Parser;
using HojaResumen.Servicios.PDFCreator;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HojaResumen
{
    class Program
    {



        static void Main(string[] args)
        {




            ////Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();

            ////string[] Ids = new string[] { "NF8387A02443", "8388B24068", "8389C22575", "8607D", "NF1029E", "NF1030F", "NF1031G", "NA0658EGH", "NA0672EGI", "0827J", "0828K", "1167L", "NA0611EFM" };

            ////string[] Id =new string[] { Ids[0], Ids[1], Ids[2] };
            ////string[] prefijo = new string[] { Id[0].Substring(0, 7), Id[1].Substring(0,5), Id[2].Substring(0,5) };
            ////string[] ciclo = new string[] { Id[0].Substring(7), Id[1].Substring(5), Id[2].Substring(5) };
            ////string[] last =new string[] { prefijo[0] + ciclo[0], prefijo[1]+ciclo[1], prefijo[2] + ciclo[2] };


            ////int i = 0;
            ////for (i = 0; i <= 1; i++)
            ////{
            ////    var actualA = Regex.Replace(ciclo[0], "\\d+",
            ////    m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

            ////    var actualB = Regex.Replace(ciclo[1], "\\d+",
            ////    m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

            ////    var actualC = Regex.Replace(ciclo[2], "\\d+",
            ////  m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

            ////    string remoteA = prefijo[0] + actualA + ".LOG";
            ////    string remoteB = prefijo[1] + actualB + ".LOG";
            ////    string remoteC = prefijo[2] + actualC + ".LOG";








            ////    var ListaDatos = new List<ConnectionData>
            //// {

            ////     new ConnectionData { Version=4401, Ip="10.109.80.81", Remote=remoteA, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveA\\"+ remoteA+".txt"},
            ////     new ConnectionData { Version=4002, Ip="10.109.80.82", Remote=remoteB, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveB\\"+ remoteB+".txt"},
            ////     new ConnectionData { Version=37, Ip="10.109.80.83", Remote=remoteC, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveC\\"+ remoteC+".txt" },

            ////    //new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02443.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveA_02443.txt" },
            ////    //new ConnectionData { Version=4002, Ip="10.109.80.89", Remote="NA0672EGI08871.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveI_08871.txt" },
            ////    //new ConnectionData { Version=4002, Ip="10.109.80.94", Remote="NA0658EGH13977.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveH_13977.txt" },
            ////    //new ConnectionData { Version=4002, Ip="10.109.80.93", Remote="NA0611EFM05014.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveM_05014.txt" },
            ////    //new ConnectionData { Version=4002, Ip="10.109.80.90", Remote="0827J07176.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveJ.txt" },
            ////    //new ConnectionData { Version=4002, Ip="10.109.80.91", Remote="0828K10597.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveK.txt" },
            ////    //new ConnectionData { Version=4001, Ip="10.109.80.92", Remote="1167L20752.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveL.txt" },

            //// };



            ////    foreach (var item in ListaDatos)
            ////    {
            ////        Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
            ////        System.Threading.Thread.Sleep(1000);

            ////        Console.WriteLine(item.Remote);
            ////        //get Version
            ////        float version = con.get_version();
            ////        Console.WriteLine(version);

            ////        //session
            ////        uint handle = con.ConnectSession(item.Version, item.Ip);
            ////        Console.WriteLine("Valor de Sesion: " + handle);

            ////        // Connection
            ////        uint lhandle = con.ConnectApi(handle);
            ////        Console.WriteLine("Valor de Conexion: " + lhandle);

            ////        //GetFile
            ////        uint result = con.GetData(handle, item.Remote, item.Local);

            ////        Console.WriteLine("Resultado de getData: " + result);

            ////        //LastError
            ////        uint lasterror = con.GetError();
            ////        Console.WriteLine("Valor Error: " + lasterror);

            ////        //Close Connection
            ////        Console.WriteLine("Cerrar Conexion valor: " + con.CloseConnection(ref handle));
            ////        Console.WriteLine("\n\n");
            ////        System.Threading.Thread.Sleep(10000);
            ////    }
            ////}








            //string cabecera = "NF8387A";
            //int i = 0;
            //int ciclo = 2443;
            //string last = cabecera + ciclo;
            //if (last.Equals(cabecera + ciclo))
            //{

            //    string NuevoCiclo = last + i++;
            //    last = NuevoCiclo;
            //    Console.WriteLine(last);

            //}
            //else { Console.WriteLine("Imprimir"); }

            //string prefijo = "NF8387A";
            //int proximociclo = 02443;

            //string com = prefijo + proximociclo;



            //Console.WriteLine(com);



            // Console.WriteLine(x.Length);



            IApiConnect con = new ApiConnect();
            con.ConnectApi();


            //IParser GetData = new Parser();
            //GetData.ParserFile();

            //IParserSabiDos GetDataSabiDos = new ParserSabiDos();
            //GetDataSabiDos.ParserSabiDosFile();


            //Console.WriteLine("Escribiendo en la base de datos");


            //System.Threading.Thread.Sleep(1000);
            //ICreator Create = new Creator();
            //Create.CreatePdf();

            //ICreator createA = new CreatorNF8387A();
            //createA.creatorNF8387A();


            //System.Threading.Thread.Sleep(1000);
            //ICreatorSabiDos CreateDos = new CreatorSabiDos();
            //CreateDos.CreateSabiDosPDF();

            //Console.WriteLine("PDF Generado");


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





            Console.ReadKey();
            }
        }
    }

