using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getColorsLed2ByIdModel
    /// </summary>
    public class getColorsLed2ByIdModel : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllColors = m_db.getColorsLed2ByIdModel(idModel);
                foreach (getColorsLed2ByIdModel_Result row in AllColors)
                {
                    html += "<option value='" + row.se_id + "'>" + row.se_description + "</option>";
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