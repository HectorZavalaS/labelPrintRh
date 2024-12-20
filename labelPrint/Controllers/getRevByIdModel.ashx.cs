using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getRevByIdModel
    /// </summary>
    public class getRevByIdModel : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            String id_review = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllRevs = m_db.getReviewByIdModel(idModel);
                foreach (getReviewByIdModel_Result row in AllRevs)
                {
                    html += "<option value='" + row.se_id_review + "'>" + row.se_description + "</option>";
                    id_review = row.se_id_review.ToString();
                }
                json += "\"result\":\"true\",";
                json += "\"idRev\":\"" + id_review + "\",";
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