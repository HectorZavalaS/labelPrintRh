using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for hasLDM
    /// </summary>
    public class hasLDM : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                int id_model_ldm = m_db.hasLDM(id_model).First().id_ldm;
                if(id_model_ldm > 0) {
                    json += "\"result\":\"true\",";
                    json += "\"hasLDM\":\"" + id_model_ldm.ToString() + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"hasLDM\":\"" + id_model_ldm.ToString() + "\"";
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