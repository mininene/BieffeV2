using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.Printer
{
    public class Print : IPrint
    {
        void IPrint.Printer(string archivo, string impresora)
        {
            //foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    var x = printer.ToString() + System.Environment.NewLine;
            //    Console.WriteLine(x);
            //}



           // string path = @"C:\Users\fuenteI3\Desktop\PDFGenerados\AutoclaveA\";
            using (Process printJob = new Process())
            {
                try
                {
                    printJob.StartInfo.FileName = archivo;
                    printJob.StartInfo.UseShellExecute = true;
                    printJob.StartInfo.Verb = "printto";
                    printJob.StartInfo.CreateNoWindow = true;
                   // printJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //printJob.StartInfo.Arguments = "\"" + "RRHH" + "\"";
                    printJob.StartInfo.Arguments = "\"" + impresora + "\"";
                    printJob.StartInfo.WorkingDirectory = Path.GetDirectoryName(archivo);
                    printJob.Start();
                    printJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    System.Threading.Thread.Sleep(200);
                    //printJob.CloseMainWindow();
                 //   if (printJob.HasExited == false) { printJob.Kill(); }
                  // printJob.EnableRaisingEvents = true;

                   printJob.Close();
                  

                    var resultado = from item in System.Diagnostics.Process.GetProcesses()
                                    where item.ProcessName.ToUpper() == "AcroRd32.exe"
                                    select item;

                    foreach (var item in resultado)
                       
                    {
                        item.Kill();
                       

                    }

                    //printJob.Kill();

                    //foreach (Process clsProcess in Process.GetProcesses().Where(
                    //     clsProcess => clsProcess.ProcessName.StartsWith("AcroRd32.exe")))
                    //{
                    //    clsProcess.Kill();

                    //}
                    printJob.CloseMainWindow();
                    printJob.Close();
                    System.Threading.Thread.Sleep(500);

                }
                catch { Console.WriteLine("Algo Paso"); }


            }


        }
    }
}
