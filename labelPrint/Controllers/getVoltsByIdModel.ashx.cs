using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getVoltsByIdModel
    /// </summary>
    public class getVoltsByIdModel : IHttpHandler
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
                var AllVolts = m_db.getVoltsByIdModel(idModel);
                foreach (getVoltsByIdModel_Result row in AllVolts)
                {
                    html += "<option value='" + row.se_id_volt + "'>" + row.se_description + "</option>";
                    tbl += "<tr>";
                    tbl += "<td>" + row.se_description + "</td>";
                    tbl += "<td>" + row.se_volt_lbl + "</td>";
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