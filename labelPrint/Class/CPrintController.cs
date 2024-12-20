using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public class CPrintController
    {
        public bool searchPrinter(ref String namePrinter)
        {
            bool result = false;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (printer.Equals(namePrinter))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public bool sendToPrinter(String printer, String ZPL, ref String errorMessage, String serials)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printer;
            //ps.DefaultPageSettings.PaperSize.Height = 5;
            //ps.DefaultPageSettings.PaperSize.Width = 5;
            return RawPrinterHelper.SendStringToPrinter(printer, ZPL, ref errorMessage,serials);
        }
    }
}