﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Output
{
    class ProductionLog : ILog
    {
        private readonly string _path;

        public ProductionLog()
        {
            // _path =  AppDomain.CurrentDomain.BaseDirectory + "LogGenerado"; //// imprime dentro de bin/debug
            //_path = AppDomain.CurrentDomain.BaseDirectory + "LogGenerado";
            // _path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "LogGenerado"; //// imprime en el escritorio de cualquier equipo
            // _path = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + "LogGenerado"; //// imprime en el escritorio de cualquier equipo
            _path = @"C:\Users\fuenteI3\Desktop\LogGenerado";


            Directory.CreateDirectory(_path);
        }

        public void WriteLog(string mensaje)
        {
            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
            var hora = DateTime.Now.ToString("HH:mm:ss");

            using (var sw = new StreamWriter(_path + "/HojaResumen"+fecha.ToString().Replace("/","")+".Log", true))
            {
                sw.WriteLine("[" + fecha + " " + hora + "] " + mensaje);
            }
        }
    }
}
