using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for hasFourSides
    /// </summary>
    public class hasFourSides : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                int hasFS = m_db.hasFourSides(id_model).First().se_four_sides;
                if (hasFS == 1)
                {
                    json += "\"result\":\"true\",";
                    json += "\"hasLDM\":\"" + hasFS.ToString() + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"hasLDM\":\"" + hasFS.ToString() + "\"";
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