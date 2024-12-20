using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for updateZPLOS
    /// </summary>
    public class updateZPLOS : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int idZPL = Convert.ToInt32(context.Request.Form["idZPL"]);
                String ZPL = context.Request.Form["ZPL"];

                m_db.updateZPLOS(idZPL,ZPL);
                json += "\"result\":\"true\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message.Replace("\"","'") + "\"";
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