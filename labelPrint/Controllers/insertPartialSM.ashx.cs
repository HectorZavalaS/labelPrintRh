using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de insertPartialSM
    /// </summary>
    public class insertPartialSM : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();

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

                result = m_db.insertPCBcountSM(id_model, djNo.ToString(), id_spec, pcbQty).First().RESULT;

                if (result == 1)
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