using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de printLabelLed2V2
    /// </summary>
    public class printLabelLed2V2 : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL_Led2V2 m_zpl = new CPrintZPL_Led2V2();
        CModelPrint model = new CModelPrint();
        CPrintZPL_Led2 m_zpl2 = new CPrintZPL_Led2();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            bool result = false;
            try
            {
                generate_lblCode_templateLed2_Result template_codeL;
                int ID_RESULT = 0;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSide"]);
                int id_sideR;

                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);

                int id_flx1 = Convert.ToInt32(context.Request.Form["idflx1"]);
                int id_color1 = Convert.ToInt32(context.Request.Form["idColor1"]);
                int id_vol1 = Convert.ToInt32(context.Request.Form["idVol1"]);

                int id_rev;//= Convert.ToInt32(context.Request.Form["idRev"]);
                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);

                int id_user = Convert.ToInt32(context.Request.Form["idUser"]);
                String noDJ = context.Request.Form["noDJ"];
                String djGrp = context.Request.Form["djGrp"];
                string aName = context.Request.Form["aName"];

                int num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"]);
                int id_printer = Convert.ToInt32(context.Request.Form["idPrinter"]);//, idPrinter: idPrinter
                                                                                    //num_lbls = num_lbls + 100;

                String f = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN
                int is_rem = Convert.ToInt32(context.Request.Form["isRem"].ToString().Trim());
                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);
                String type_print = context.Request.Form["typePrint"];
                int modePrint = Convert.ToInt32(context.Request.Form["printMode"]);
                getPrintrById_Result printer = m_dbM.getPrintrById(id_printer).First();

                List<CFolios> allFolios = new List<CFolios>();
                CFolios folios_660B = new CFolios();

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
                model.Id_flux1 = id_flx1;
                model.Id_color1 = id_color1;
                model.Id_Volt1 = id_vol1;
                model.Dj_group = djGrp;
                model.Date_plan = dateDJ;
                model.Id_model = id_model;
                model.Id_rev = id_rev;
                model.Id_side = id_sideL;
                model.Num_labels = num_lbls;


                if(id_model == 314 || id_model == 428 || id_model == 429 || id_model == 430 || id_model == 431 || id_model == 432 || id_model == 433 || id_printer == 17 || id_printer == 13)
                {
                    template_codeL = m_db.generate_lblCode_templateLed2(id_model, id_sideL, id_flx, id_vol, id_rev, id_color, fechaDJ, id_flx1, id_vol1, id_color1).First();
                    json = m_zpl2.sendLRSidesToPrinterV2(template_codeL, num_lbls, ref result, f, c, v, ref folios_660B, is_rem, fechaDJ, id_printer, djGrp, type_print, id_sideL);
                    if (type_print == "FOLIO")
                        ID_RESULT = m_db.insertSpecLed2_2V2(id_model, id_flx, id_color, id_vol, id_user, noDJ, aName, fechaDJ, is_rem, id_flx1, id_color1, id_vol1, djGrp, folios_660B.PrintFolio).First().RESULT;
                    else ID_RESULT = -1;
                    json += "\"ID_RESULT\":\"" + ID_RESULT.ToString() + "\",";
                    json += "\"result\":\"true\",";
                    json += "\"MessageSuccess\":\"Se imprimieron las etiquetas.\"";
                }
                else
                if (m_zpl.generateSerials(model, ref allFolios))
                {
                    if (m_zpl.printSerials(model, allFolios, f, c, v, id_printer))
                    {
                        foreach (CFolios folios in allFolios)
                            ID_RESULT = m_db.insertSpecLed2_2V2(id_model, id_flx, id_color, id_vol, id_user, noDJ, aName, fechaDJ, is_rem, id_flx1, id_color1, id_vol1, djGrp, folios.PrintFolio).First().RESULT;
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