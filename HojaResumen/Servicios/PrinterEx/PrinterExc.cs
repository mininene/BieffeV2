using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HojaResumen.Servicios.PrinterEx
{
    public class PrinterExc : IPrinterExc
    {
        void IPrinterExc.PrinterExc(string archivo, string impresora)
        {
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(archivo, new Font("Times New Roman", 14), new SolidBrush(Color.Black), new RectangleF(20, 20, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

            };
            try
            {
                p.PrinterSettings.PrinterName = impresora;
                p.Print();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }
    }
}
