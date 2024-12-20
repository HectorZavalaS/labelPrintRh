using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for hasFourLbls
    /// </summary>
    public class hasFourLbls : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                int id_model_Flbl = m_db.hasFourLbls(id_model).First().id_mB;
                if (id_model_Flbl > 0)
                {
                    json += "\"result\":\"true\",";
                    json += "\"hasFourLbls\":\"" + id_model_Flbl.ToString() + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"hasFourLbls\":\"" + id_model_Flbl.ToString() + "\"";
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