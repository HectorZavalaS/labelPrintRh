using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getIdsDJSpecByNoDJ
    /// </summary>
    public class getIdsDJSpecByNoDJ : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                String noDj = context.Request.Form["djNo"];
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                getIdsDJSpecByNoDJ_Result spec = m_db.getIdsDJSpecByNoDJ(noDj,id_model).First();

                json += "\"flux\":\"" + spec.se_id_flx + "\",";
                json += "\"color\":\"" + spec.se_id_color + "\",";
                json += "\"volt\":\"" + spec.se_id_volt + "\",";
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