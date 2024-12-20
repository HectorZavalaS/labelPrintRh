using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getZebraPrinters
    /// </summary>
    public class getZebraPrinters : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            //List<String> printers = new List<String>();
            printer m_p = new printer();
            var printers = (ObjectResult)null;

            try
            {
                //m_p.GetAllZebraPrinterList(ref printers);
                printers = m_db.getPrinters();
                foreach(getPrinters_Result p in printers)
                    html += "<option value=" + p.se_id_printer + ">" + p.se_description + "</option>";

                json += "\"html\":\"" + html + "\",";
                json += "\"result\":\"true\"";

            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + "\"";
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