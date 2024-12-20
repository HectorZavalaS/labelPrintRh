using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de getModelDescription
    /// </summary>
    public class getModelDescription : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            int idM = 0;
            try
            {
                int idModel = idM = Convert.ToInt32(context.Request.Form["idModel"]);

                var model = m_db.getModelByID(idModel);
                //foreach (getColorModelB_Result row in AllColors)
                //{
                //    html += "<option value='" + row.se_id + "'>" + row.se_description + "</option>";
                //}
                if (model != null)
                {
                    json += "\"result\":\"true\",";
                    json += "\"html\":\"" + model.First().se_description + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"MessageError\":\"" + "No se encontro el modelo." + idModel.ToString() + "\"";
                }
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + " - " + idM.ToString()  + "\"";
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