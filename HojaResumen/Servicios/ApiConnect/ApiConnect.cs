using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using HojaResumen.Servicios.Output;
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
        
        ILog _log = new ProductionLog();

        public void ConnectTHLog()
        {
            try { 
            using (var context = new CicloAutoclave())
            { //entidad de data entity

                Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();
                _log.WriteLog("++++++++++++++++++++++++Log de Eventos Hoja Resumen++++++++++++++++++++++");
                foreach (var s in context.MaestroAutoclave) // tabla MaestroAutoclave genero otro contexto
                {
                    
                    if (s.Estado == true)
                    {
                                            

                        string ciclo = s.Matricula.Trim() + s.UltimoCiclo.Trim() + ".LOG";
                        string rutaSalida = s.RutaSalida.Trim() + ciclo;


                      
                       
                        _log.WriteLog("Autoclave: " + s.Matricula.Trim() + " Activo");
                        _log.WriteLog(ciclo);
                        System.Threading.Thread.Sleep(1000);

                        //get Version
                        float version = con.get_version();
                         _log.WriteLog(version.ToString());


                        //session
                        uint handle = con.ConnectSession(s.Version, s.IP);
                        if (handle != 0) { _log.WriteLog("Sesion Iniciada Correctamente");  } else { _log.WriteLog("IP o version Incorrecta" + handle);  }
                        //Console.WriteLine("Valor de Sesion: " + handle);


                        //Connection
                        uint lhandle = con.ConnectApi(handle);
                        if (lhandle == 0) { _log.WriteLog("Conexión Establecida"); } else { _log.WriteLog("No se pudo establecer la Conexión - IP o version Incorrecta" + lhandle); }
                        //Console.WriteLine("Valor de Conexion: " + lhandle);


                        //GetFile
                        uint result = con.GetData(handle, ciclo, rutaSalida);
                        if (lhandle == 0)
                        {

                            using (var db = new CicloAutoclave())
                            { //entidad de data entity


                                switch (result)
                                {

                                    case 0:
                                      
                                        _log.WriteLog("Archivo Recibido con Exito" + "  " + result);

                                        var actual = Regex.Replace(s.UltimoCiclo, "\\d+",
                                       m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));

                                        List<Maestro> RegistroActuales = new List<Maestro>();
                                        Maestro filas = new Maestro
                                        {
                                            Id = s.Id,
                                            Matricula = s.Matricula,
                                            Nombre = s.Nombre,
                                            Version = s.Version,
                                            IP = s.IP,
                                            Seccion = s.Seccion,
                                            Estado = s.Estado,
                                            UltimoCiclo = actual,
                                            RutaSalida = s.RutaSalida
                                        }; RegistroActuales.Add(filas);

                                        Parallel.ForEach(RegistroActuales, i =>
                                        //foreach (var i in RegistroActuales.ToList())
                                        {

                                            _log.WriteLog(i.UltimoCiclo.Trim() + "   " + i.Matricula.Trim());
                                            var resultado = db.MaestroAutoclave.FirstOrDefault(b => b.Id == i.Id);
                                            if (resultado != null)
                                            {
                                                resultado.UltimoCiclo = i.UltimoCiclo;

                                            }
                                            db.SaveChanges();
                                            System.Threading.Thread.Sleep(2000);

                                        });
                                        break;
                                    case 21:
                                        _log.WriteLog("El dispositivo no está listo. No se puede crear el archivo." + " " + result);
                                        break;
                                    case 5:
                                        _log.WriteLog("Acceso denegado al intentar crear el archivo." + " " + result);
                                        break;
                                    case 32899128:
                                        _log.WriteLog("Ya existe una conexion abierta." + " " + result);
                                        break;
                                    case 32899137:
                                        _log.WriteLog("Estado de conexion no permitido." + " " + result);
                                        break;
                                    case 32899138:
                                        _log.WriteLog("Dispositivo remoto desconocido. Revise el ID o IP" + " " + result);
                                        break;
                                    case 32899139:
                                        _log.WriteLog("Handle inválido pasado en la API." + " " + result);
                                        break;
                                    case 3670019:
                                       _log.WriteLog("No ha sido Encontrado el archivo." + "  " + result);
                                        break;

                                }

                            }


                        }

                        //Close Connection
                        var close = con.CloseConnection(ref handle);
                        if (close == 0) { _log.WriteLog("Conexion Cerrada.");  } else { _log.WriteLog("La conexion no pudo ser cerrada"); }
                       
                        _log.WriteLog("\n");
                        System.Threading.Thread.Sleep(1000);

                        var closet = con.CloseConnection(ref handle);



                    }
                    else
                    {
                       
                        _log.WriteLog("Autoclave: " + s.Matricula.Trim() + " Desactivado*"); 
                        _log.WriteLog("\n");
                        
                    }

                }

            }
            }
            catch { _log.WriteLog("Falla de la DLL, verificar el estado de los autoclaves en la tabla maestra"); }
            }

      
    }
}

