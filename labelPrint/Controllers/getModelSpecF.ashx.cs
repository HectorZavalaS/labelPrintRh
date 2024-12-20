using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getModelSpecF
    /// </summary>
    public class getModelSpecF : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);

                getModelSpecF_Result LDM = m_db.getModelSpecF(id_model).First();
                //int nSides = m_db.getSidesByIdModel(id_model).Count();
                var Color = m_db.getModelColorB(id_model, id_color);

                json += "\"result\":\"true\",";
                json += "\"idModel\":\"" + LDM.idModel.ToString() + "\",";
                json += "\"idColor\":\"" + Color.First().se_id.ToString() + "\",";
                if(id_model == 92)
                    json += "\"idSideL\":\"" + "3" + "\",";
                else
                    json += "\"idSideL\":\"" + "1" + "\",";
                json += "\"idSideR\":\"" + "2" + "\",";
                json += "\"idRev\":\"" + LDM.idRev.ToString() + "\"";
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