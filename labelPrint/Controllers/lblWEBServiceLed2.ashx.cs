using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for lblWEBServiceLed2
    /// </summary>
    public class lblWEBServiceLed2 : IHttpHandler
    {

        CLblWEBService m_webservice;
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";

            try
            {
                String code = context.Request.Form["code"];
                String line1 = context.Request.Form["line1"];
                String line2 = context.Request.Form["line2"];
                String line1U611 = context.Request.Form["line1U611"];
                String line2U611 = context.Request.Form["line2U611"];
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int id_flx1 = Convert.ToInt32(context.Request.Form["idflx1"]);
                int id_vol1 = Convert.ToInt32(context.Request.Form["idVol1"]);
                int id_color1 = Convert.ToInt32(context.Request.Form["idColor1"]);
                String strZpl = "";

                String bin = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN

                getLabelByModel_Result label = m_db.getLabelByModel(id_model, bin, c, v).First();
                strZpl = label.zpl_preview.Replace("_XXXXXXXXXXXX_", line1);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);
                if (id_model == 117)
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                    strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
                }
                if (id_model == 116)
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                    strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
                }
                if (id_model == 114)
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                    strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
                }
                if (id_model == 46 || id_model == 167 || id_model == 168 || id_model == 169 || id_model == 170 || id_model == 171 || id_model == 172 || id_model == 296) 
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", line1U611);
                }
                if (id_model == 428 || id_model == 429 || id_model == 430 || id_model == 431 || id_model == 432 || id_model == 433)
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAA_", line2.Substring(0, 5));
                }

                byte[] zpl = Encoding.UTF8.GetBytes(strZpl);

                m_webservice = new CLblWEBService(zpl, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        json += "\"result\":\"true\",";
                        json += "\"dirprev\":\"" + m_webservice.FileName + "\"";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message + " - " + ex.InnerException);
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + " - " + ex.Message + "\"";
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