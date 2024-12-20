using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de UpdateDjQty
    /// </summary>
    public class UpdateDjQty : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                int djNo = Convert.ToInt32(context.Request.Form["djNo"]);
                int djGrp = Convert.ToInt32(context.Request.Form["djGrp"]);

                var djQty = m_db.getQtyDjGrp(djGrp,djNo,idModel);
                json += "\"result\":\"true\"";
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