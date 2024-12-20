using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getPreviewLabels
    /// </summary>
    public class getPreviewLabels : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL m_zpl = new CPrintZPL();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                generate_lblCode_template2_Result template_codeL, template_codeR;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSideL"]);
                int id_sideR;
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_rev;//= Convert.ToInt32(context.Request.Form["idRev"]);
                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"])/2;
                //num_lbls = num_lbls + 100;
                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int isRem = Convert.ToInt32(context.Request.Form["isRem"]);

                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);

                String bin = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN

                int nSides = m_dbM.getSidesByIdModel(id_model).Count();
                template_codeL = m_db.generate_lblCode_template2(id_model, id_sideL, id_flx, id_vol, id_rev,id_color, fechaDJ).First();

                if (nSides == 2)
                {
                    id_sideR = Convert.ToInt32(context.Request.Form["idSideR"]);
                    template_codeR = m_db.generate_lblCode_template2(id_model, id_sideR, id_flx, id_vol, id_rev, id_color,fechaDJ).First();
                    json = m_zpl.getLRSidesPreview(template_codeL, template_codeR, num_lbls,bin,c,v,isRem, fechaDJ);
                }
                else
                {
                    json = m_zpl.getUniqueSidePreview(template_codeL, num_lbls*2,bin,c,v,fechaDJ);
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