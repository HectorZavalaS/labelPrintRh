using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getPickedBins
    /// </summary>
    public class getPickedBins : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String tbl = "";
            try
            {
                String dj_no = context.Request.Form["dj_no"];
                var AllBines = m_db.getBinesDJ(dj_no);
                foreach (getBinesDJ_Result row in AllBines)
                {
                    tbl += "<tr>";
                    tbl += "<td>" + row.PART_NUMBER + "</td>";
                    tbl += "<td>" + row.LOT_NUMBER + "</td>";
                    tbl += "<td>" + row.FLUX + "</td>";
                    tbl += "<td>" + row.COLOR + "</td>";
                    tbl += "<td>" + row.VOLTAGE + "</td>";
                    tbl += "</tr>";
                }
                json += "\"result\":\"true\",";
                json += "\"tbl\":\"" + tbl + "\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + " --**-- " + ex.InnerException +  "\"";
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