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
    /// Summary description for lblWEBService
    /// </summary>
    public class lblWEBService : IHttpHandler
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
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                String strZpl = "";

                String bin = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN

                getLabelByModel_Result label = m_db.getLabelByModel(id_model,bin,c,v).First();

                strZpl = label.zpl_preview.Replace("_XXXXXXXXXXXX_", line1);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);

                if (id_model == 92 || id_model == 216)
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", line1);

                if (id_model == 128 || id_model == 136)
                {
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", line1.Substring(0, 10));  //101598D022, 100598D022
                    strZpl = strZpl.Replace("_BBBBBBBBBBBB_", line1.Substring(10, 6));
                    strZpl = strZpl.Replace("_CCCCCCCCCCCC_", line2);
                }

                byte[] zpl = Encoding.UTF8.GetBytes(strZpl);

                m_webservice = new CLblWEBService(zpl, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        json += "\"result\":\"true\",";
                        json += "\"dirprev\":\"" +  m_webservice.FileName + "\"";
                    }
                    else
                    {
                        json += "\"result\":\"false\",";
                        json += "\"MessageError\":\"Ocurrio un error al recibir preview de etiqueta del Webservice.\"";
                    }
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"MessageError\":\"Ocurrio un error al enviar plantilla de etiqueta a Webservice.\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message + " - " + ex.InnerException);
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + " - " + ex.InnerException + "\"";
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