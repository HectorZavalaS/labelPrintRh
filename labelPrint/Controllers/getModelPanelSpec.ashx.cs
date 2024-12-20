using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de getModelPanelSpec
    /// </summary>
    public class getModelPanelSpec : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);

                double numPCBS = m_db.getModelPanel(id_model).First().se_num_pcb;

                getModelPanelSpec_Result LDM = m_db.getModelPanelSpec(id_model).First();
                //int nSides = m_db.getSidesByIdModel(id_model).Count();
                var Color = m_db.getModelColorP(id_model, id_color);

                json += "\"result\":\"true\",";
                json += "\"idModel\":\"" + LDM.idModel.ToString() + "\",";
                json += "\"idColor\":\"" + Color.First().se_id.ToString() + "\",";
                json += "\"idSideL\":\"" + "1" + "\",";
                json += "\"idSideR\":\"" + "2" + "\",";
                json += "\"numPCB\":\"" + numPCBS.ToString() + "\",";
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