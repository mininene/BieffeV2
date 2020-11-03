using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
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
        public void ConnectTHLog()
        {
           

            using (var context = new CicloAutoclave())
            { //entidad de data entity

                Wrapper.ConnectionWrapper con = new Wrapper.ConnectionWrapper();

                foreach (var s in context.MaestroAutoclave) // tabla MaestroAutoclave genero otro contexto
                {
                  

                    string ciclo = s.Matricula.Trim() + s.UltimoCiclo.Trim() + ".LOG";
                    string rutaSalida = s.RutaSalida.Trim() + ciclo;


                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine(ciclo);
                    System.Threading.Thread.Sleep(1000);

                    //get Version
                    float version = con.get_version();
                    Console.WriteLine(version);


                    //session
                    uint handle = con.ConnectSession(s.Version, s.IP);
                    if (handle != 0) { Console.WriteLine("Sesion Iniciada Correctamente"); } else { Console.WriteLine("IP o version Incorrecta" + handle); }
                    //Console.WriteLine("Valor de Sesion: " + handle);


                    //Connection
                    uint lhandle = con.ConnectApi(handle);
                    if (lhandle == 0) { Console.WriteLine("Conexión Establecida"); } else { Console.WriteLine("No se pudo establecer la Conexión - IP o version Incorrecta" + lhandle); }
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
                                    Console.WriteLine("Archivo Recibido con Exito" + "  " + result);

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


                                    foreach (var i in RegistroActuales.ToList())
                                    {
                                        Console.WriteLine(i.UltimoCiclo.Trim() + "   " + i.Matricula.Trim());
                                        var resultado = db.MaestroAutoclave.FirstOrDefault(b => b.Id == i.Id);
                                        if (resultado != null)
                                        {
                                            resultado.UltimoCiclo = i.UltimoCiclo;

                                        }
                                        db.SaveChanges();
                                        System.Threading.Thread.Sleep(2000);

                                    }
                                    break;
                                case 21:
                                    Console.WriteLine("El dispositivo no está listo. No se puede crear el archivo." + " " + result);
                                    break;
                                case 5:
                                    Console.WriteLine("Acceso denegado al intentar crear el archivo." + " " + result);
                                    break;
                                case 32899128:
                                    Console.WriteLine("Ya existe una conexion abierta." + " " + result);
                                    break;
                                case 32899137:
                                    Console.WriteLine("Estado de conexion no permitido." + " " + result);
                                    break;
                                case 32899138:
                                    Console.WriteLine("Dispositivo remoto desconocido. Revise el ID o IP" + " " + result);
                                    break;
                                case 32899139:
                                    Console.WriteLine("Handle inválido pasado en la API." + " " + result);
                                    break;
                                case 3670019:
                                    Console.WriteLine("No ha sido Encontrado el archivo." + "  " + result);
                                    break;

                            }

                        }


                    }

                    //Close Connection
                    var close = con.CloseConnection(ref handle);
                    if (close == 0) { Console.WriteLine("Conexion Cerrada"); } else { Console.WriteLine("La conexion no pudo ser cerrada"); }
                    //Console.WriteLine("Cerrar Conexion valor: " + con.CloseConnection(ref handle));
                    Console.WriteLine("\n\n");
                    System.Threading.Thread.Sleep(1000);

                    var closet = con.CloseConnection(ref handle);



                }

            }

        }
    }
}
