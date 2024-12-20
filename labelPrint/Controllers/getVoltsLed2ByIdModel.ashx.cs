using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getVoltsLed2ByIdModel
    /// </summary>
    public class getVoltsLed2ByIdModel : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllVolts = m_db.getVoltsLed2ByIdModel(idModel);
                foreach (getVoltsLed2ByIdModel_Result row in AllVolts)
                {
                    html += "<option value='" + row.se_id_volt + "'>" + row.se_description + "</option>";
                }
                json += "\"result\":\"true\",";
                json += "\"html\":\"" + html + "\"";
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