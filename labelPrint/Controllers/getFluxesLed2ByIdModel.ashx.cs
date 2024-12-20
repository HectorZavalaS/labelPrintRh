using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getFluxesLed2ByIdModel
    /// </summary>
    public class getFluxesLed2ByIdModel : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllFluxes = m_db.getFluxesLed2ByIdModel(idModel);
                foreach (getFluxesLed2ByIdModel_Result row in AllFluxes)
                {
                    html += "<option value='" + row.se_id_flx + "'>" + row.se_description + "</option>";
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