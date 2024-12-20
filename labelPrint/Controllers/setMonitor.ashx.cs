using file_monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de setMonitor
    /// </summary>
    public class setMonitor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            EventWatcherAsync m_monitor = new EventWatcherAsync("192.168.6.51", @"C:", "\\\\SysLblLogs\\\\ToRelease\\\\");

            if (m_monitor.ResultConnect == true)
            {
                m_monitor.startWatch();
                json += "\"result\":\"true\",";
                json += "\"html\":\"Se inicio el proceso de Release exitosamente....\"";
            }
            else
            {
                json += "\"result\":\"false\",";
                json += "\"html\":\"" + m_monitor.ErrorMessage + "\"";
            }

            json += "}";
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}