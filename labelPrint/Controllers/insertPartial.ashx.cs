using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de insertPartial
    /// </summary>
    public class insertPartial : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int pcbQty = Convert.ToInt32(context.Request.Form["pcbQty"]);
                int djNo = Convert.ToInt32(context.Request.Form["djNo"]);
                int id_spec = Convert.ToInt32(context.Request.Form["idSpec"]);
                int result = 0;

                int sides = Convert.ToInt32(m_dbM.getNumSidesByIdModel(id_model).First().numSides);

                result = m_db.insertPCBcount(id_model, djNo.ToString(), id_spec, pcbQty).First().RESULT;

                if (result == 1 )
                    json += "\"result\":\"true\"";
                else
                    json += "\"result\":\"false\"";

                //json += "\"messagge\":\"" + m_Result.TXTRESULT + "\"";

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