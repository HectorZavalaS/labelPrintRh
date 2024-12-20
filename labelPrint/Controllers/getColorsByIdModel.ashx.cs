using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getColorsByIdModel
    /// </summary>
    public class getColorsByIdModel : IHttpHandler
    {

        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            String tbl = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllColors = m_db.getColorsByIdModel(idModel);
                foreach (getColorsByIdModel_Result row in AllColors)
                {
                    html += "<option value='" + row.se_id + "'>" + row.se_description + "</option>";
                    tbl += "<tr>";
                    tbl += "<td>" + row.se_description + "</td>";
                    tbl += "<td>" + row.se_lblTag + "</td>";
                    tbl += "<td>" + "<button class='btn btn-sm btn-info' style='padding: 5px'><span class='glyphicon glyphicon-pencil' style='margin:0;'></span></button>" + "</td>";
                    tbl += "<td>" + "<button class='btn btn-sm btn-danger' style='padding: 5px'><span class='glyphicon glyphicon-trash' style='margin:0;'></span></button>" + "</td>";
                    tbl += "</tr>";
                }
                json += "\"result\":\"true\",";
                json += "\"tbl\":\"" + tbl + "\",";
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