using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for isPrinted
    /// </summary>
    public class isPrinted : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                int dJ = Convert.ToInt32(context.Request.Form["dj_no"]);
                int id_modelo = Convert.ToInt32(context.Request.Form["idModel"]);

                int result = m_db.isPrintedDJV2(dJ.ToString(),id_modelo).First().COMPLETE;
                if (result == 1)
                {
                    json += "\"result\":\"true\"";
                }
                else
                {
                    json += "\"result\":\"false\"";
                }
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