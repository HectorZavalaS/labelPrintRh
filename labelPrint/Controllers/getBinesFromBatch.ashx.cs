using labelPrint.Models;
using System;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de getBinesFromBatch
    /// </summary>
    public class getBinesFromBatch : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {

            String json = "{";
            try
            {
                String batch = context.Request.Form["batch"];
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);

            //    getBinesDJFromOracleV2_Result bines = m_db.getBinesDJFromOracleV2(batch,idModel).First();
                var bines = m_db.getBinesDJFromOracleV2(batch, idModel);
                string html = "";
                string html2 = "";
                bool hasTwoLeds = false;
                int i = 1;
                foreach(getBinesDJFromOracleV2_Result bin in bines)
                {
                    if (bin.DESCR.Contains("LED1"))
                    {
                        html += "<label id='" + bin.IDFLUX.ToString() + "'>" + bin.FLUX + "</label>";
                        html += "<label id='" + bin.IDCOLOR.ToString() + "'>" + bin.COLOR + "</label>";
                        html += "<label id='" + bin.IDVOLTAGE.ToString() + "'>" + bin.VOLTAGE + "</label>";
                    }
                    else
                    {
                        html2 += "<label id='" + bin.IDFLUX.ToString() + "'>" + bin.FLUX + "</label>";
                        html2 += "<label id='" + bin.IDCOLOR.ToString() + "'>" + bin.COLOR + "</label>";
                        html2 += "<label id='" + bin.IDVOLTAGE.ToString() + "'>" + bin.VOLTAGE + "</label>";
                        hasTwoLeds = true;
                    }
                }

                    json += "\"result\":\"true\",";
                    json += "\"hasTwoLeds\":\"" + hasTwoLeds.ToString() + "\",";
                //    json += "\"idcolor\":\"" + bines.IDCOLOR + "\",";
                //    json += "\"idvoltage\":\"" + bines.IDVOLTAGE + "\",";
                //    json += "\"flux\":\"" + bines.FLUX + "\",";
                    if (hasTwoLeds)
                        json += "\"html2\":\"" + html2 + "\",";
                    json += "\"html1\":\"" + html + "\"";
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
            //    json += "\"idflux\":\"" + "NA" + "\",";
            //    json += "\"idcolor\":\"" + "NA" + "\",";
            //    json += "\"idvoltage\":\"" + "NA" + "\",";
            //    json += "\"flux\":\"" + "NOT PICKED" + "\",";
            //    json += "\"color\":\"" + "NOT PICKED" + "\",";
            //    json += "\"voltage\":\"" + "NOT PICKED" + "\",";
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