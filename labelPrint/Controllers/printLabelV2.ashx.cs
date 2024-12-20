using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de printLabelV2
    /// </summary>
    public class printLabelV2 : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPLV2 m_zpl = new CPrintZPLV2();
        CModelPrint model = new CModelPrint();
        CPrintZPL m_zplOld = new CPrintZPL();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            bool result = false;
            try
            {

                generate_lblCode_template2_Result template_codeL, template_codeR;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSide"]);
                int id_sideR;
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_rev;// = Convert.ToInt32(context.Request.Form["idRev"]);
                int id_user = Convert.ToInt32(context.Request.Form["idUser"]);
                String noDJ = context.Request.Form["noDJ"];
                String djGrp = context.Request.Form["djGrp"];
                string aName = context.Request.Form["aName"];
                CModelPrint model = new CModelPrint();
                int ID_RESULT = 0;

                int num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"]);
                int id_printer = Convert.ToInt32(context.Request.Form["idPrinter"]);//, idPrinter: idPrinter
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                String f = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN
                int is_rem = Convert.ToInt32(context.Request.Form["isRem"].ToString().Trim());
                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);

                List<CFolios> allFolios = new List<CFolios>();
                CFolios foliosBrady = new CFolios();

                getPrintrById_Result printer = m_dbM.getPrintrById(id_printer).First();

                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);

                int nSides = m_dbM.getSidesByIdModel(id_model).Count();

                if (nSides == 2)
                    num_lbls = num_lbls / 2;

                model.Id_flux = id_flx;
                model.Id_color = id_color;
                model.Id_Volt = id_vol;
                model.Dj_group = djGrp;
                model.Date_plan = dateDJ;
                model.Id_model = id_model;
                model.Id_rev = id_rev;
                model.Id_side = id_sideL;
                model.Num_labels = num_lbls;

                if (id_printer == 17 || id_printer == 13)
                {
                    template_codeL = m_db.generate_lblCode_template2(id_model, model.Id_side, id_flx, id_vol, id_rev, id_color, fechaDJ).First();

                    json = m_zplOld.sendLRSidesToPrinterBrady(template_codeL, num_lbls, ref result, f, c, v, ref foliosBrady, is_rem, fechaDJ, id_printer, djGrp, "FOLIO", model.Id_side);
                    ID_RESULT = m_db.insertSpec3V2(id_model, id_flx, id_color, id_vol, id_user, Convert.ToInt32(num_lbls), foliosBrady.Init_folio, foliosBrady.Last_folio, foliosBrady.Curr_folio, noDJ, aName, fechaDJ, is_rem, djGrp, model.Id_side).First().RESULT.Value;

                    json += "\"ID_RESULT\":\"" + ID_RESULT.ToString() + "\",";
                    json += "\"result\":\"true\",";
                    json += "\"MessageSuccess\":\"Se imprimieron las etiquetas.\"";
                }
                else
                    if (m_zpl.generateSerials(model, ref allFolios))
                    {
                        if(m_zpl.printSerials(model, allFolios, f, c, v, id_printer))
                        {
                            foreach(CFolios folios in allFolios)
                                ID_RESULT = m_db.insertSpec3V2(id_model, id_flx, id_color, id_vol, id_user, model.Num_labels, folios.Init_folio, folios.Last_folio, folios.Curr_folio, noDJ, aName, fechaDJ, is_rem, djGrp,folios.Side).First().RESULT.Value;
                            json += "\"ID_RESULT\":\"" + ID_RESULT.ToString() + "\",";
                            json += "\"result\":\"true\",";
                            json += "\"MessageSuccess\":\"Se imprimieron las etiquetas.\"";
                        }
                        else
                        {
                            json += "\"result\":\"false\",";
                            json += "\"MessageError\":\"Ocurrio un error al imprimir los seriales.\"";
                        }
                    }
                    else
                    {
                        json += "\"result\":\"false\",";
                        json += "\"MessageError\":\"Ocurrio un error al generar los seriales. No se imprimiran las etiquetas.\"";
                    }

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