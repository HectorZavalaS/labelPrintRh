using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de checkPartSM
    /// </summary>
    public class checkPartSM : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String response = "";
            try
            {
                int djGroup = Convert.ToInt32(context.Request.Form["djGroup"]);
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int id_flx1 = Convert.ToInt32(context.Request.Form["idflx1"]);
                int id_vol1 = Convert.ToInt32(context.Request.Form["idVol1"]);
                int id_color1 = Convert.ToInt32(context.Request.Form["idColor1"]);
                int qty = Convert.ToInt32(context.Request.Form["qty"]);
                int pcbQty = Convert.ToInt32(context.Request.Form["pcbDjQty"]);

                String bin = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN

                var result = m_db.checkPartSM(id_model, djGroup.ToString(), pcbQty, id_flx, id_color, id_vol,id_flx1,id_color1,id_vol1).First();

                switch (result.RESULT)
                {
                    case 1:
                        json += "\"result\":\"true\",";
                        response = "Se pueden imprimir las etiquetas.";
                        break;
                    case -1:
                        json += "\"result\":\"false\",";
                        response = "YA SE IMPRIMIO COMPLETA LA DJ.";
                        break;
                    case -2:
                        json += "\"result\":\"false\",";
                        response = "YA SE IMPRIMIO LA DJ CON ESTOS BINES.";
                        break;
                    case -3:
                        json += "\"result\":\"false\",";
                        response = "<div style='color:red;'>NO HAY SUFICIENTES PCB's DISPONIBLES PARA IMPRIMIR ETIQUETAS. <br><stronge>IMPRESION DSPONIBLE PARA " + result.pcbDisp.ToString() + " PCB's.</stronge>";
                        break;
                }
                json += "\"Message\":\"" + response + "\"";
                json += "}";

            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message + "\"";
                json += "}";
            }

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