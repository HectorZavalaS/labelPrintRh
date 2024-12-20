using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getPreviewLabelsLed2
    /// </summary>
    public class getPreviewLabelsLed2 : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL_Led2V2 m_zpl = new CPrintZPL_Led2V2();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                generate_lblCode_templateLed2_Result template_codeL, template_codeR;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSideL"]);
                int id_sideR;
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int id_flx1 = Convert.ToInt32(context.Request.Form["idflx1"]);
                int id_vol1 = Convert.ToInt32(context.Request.Form["idVol1"]);
                int id_color1 = Convert.ToInt32(context.Request.Form["idColor1"]);
                int id_rev;//= Convert.ToInt32(context.Request.Form["idRev"]);


                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);
                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"])/2;

                int isRem = Convert.ToInt32(context.Request.Form["isRem"]);

                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);

                String f = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN

                int nSides = m_dbM.getSidesByIdModel(id_model).Count();
                template_codeL = m_db.generate_lblCode_templateLed2(id_model, id_sideL, id_flx, id_vol, id_rev, id_color, fechaDJ,id_flx1,id_vol1,id_color1).First();

                if (nSides == 2)
                {
                    id_sideR = Convert.ToInt32(context.Request.Form["idSideR"]);
                    template_codeR = m_db.generate_lblCode_templateLed2(id_model, id_sideR, id_flx, id_vol, id_rev, id_color, fechaDJ,id_flx1,id_vol1,id_color1).First();
                    json = m_zpl.getLRSidesPreviewLed2(template_codeL, template_codeR, num_lbls, f, c, v, isRem, fechaDJ,id_sideL);
                }
                else
                {
                    json = m_zpl.getUniqueSidePreviewLed2(template_codeL, num_lbls*2, f, c, v, fechaDJ, id_sideL);
                }
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