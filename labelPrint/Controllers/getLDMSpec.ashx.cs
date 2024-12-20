using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getLDMSpec
    /// </summary>
    public class getLDMSpec : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                getLDMSpec_Result LDM = m_db.getLDMSpec(id_model).First();
               //int nSides = m_db.getSidesByIdModel(id_model).Count();

                json += "\"result\":\"true\",";
                json += "\"idModel\":\"" + LDM.idModel.ToString() + "\",";
                json += "\"idSideL\":\"" + "1" + "\",";
                json += "\"idSideR\":\"" + "2" + "\",";
                json += "\"idFlx\":\"" + LDM.idFlx.ToString() + "\",";
                json += "\"idColor\":\"" + LDM.idCol.ToString() + "\",";
                json += "\"idVol\":\"" + LDM.idVol.ToString() + "\",";
                json += "\"idRev\":\"" + LDM.idRev.ToString() + "\",";
                json += "\"descr\":\"" + LDM.descr + "\"";
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