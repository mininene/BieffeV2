using HojaResumen.Servicios.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace HojaResumen.WindowsService
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            ILog _log = new ProductionLog();
            try {
                HostFactory.Run(hostConfig =>
                {

                    hostConfig.Service<WindowsServiceHR>(serviceConfig =>
                    {
                        serviceConfig.ConstructUsing(() => new WindowsServiceHR());
                        serviceConfig.WhenStarted(s => s.Start());
                        serviceConfig.WhenStopped(s => s.Stop());

                    //hostConfig.StartAutomatically();
                });

                    hostConfig.RunAsLocalSystem();
                    hostConfig.SetServiceName("HojaResumenService");
                    hostConfig.SetDisplayName("HojaResumenService");
                    hostConfig.SetDescription("Servicio de HojaResumen Autoclaves utilizando API TH4LOG ");

                });
            }
            catch { _log.WriteLog("No Iniciar la configuración"); }
        }
    }
  }

