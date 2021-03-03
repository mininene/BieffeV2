using HojaResumen.Modelo;
using HojaResumen.Modelo.BaseDatosT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.LogRecord
{
    public class LgRecord : ILogRecord
    {
      
       
        public void Writer(string Usuario, DateTime fechaHora, string Evento, string comentario)
        {
            using (var contexto = new CicloAutoclave())

            {
                var listaInicio = new List<Audi>();
                Audi rowe = new Audi
                
               { 
                    Usuario = Usuario,
                    Tabla = "",
                    FechaHora = fechaHora,
                    Evento = Evento,
                    Campo = "", 
                    Valor = "", 
                    ValorActualizado = "",
                    Comentario = comentario,
                    Tiempo = null 
                };listaInicio.Add(rowe);


                var plot = new AudiTrail();
                foreach (var te in listaInicio)
                {
               plot.Usuario = te.Usuario;
               plot.Tabla = "";
               plot.FechaHora = fechaHora;
               plot.Evento = Evento;
               plot.Campo = "";
               plot.Valor = "";
               plot.ValorActualizado = "";
               plot.Comentario = comentario;
               plot.Tiempo = null;
              

                }
               
                contexto.AudiTrail.Add(plot);
                contexto.SaveChanges();
                



            }; 

        }
    }
    
}
