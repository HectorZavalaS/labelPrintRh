using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for cmbPrintedDJs
    /// </summary>
    public class cmbPrintedDJs : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";

            try
            {

                var AllModels = m_db.getPrintedDJs();
                foreach (getPrintedDJs_Result row in AllModels)
                {
                    html += "<option value='" + row.idModel + "'><b>" + row.DJ + "</b>, " + row.MODELDESCR + ", " + row.FECHA.Value.ToShortDateString() +  "</option>";
                }
                json += "\"result\":\"true\",";
                json += "\"html\":\"" + html + "\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.InnerException.ToString().Replace("\"", "'") + "\"";
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