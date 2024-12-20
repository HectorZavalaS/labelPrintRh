using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/***************************************************************************************/
/***************************************************************************************/
/***************************************************************************************/
//                                                                                     //
//               Clase con los metodos para imprimir etiquetas de un bin               //
//               uno al paso obteniendo los seriales desde la BD                       //
//                                                                                     //
/***************************************************************************************/
/***************************************************************************************/
/***************************************************************************************/
namespace labelPrint.Class
{
    public class CPrintZPLV2
    {

        #region VARIABLES

            private CLblWEBService m_webservice;
            private siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
            private siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
            private CPrintController m_printer = new CPrintController();
            private Utils m_utils = new Utils();

            public CLblWEBService Webservice { get => m_webservice; set => m_webservice = value; }
            public siixsem_sys_lblPrint_dbEntities Db { get => m_db; set => m_db = value; }
            public siixsem_main_dbEntities DbM { get => m_dbM; set => m_dbM = value; }
            public CPrintController Printer { get => m_printer; set => m_printer = value; }
            public Utils Utils { get => m_utils; set => m_utils = value; }

        #endregion
        public bool generateSerials(CModelPrint model, ref List<CFolios> allFolios)
        {
            bool result = false;
            int valIni = 0;
            int MAXSERIAL = 0;
            DateTime temp;
            List<generateSerialsByDJV3_Result> regFolios = new List<generateSerialsByDJV3_Result>(); ; 

            try
            {
                if (DateTime.TryParse(model.Date_plan, out temp))
                {
                    if (m_dbM.isSerialContinuos(model.Id_model).First().isContinuos == 1)
                        valIni = Convert.ToInt32(m_db.getLastSerial(model.Id_model).First().lastSerial);
                    else
                        //if (m_db.checkDateDJV2(model.Id_model, temp.Day.ToString() + "/" + temp.Month.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                        if (m_db.checkDateDJV2(model.Id_model, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                            valIni = Convert.ToInt32(m_db.getLastSerialBDJ(model.Dj_group).First().lastSerial);
                        else
                            valIni = 1;

                    MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialByDjV3(model.Dj_group, model.Id_model, model.Id_side).First().MAXSERIAL);

                    if (MAXSERIAL > 0)
                        if (MAXSERIAL > valIni)
                            valIni = MAXSERIAL;

                    //generateSerialsByDJV2_Result resultSerials = m_db.generateSerialsByDJV2(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color, 
                    //                                                                        model.Id_Volt, model.Id_rev, model.Date_plan,valIni, model.Id_side).First();

                    regFolios = m_db.generateSerialsByDJV3(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color,
                                                                                            model.Id_Volt, model.Id_rev, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(), valIni, model.Id_side,model.Id_model).ToList();
                    if (regFolios.First().printFolio == -1)
                        result = false;
                    else
                    {
                        foreach (generateSerialsByDJV3_Result resFolios in regFolios)
                        {
                            CFolios folios = new CFolios();
                            folios.Init_folio = Convert.ToInt32(resFolios.initFolio);
                            folios.Last_folio = Convert.ToInt32(resFolios.lastFolio);
                            folios.Curr_folio = Convert.ToInt32(resFolios.lastFolio);
                            folios.PrintFolio = Convert.ToInt32(resFolios.printFolio);
                            folios.Full_template = resFolios.FULLTEMPLATE;
                            folios.Line_bot_template = resFolios.LINEBTTEMPLATE;
                            folios.Side = Convert.ToInt32(resFolios.ID_SIDE);
                            allFolios.Add(folios);
                        }
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool printSerials(CModelPrint modelToPrint,List<CFolios> allFolios, string f, string c, string v, int idPrinter)
        {
            bool result = false;
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            {
                return false;
            }
            getModelByID_Result model = m_dbM.getModelByID(modelToPrint.Id_model).First();
            getGeneratedSerialsByDJV2_Result serial;

            foreach (CFolios folios in allFolios)
            {

                serial = m_db.getGeneratedSerialsByDJV2(modelToPrint.Dj_group, folios.PrintFolio, modelToPrint.Id_side, modelToPrint.Id_model).First();

                if (serial == null)
                    result = false;
                else
                {
                    getLabelByModel_Result label = m_db.getLabelByModel(modelToPrint.Id_model, f, c, v).First();
                    if (m_printer.searchPrinter(ref namePrinter))
                    {
                        getTemplateLblPrint_Result template = m_db.getTemplateLblPrint(folios.Full_template, folios.Line_bot_template).First();
                        //if (idPrinter == 17 || idPrinter == 13)
                        //    strZPL += Utils.replaceLabelV2(serial.se_serTop, serial.se_serBot, label.zpl_two_cab, modelToPrint, template);
                        //else
                        strZPL += Utils.replaceLabelV2(serial.se_serTop, serial.se_serBot, label.zpl_two_zt, modelToPrint, template);

                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, model.se_description + " DJ group: " + modelToPrint.Dj_group))
                            result = false;
                        else result = true;
                    }

                }
            }

            return result;
        }
    }
}