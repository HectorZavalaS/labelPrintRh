using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getLabelSize
    /// </summary>
    public class getLabelSize : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);

                getLabelByModel_Result label = m_db.getLabelByModel(id_model,"NA","NA","NA").First();

                Double width = Math.Round(label.width * 25.4,1);
                Double height = Math.Round(label.height * 25.4,1);

                json += "\"result\":\"true\",";
                json += "\"width\":\"" + width.ToString() + " mm\",";
                json += "\"height\":\"" + height.ToString() + " mm\"";
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