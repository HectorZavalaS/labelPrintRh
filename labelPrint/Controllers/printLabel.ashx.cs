using labelPrint.Class;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Management;
using labelPrint.Models;
using System.Threading;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for printLabel
    /// </summary>
    public class printLabel : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL m_zpl = new CPrintZPL();
        CPrintZPL_Led2 m_zpl2 = new CPrintZPL_Led2();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            bool result = false;
            try
            {
                
                generate_lblCode_template2_Result template_codeL, template_codeR;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_sideL = Convert.ToInt32(context.Request.Form["idSideL"]);
                int id_sideR;
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_rev;// = Convert.ToInt32(context.Request.Form["idRev"]);
                int id_user = Convert.ToInt32(context.Request.Form["idUser"]);
                String noDJ = context.Request.Form["noDJ"];
                String djGrp = context.Request.Form["djGrp"];
                string aName = context.Request.Form["aName"];
                int ID_RESULT = 0;
                int modePrint = Convert.ToInt32(context.Request.Form["printMode"]);


                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);

                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"])/2;
                int id_printer = Convert.ToInt32(context.Request.Form["idPrinter"]);//, idPrinter: idPrinter
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                String bin = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN
                int is_rem = Convert.ToInt32(context.Request.Form["isRem"].ToString().Trim());
                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);
                String type_print = context.Request.Form["typePrint"];

                CFolios folios = new CFolios();

                getPrintrById_Result printer = m_dbM.getPrintrById(id_printer).First();


                int nSides = m_dbM.getSidesByIdModel(id_model).Count();
                template_codeL = m_db.generate_lblCode_template2(id_model, id_sideL, id_flx, id_vol, id_rev,id_color, fechaDJ).First();

                if (nSides == 2)
                {
                    id_sideR = Convert.ToInt32(context.Request.Form["idSideR"]);
                    template_codeR = m_db.generate_lblCode_template2(id_model, id_sideR, id_flx, id_vol, id_rev,id_color, fechaDJ).First();
                    json = m_zpl.sendLRSidesToPrinter(template_codeL, template_codeR, num_lbls, ref result, bin, c, v,ref folios,is_rem, fechaDJ,id_printer,djGrp,type_print);
                
                    ID_RESULT = m_db.insertSpec3(id_model, id_flx, id_color, id_vol, id_user, Convert.ToInt32(num_lbls), folios.Init_folio, folios.Last_folio, folios.Curr_folio, noDJ, aName, fechaDJ, is_rem, djGrp).First().RESULT.Value;
                }
                else
                {
                    if (template_codeL.idModel == 314 || template_codeL.idModel == 313 || template_codeL.idModel == 316 || template_codeL.idModel == 317 || template_codeL.idModel == 318)///tres al paso
                    {
                        json = m_zpl.sendUniqueSideToPrinterT(template_codeL, num_lbls * 2, ref result, bin, c, v, ref folios, fechaDJ, id_printer, djGrp, type_print, id_sideL);
                    }
                    else
                        json = m_zpl.sendUniqueSideToPrinter(template_codeL, num_lbls*2,ref result,bin,c,v, ref folios,fechaDJ,id_printer, djGrp, type_print);

                    ID_RESULT = m_db.insertSpec3(id_model, id_flx, id_color, id_vol, id_user, Convert.ToInt32(num_lbls*2), folios.Init_folio, folios.Last_folio, folios.Curr_folio, noDJ, aName,fechaDJ,is_rem,djGrp).First().RESULT.Value;
                }
                json += "\"ID_RESULT\":\"" + ID_RESULT.ToString() + "\"";
                json += "}"; //177480 40 - 9
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