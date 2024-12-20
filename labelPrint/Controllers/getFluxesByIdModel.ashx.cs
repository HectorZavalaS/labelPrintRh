using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getFluxesByIdModel
    /// </summary>
    public class getFluxesByIdModel : IHttpHandler
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
                var AllFluxes = m_db.getFluxesByIdModel(idModel);
                foreach (getFluxesByIdModel_Result row in AllFluxes)
                {
                    html += "<option value='" + row.se_id_flx + "'>" + row.se_description + "</option>";
                    tbl += "<tr>";
                    tbl += "<td>" + row.se_description + "</td>";
                    tbl += "<td>" + row.se_flx_lbl + "</td>";
                    tbl += "<td>" + "<button class='btn btn-sm btn-info' onclick='getDlgEditFlux("+row.se_id_detail.ToString()+")' style='padding: 5px' data-toggle='modal' data-target='#dlgGeneral'><span class='glyphicon glyphicon-pencil' style='margin:0;'></span></button>" + "</td>";
                    tbl += "<td>" + "<button class='btn btn-sm btn-danger' style='padding: 5px' data-toggle='modal' data-target='#dlgGeneral'><span class='glyphicon glyphicon-trash' style='margin:0;'></span></button>" + "</td>";
                    tbl += "</tr>";
                }
                json += "\"result\":\"true\",";
                json += "\"tbl\":\"" + tbl + "\",";
                json += "\"html\":\"" + html + "\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + "Error al obtener el flux: " + ex.Message + "\n" + ex.InnerException.ToString().Replace("\"","-")  + "\"";
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