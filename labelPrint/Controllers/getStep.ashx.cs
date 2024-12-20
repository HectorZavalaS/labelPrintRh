using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getStep
    /// </summary>
    public class getStep : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                int result = Convert.ToInt32(m_db.getStep(id_model).First().RESULT);
                //int nSides = m_db.getSidesByIdModel(id_model).Count();

                if (result > 0)
                {
                    json += "\"result\":\"true\",";
                    json += "\"step\":\"" + result.ToString() + "\",";
                    json += "\"string\":\"" + (result == 1 ? "Uno al paso" : "Dos al paso") + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"step\":\"" + result.ToString() + "\"";
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