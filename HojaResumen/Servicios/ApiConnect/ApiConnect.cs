﻿using HojaResumen.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.ApiConnect
{
    public class ApiConnect : IApiConnect
    {
        public void ConnectApi()
        {
            Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();

            string[] Ids = new string[] { "NF8387A02443", "8388B24068", "8389C22575", "8607D", "NF1029E", "NF1030F", "NF1031G", "NA0658EGH", "NA0672EGI", "0827J", "0828K", "1167L", "NA0611EFM" };

            string[] Id = new string[] { Ids[0], Ids[1], Ids[2] };
            string[] prefijo = new string[] { Id[0].Substring(0, 7), Id[1].Substring(0, 5), Id[2].Substring(0, 5) };
            string[] ciclo = new string[] { Id[0].Substring(7), Id[1].Substring(5), Id[2].Substring(5) };
            string[] last = new string[] { prefijo[0] + ciclo[0], prefijo[1] + ciclo[1], prefijo[2] + ciclo[2] };


            int i = 0;
            for (i = 0; i <= 1; i++)
            {
                var actualA = Regex.Replace(ciclo[0], "\\d+",
                m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

                var actualB = Regex.Replace(ciclo[1], "\\d+",
                m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

                var actualC = Regex.Replace(ciclo[2], "\\d+",
              m => (int.Parse(m.Value) + i).ToString(new string('0', m.Value.Length)));

                string remoteA = prefijo[0] + actualA + ".LOG";
                string remoteB = prefijo[1] + actualB + ".LOG";
                string remoteC = prefijo[2] + actualC + ".LOG";








                var ListaDatos = new List<ConnectionData>
             {

                 new ConnectionData { Version=4401, Ip="10.109.80.81", Remote=remoteA, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveA\\"+ remoteA+".txt"},
                 new ConnectionData { Version=4002, Ip="10.109.80.82", Remote=remoteB, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveB\\"+ remoteB+".txt"},
                 new ConnectionData { Version=37, Ip="10.109.80.83", Remote=remoteC, Local="C:\\Users\\fuenteI3\\Desktop\\API\\AutoClaveC\\"+ remoteC+".txt" },
             
                //new ConnectionData { Version=4401, Ip="10.109.80.81", Remote="NF8387A02443.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveA_02443.txt" },
                //new ConnectionData { Version=4002, Ip="10.109.80.89", Remote="NA0672EGI08871.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveI_08871.txt" },
                //new ConnectionData { Version=4002, Ip="10.109.80.94", Remote="NA0658EGH13977.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveH_13977.txt" },
                //new ConnectionData { Version=4002, Ip="10.109.80.93", Remote="NA0611EFM05014.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\P8AutoClaveM_05014.txt" },
                //new ConnectionData { Version=4002, Ip="10.109.80.90", Remote="0827J07176.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveJ.txt" },
                //new ConnectionData { Version=4002, Ip="10.109.80.91", Remote="0828K10597.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveK.txt" },
                //new ConnectionData { Version=4001, Ip="10.109.80.92", Remote="1167L20752.LOG", Local="C:\\Users\\fuenteI3\\Desktop\\ReportesGenerados\\AutoClaveL.txt" },
                
             };



                foreach (var item in ListaDatos)
                {
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine(item.Remote);
                    //get Version
                    float version = con.get_version();
                    Console.WriteLine(version);

                    //session
                    uint handle = con.ConnectSession(item.Version, item.Ip);
                    Console.WriteLine("Valor de Sesion: " + handle);

                    // Connection
                    uint lhandle = con.ConnectApi(handle);
                    Console.WriteLine("Valor de Conexion: " + lhandle);

                    //GetFile
                    uint result = con.GetData(handle, item.Remote, item.Local);

                    Console.WriteLine("Resultado de getData: " + result);

                    //LastError
                    uint lasterror = con.GetError();
                    Console.WriteLine("Valor Error: " + lasterror);

                    //Close Connection
                    Console.WriteLine("Cerrar Conexion valor: " + con.CloseConnection(ref handle));
                    Console.WriteLine("\n\n");
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }
    }
}
