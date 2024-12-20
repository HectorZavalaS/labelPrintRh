using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for printLabelSide
    /// </summary>
    public class printLabelSide : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        CPrintZPL m_zpl = new CPrintZPL();
        //CPrintZPL_Led2 m_zpl2 = new CPrintZPL_Led2();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            bool result = false;
            try
            {

                generate_lblCode_template2_Result template_codeL;
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_side = Convert.ToInt32(context.Request.Form["idSide"]);
                int id_flx = Convert.ToInt32(context.Request.Form["idflx"]);
                int id_vol = Convert.ToInt32(context.Request.Form["idVol"]);
                int id_rev;// = Convert.ToInt32(context.Request.Form["idRev"]);
                int id_user = Convert.ToInt32(context.Request.Form["idUser"]);
                String noDJ = context.Request.Form["noDJ"];
                String djGrp = context.Request.Form["djGrp"];
                string aName = context.Request.Form["aName"];
                int ID_RESULT = 0;


                if (m_dbM.isDFCIFC(id_model).First().se_dfc_ifc == 1)
                    id_rev = 0;
                else
                    id_rev = Convert.ToInt32(context.Request.Form["idRev"]);

                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"]) / 2;
                int id_printer = Convert.ToInt32(context.Request.Form["idPrinter"]);//, idPrinter: idPrinter
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                String f = m_dbM.getFluxDescByID(id_flx).First().se_description;  // FLUX BIN
                String c = m_dbM.getColorDescById(id_color).First().se_description;  // COLOR BIN
                String v = m_dbM.getVoltDescById(id_vol).First().se_description;   // VOLTAGE BIN
                int is_rem = Convert.ToInt32(context.Request.Form["isRem"].ToString().Trim());
                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                DateTime fechaDJ = DateTime.Parse(dateDJ);
                String type_print = context.Request.Form["typePrint"];

                CFolios folios = new CFolios();

                getPrintrById_Result printer = m_dbM.getPrintrById(id_printer).First();

                if (id_side == 3) num_lbls = num_lbls * 2;

                int nSides = m_dbM.getSidesByIdModel(id_model).Count();
                template_codeL = m_db.generate_lblCode_template2(id_model, id_side, id_flx, id_vol, id_rev, id_color, fechaDJ).First();

                json = m_zpl.sendLRSidesToPrinterV2(template_codeL, num_lbls, ref result, f, c, v, ref folios, is_rem, fechaDJ, id_printer, djGrp, type_print, id_side);
                if (type_print == "FOLIO")
                    ID_RESULT = m_db.insertSpec3V2(id_model, id_flx, id_color, id_vol, id_user, Convert.ToInt32(num_lbls), folios.Init_folio, folios.Last_folio, folios.Curr_folio, noDJ, aName, fechaDJ, is_rem, djGrp, id_side).First().RESULT.Value;
                else ID_RESULT = -1;

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