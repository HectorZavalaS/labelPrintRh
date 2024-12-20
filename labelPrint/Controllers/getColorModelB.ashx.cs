using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getColorModelB
    /// </summary>
    public class getColorModelB : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                int idColor = Convert.ToInt32(context.Request.Form["idColor"]);

                var AllColors = m_db.getModelColorB(idModel,idColor);
                //foreach (getColorModelB_Result row in AllColors)
                //{
                //    html += "<option value='" + row.se_id + "'>" + row.se_description + "</option>";
                //}
                json += "\"result\":\"true\",";
                json += "\"html\":\"" + AllColors.First().se_id + "\"";
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