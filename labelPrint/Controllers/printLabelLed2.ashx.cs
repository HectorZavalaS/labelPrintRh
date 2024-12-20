using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for printLabelLed2
    /// </summary>
    public class printLabelLed2 : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL_Led2 m_zpl = new CPrintZPL_Led2();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            bool result = false;
            try
            {

                generate_lblCode_templateLed2_Result template_codeL, template_codeR;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSideL"]);
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

                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"])/2;
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

                CFolios folios = new CFolios();
                int ID_RESULT = 0;

                getPrintrById_Result printer = m_dbM.getPrintrById(id_printer).First();


                int nSides = m_dbM.getSidesByIdModel(id_model).Count();
                template_codeL = m_db.generate_lblCode_templateLed2(id_model, id_sideL, id_flx, id_vol, id_rev, id_color, fechaDJ,id_flx1,id_vol1,id_color1).First();

                if (nSides == 2)
                {
                    id_sideR = Convert.ToInt32(context.Request.Form["idSideR"]);
                    template_codeR = m_db.generate_lblCode_templateLed2(id_model, id_sideR, id_flx, id_vol, id_rev, id_color, fechaDJ,id_flx1,id_vol1,id_color1).First();
                    json = m_zpl.sendLRSidesLed2ToPrinter(template_codeL, template_codeR, num_lbls, ref result, f, c, v, ref folios, is_rem, fechaDJ,id_printer,djGrp,type_print,id_sideL);
                    //json = m_zpl.sendLRSidesToPrinterV2(template_codeL, num_lbls, ref result, f, c, v, ref folios, is_rem, fechaDJ, id_printer, djGrp, type_print, 1);
                    //Thread.Sleep(10000);
                    //json = m_zpl.sendLRSidesToPrinterV2(template_codeR, num_lbls, ref result, f, c, v, ref folios, is_rem, fechaDJ, id_printer, djGrp, type_print, 2);

                    ID_RESULT = m_db.insertSpecLed2_2V2(id_model, id_flx, id_color, id_vol, id_user, noDJ, aName, fechaDJ, is_rem,id_flx1,id_color1,id_vol1,djGrp,folios.PrintFolio).First().RESULT;

                }
                else
                {
                    if (template_codeL.idModel == 314)///tres al paso
                    {
                        json = m_zpl.sendUniqueSideToPrinterLed2T(template_codeL, num_lbls * 2, ref result, f, c, v, ref folios, fechaDJ, id_printer, djGrp, type_print,id_sideL);
                    }
                    else
                        if(template_codeL.idModel != 167 && template_codeL.idModel != 168 && template_codeL.idModel != 169 && template_codeL.idModel != 170 && template_codeL.idModel != 171 && template_codeL.idModel != 172)
                            json = m_zpl.sendUniqueSideToPrinterLed2(template_codeL, num_lbls*2, ref result, f, c, v, ref folios, fechaDJ,id_printer,djGrp,type_print,id_sideL);
                        else
                            json = m_zpl.sendUniqueSideToPrinterLed2_2(template_codeL, num_lbls * 2, ref result, f, c, v, ref folios, fechaDJ, id_printer,djGrp,id_sideL);

                    ID_RESULT = m_db.insertSpecLed2_2(id_model, id_flx, id_color, id_vol, id_user, Convert.ToInt32(num_lbls*2), folios.Init_folio, folios.Last_folio, folios.Curr_folio, noDJ, aName, fechaDJ, is_rem,id_flx1,id_color1,id_vol1,djGrp).First().RESULT.Value;
                    //ID_RESULT = m_db.insertSpecLed2_2V2(id_model, id_flx, id_color, id_vol, id_user, noDJ, aName, fechaDJ, is_rem, id_flx1, id_color1, id_vol1, djGrp, folios.PrintFolio).First().RESULT;
                }
                json += "\"ID_RESULT\":\"" + ID_RESULT.ToString() + "\"";
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