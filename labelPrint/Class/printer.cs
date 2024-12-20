using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Web;

namespace labelPrint.Class
{
    public class printer
    {
        public bool searchZebraPrinter(ref String namePrinter, ref String PrinterInstalled)
        {
            bool result = false;
            PrinterInstalled = "";
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                //if (printer.Contains("2824"))
                if (printer.Contains("110Xi4"))
                //if (printer.Contains("ZT610"))
                {
                    namePrinter = printer;
                    PrinterInstalled += namePrinter;
                    result = true;
                }
            }
            return result;
        }
        public bool sendToPrinter(String printer, String ZPL, ref String errorMessage)
        {
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printer;
            ps.DefaultPageSettings.PaperSize.Height = 5;
            ps.DefaultPageSettings.PaperSize.Width = 5;
            //ps.PrintRange = PrintRange.;
            //ps.DefaultPageSettings.PrinterSettings.
            return RawPrinterHelper.SendStringToPrinter(printer, ZPL, ref errorMessage,"");
        }

        public bool sendToPrinterIP(String ip, String ZPL, ref String errorMessage)
        {
            // Printer IP Address and communication port
            string ipAddress = ip;
            int port = 9100;
            bool result = false;

            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer = new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPL);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
                result = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                result = false;
            }
            return result;

        }
        public bool GetAllPrinterList(ref String namePrinter, ref String PrintersInstalled)
        {
            PrintersInstalled = "";
            bool result = false;
            System.Management.ManagementScope objMS =
                new System.Management.ManagementScope(ManagementPath.DefaultPath);
            objMS.Connect();

            SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
            System.Management.ManagementObjectCollection objMOC = objMOS.Get();

            foreach (ManagementObject Printers in objMOC)
            {
                if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                {
                    PrintersInstalled += "<br><br>Local: " + Printers["Name"] + "<br>";
                    if (Printers["Name"].ToString().Contains("110Xi4"))
                    //if (Printers["Name"].ToString().Contains("2824"))
                    //if (Printers["Name"].ToString().Contains("ZT610"))
                    {
                        namePrinter = Printers["Name"].ToString();
                        result = true;
                    }
                }
                //if (Convert.ToBoolean(Printers["Network"]))     // ALL NETWORK PRINTERS.
                //{
                //    PrintersInstalled += "Red: " + Printers["Name"] + "<br>";
                //    if (Printers["Name"].ToString().Contains("110Xi4"))
                //    {
                //        namePrinter = Printers["Name"].ToString();
                //        result = true;
                //    }
                //}
                //if (Convert.ToBoolean(Printers["Shared"]))     // ALL NETWORK PRINTERS.
                //{
                //    namePrinter = Printers["Name"].ToString();
                //    result = true;
                //}
            }
            return result;

        }
        public bool GetAllZebraPrinterList(ref List<String> PrintersInstalled)
        {
            PrintersInstalled = new List<String>();
            bool result = false;
            System.Management.ManagementScope objMS =
                new System.Management.ManagementScope(ManagementPath.DefaultPath);
            objMS.Connect();

            SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
            System.Management.ManagementObjectCollection objMOC = objMOS.Get();

            foreach (ManagementObject Printers in objMOC)
            {
                if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                {
                    if (Printers["Name"].ToString().Contains("ZDesigner"))
                    {
                        PrintersInstalled.Add(Printers["Name"].ToString());
                        result = true;
                    }
                }
            }
            return result;

        }
        public bool isOnline(string printerName)
        {
            bool online = false;

            Ping Pings = new Ping();
            int timeout = 10;

            //if (Pings.Send("192.168.6.69", timeout).Status == IPStatus.Success)
            //if (Pings.Send("192.168.6.110", timeout).Status == IPStatus.Success)
            {
                online = true;
            }

            return online;
        }
        public bool ZebraIsOnline(string ip)
        {
            bool online = false;

            Ping Pings = new Ping();
            int timeout = 10;

            //if (Pings.Send("192.168.6.69", timeout).Status == IPStatus.Success)
            if (Pings.Send(ip, timeout).Status == IPStatus.Success)
            {
                online = true;
            }

            return online;
        }
    }
}