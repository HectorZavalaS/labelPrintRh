using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace labelPrint.Class
{
    public class CLblWEBService
    {
        private byte[] m_strZPLLbl;
        private double m_heightLbl;
        private double m_WidthLbl;
        private HttpWebRequest m_request;
        private string m_path;
        private string m_fileName;
        public byte[] StrZPLLbl { get => m_strZPLLbl; set => m_strZPLLbl = value; }
        public double HeightLbl { get => m_heightLbl; set => m_heightLbl = value; }
        public double WidthLbl { get => m_WidthLbl; set => m_WidthLbl = value; }
        public string Path { get => m_path; set => m_path = value; }
        public string FileName { get => m_fileName; set => m_fileName = value; }

        public CLblWEBService(byte[] strZPL, double height, double width)
        {
            StrZPLLbl = strZPL;
            HeightLbl = height;
            WidthLbl = width;
        }

        public bool send()
        {
            bool result = false;
            try
            {
                //if (checkOnline()) { 
                    m_request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/24dpmm/labels/" + WidthLbl.ToString() + "x" + HeightLbl.ToString() + "/0/");
                    m_request.Method = "POST";
                    //request.Accept = "application/pdf"; // omit this line to get PNG images back
                    m_request.ContentType = "application/x-www-form-urlencoded";
                    m_request.ContentLength = StrZPLLbl.Length;

                    var requestStream = m_request.GetRequestStream();
                    requestStream.Write(StrZPLLbl, 0, StrZPLLbl.Length);
                    requestStream.Close();
                    result = true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool receive()
        {
            bool result = false;
            try
            {
                //if (checkOnline())
                //{
                    var response = (HttpWebResponse)m_request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    FileName = "temp_" + Guid.NewGuid().ToString() + ".png";
                    //if (File.Exists(context.Server.MapPath("/Labels/label.png"))) { File.Delete(context.Server.MapPath("/Labels/label.png")); }
                    Path = HttpRuntime.AppDomainAppPath + "/Labels/" + FileName;
                    var fileStream = File.Create(Path); // change file name for PNG images
                    responseStream.CopyTo(fileStream);
                    responseStream.Close();
                    fileStream.Close();
                    response.Close();
                    result = true;
                //}
                //else return false;
            }
            catch (WebException e)
            {
                Console.WriteLine("Error:" + e.Message + " - " + e.InnerException );
                result = false;
            }
            return result;
        }
        public bool checkOnline()
        {
            bool result = false;
            Ping Pings = new Ping();
            int timeout = 10;

            if (Pings.Send("labelary.com", timeout).Status == IPStatus.Success)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}