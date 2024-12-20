using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for cmbSpecials
    /// </summary>
    public class cmbSpecials : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            String datIni = "";
            String datFin = "";
            try
            {
                DateTime date = DateTime.Now;
                DateTime finDate = date.AddDays(-60);
                int month;
                int year = date.Year;
                if (date.Month > 1)
                    month = date.Month - 1;
                else
                {
                    month = 12;
                    year = date.Year - 1;
                }
                //datIni = month.ToString() + "/01/" + year.ToString();
                //datFin = date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString();
                datFin = date.Month.ToString("00") + "/" + date.Day.ToString("00") + "/" + date.Year.ToString("00");
                datIni = finDate.Month.ToString("00") + "/" + finDate.Day.ToString("00") + "/" + finDate.Year.ToString("00");
                var AllModels = m_db.getSpecialLabelsToPrint(datIni, datFin);
                foreach (getSpecialLabelsToPrint_Result row in AllModels)
                {
                    html += "<option value='" + row.ID_MODEL + "'>" + row.ASSEMBLY_DESC + ", " + row.CREATED_DT.Value.ToShortDateString() + ", 0" + row.DJ_NO + "," + row.ASSEMBLY_NAME + ", " + row.DJ_GROUP + "</option>";
                }
                json += "\"result\":\"true\",";
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