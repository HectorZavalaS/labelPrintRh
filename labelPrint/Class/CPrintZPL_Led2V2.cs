using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace labelPrint.Class
{
    public class CPrintZPL_Led2V2
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

        public String getUniqueSidePreviewLed2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, string f, string c, string v, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();

            double valIni;

            if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                valIni = 1;

            double valFin = valIni + 6;

            for (double i = valIni; i < valFin; i += 2)
            {
                if (template_codeL.idModel == 167 || template_codeL.idModel == 168 || template_codeL.idModel == 169 || template_codeL.idModel == 170 || template_codeL.idModel == 182
                    || template_codeL.idModel == 120 || template_codeL.idModel == 118 || template_codeL.idModel == 119 || template_codeL.idModel == 196 || template_codeL.idModel == 200
                    || template_codeL.idModel == 201 || template_codeL.idModel == 202 || template_codeL.idModel == 305)
                {
                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i + 1));
                }
                else
                {
                    if (template_codeL.idModel == 314)
                    {
                        tmpL = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    }
                }
                if (template_codeL.idModel == 171 || template_codeL.idModel == 172)
                {
                    String hour = String.Format("{0:00}", DateTime.Now.Hour);
                    String minu = String.Format("{0:00}", DateTime.Now.Minute);
                    String sec = String.Format("{0:00}", DateTime.Now.Second);

                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                    sec = String.Format("{0:00}", DateTime.Now.Second);
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                }
                byte[] zplL = Utils.getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = Utils.getBytesString(template_codeL, tmpR, label.zpl_preview);

                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += Utils.getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                            if (m_webservice.receive())
                            {
                                rowsTop += Utils.getHtmlLbl(m_webservice.FileName);
                                if (template_codeL.idModel == 314)
                                    tblRows += Utils.getRowTbl(tmpL + template_codeL.line2, tmpR + template_codeL.line2);
                                else
                                    tblRows += Utils.getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += Utils.getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += Utils.getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();

            double tmp = valIni + num_lbls - 5;
            valFin = valIni + num_lbls;

            for (double j = tmp; j <= valFin; j += 2)
            {
                if (template_codeL.idModel == 167 || template_codeL.idModel == 168 || template_codeL.idModel == 169 || template_codeL.idModel == 170 || template_codeL.idModel == 182
                    || template_codeL.idModel == 120 || template_codeL.idModel == 118 || template_codeL.idModel == 119 || template_codeL.idModel == 196 || template_codeL.idModel == 200
                    || template_codeL.idModel == 201 || template_codeL.idModel == 202 || template_codeL.idModel == 305)
                {
                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", j));
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", j + 1));
                }
                else
                {
                    if (template_codeL.idModel == 314)
                    {
                        tmpL = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", j));
                        tmpR = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", j + 1));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                        tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j + 1));
                    }
                }
                if (template_codeL.idModel == 171 || template_codeL.idModel == 172)
                {
                    String hour = String.Format("{0:00}", DateTime.Now.Hour);
                    String minu = String.Format("{0:00}", DateTime.Now.Minute);
                    String sec = String.Format("{0:00}", DateTime.Now.Second);

                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                    sec = String.Format("{0:00}", DateTime.Now.Second);
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                }
                byte[] zplL = Utils.getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = Utils.getBytesString(template_codeL, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += Utils.getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                            if (m_webservice.receive())
                            {
                                rowsBot += Utils.getHtmlLbl(m_webservice.FileName);
                                if (template_codeL.idModel == 314)
                                    tblRows += Utils.getRowTbl(tmpL + template_codeL.line2, tmpR + template_codeL.line2);
                                else
                                    tblRows += Utils.getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            json += "\"result\":\"true\",";
            json += "\"tblTop\":\"" + tblRows + "\",";
            json += "\"htmlTop\":\"" + rowsTop + "\",";
            json += "\"htmlBot\":\"" + rowsBot + "\"";

            json += "}";
            return json;
        }
        public String getLRSidesPreviewLed2(generate_lblCode_templateLed2_Result template_codeL, generate_lblCode_templateLed2_Result template_codeR, Double num_lbls, string f, String c, string v, int isRem, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";
            int valIni = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            //m_db.checkDateDJLed2(model.Id_model, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y"
            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                if (isRem == 1)
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else valIni = 1;

            for (int i = valIni; i < valIni + 3; i++)
            {
                if (template_codeL.idModel == 69 || template_codeL.idModel == 46)
                {
                    tmpL = template_codeL.line2.Replace("XXXX", String.Format("{0:0000}", i));
                    tmpR = template_codeR.line2.Replace("XXXX", String.Format("{0:0000}", i));
                }
                else
                {
                    if (template_codeL.idModel == 167 || template_codeL.idModel == 168)
                    {
                        tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                        tmpR = template_codeR.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    }
                }
                if (template_codeL.idModel == 171 || template_codeL.idModel == 172)
                {
                    String hour = String.Format("{0:00}", DateTime.Now.Hour);
                    String minu = String.Format("{0:00}", DateTime.Now.Minute);
                    String sec = String.Format("{0:00}", DateTime.Now.Second);

                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                    sec = String.Format("{0:00}", DateTime.Now.Second);
                    tmpR = template_codeR.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                }
                byte[] zplL = Utils.getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = Utils.getBytesString(template_codeR, tmpR, label.zpl_preview);


                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += Utils.getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                            if (m_webservice.receive())
                            {
                                rowsTop += Utils.getHtmlLbl(m_webservice.FileName);
                                tblRows += Utils.getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += Utils.getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += Utils.getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();

            double tmp = num_lbls + valIni - 2;
            for (double j = tmp; j <= valIni + num_lbls; j++)
            {
                if (template_codeL.idModel == 69 || template_codeL.idModel == 46)
                {
                    tmpL = template_codeL.line2.Replace("XXXX", String.Format("{0:0000}", j));
                    tmpR = template_codeR.line2.Replace("XXXX", String.Format("{0:0000}", j));
                }
                else
                {
                    if (template_codeL.idModel == 167 || template_codeL.idModel == 168)
                    {
                        tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", j));
                        tmpR = template_codeR.line2.Replace("XXXXXX", String.Format("{0:000000}", j));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                        tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                    }
                }
                if (template_codeL.idModel == 171 || template_codeL.idModel == 172)
                {
                    String hour = String.Format("{0:00}", DateTime.Now.Hour);
                    String minu = String.Format("{0:00}", DateTime.Now.Minute);
                    String sec = String.Format("{0:00}", DateTime.Now.Second);

                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                    sec = String.Format("{0:00}", DateTime.Now.Second);
                    tmpR = template_codeR.line2.Replace("XXXXXX", String.Format("{0:000000}", hour + minu + sec));
                    Thread.Sleep(1000);
                }

                byte[] zplL = Utils.getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = Utils.getBytesString(template_codeR, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += Utils.getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                            if (m_webservice.receive())
                            {
                                rowsBot += Utils.getHtmlLbl(m_webservice.FileName);
                                tblRows += Utils.getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            json += "\"result\":\"true\",";
            json += "\"tblTop\":\"" + tblRows + "\",";
            json += "\"htmlTop\":\"" + rowsTop + "\",";
            json += "\"htmlBot\":\"" + rowsBot + "\"";

            json += "}";
            return json;
        }
        public bool generateSerials(CModelPrint model, ref List<CFolios> allFolios)
        {
            bool result = false;
            int valIni = 0;
            int MAXSERIAL = 0;
            DateTime temp;
            List<generateSerialsByDJ_LED_2V3_Result> regFolios = new List<generateSerialsByDJ_LED_2V3_Result>();
            List<generateWabcoSerials_Result> regFoliosWabco = new List<generateWabcoSerials_Result>();

            try
            {
                if (DateTime.TryParse(model.Date_plan, out temp))
                {
                    if (m_dbM.isSerialContinuos(model.Id_model).First().isContinuos == 1)
                        valIni = Convert.ToInt32(m_db.getLastSerialLed2_V2(model.Id_model).First().lastSerial);
                    else
                        //if (m_db.checkDateDJV2(model.Id_model, temp.Day.ToString() + "/" + temp.Month.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                        if (m_db.checkDateDJLed2(model.Id_model, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                        valIni = Convert.ToInt32(m_db.getLastSerialBDJLed2V2(model.Dj_group,model.Id_model).First().lastSerial);
                    else
                        valIni = 1;

                    MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialLed2ByDjV3(model.Dj_group, model.Id_model, model.Id_side).First().MAXSERIAL);

                    if (MAXSERIAL > 0)
                        if (MAXSERIAL > valIni)
                            valIni = MAXSERIAL;

                    //generateSerialsByDJV2_Result resultSerials = m_db.generateSerialsByDJV2(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color, 
                    //                                                                        model.Id_Volt, model.Id_rev, model.Date_plan,valIni, model.Id_side).First();

                    if (m_dbM.isWabcoModel(model.Id_model).First().RESULT == -1)
                    {

                        regFolios = m_db.generateSerialsByDJ_LED_2V3(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color,
                                                                                                model.Id_Volt, model.Id_rev, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(),
                                                                                                valIni, model.Id_side, model.Id_flux1, model.Id_Volt1, model.Id_color1, model.Id_model).ToList();
                        if (regFolios.First().printFolio == -1)
                            result = false;
                        else
                        {
                            foreach (generateSerialsByDJ_LED_2V3_Result resFolios in regFolios)
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
                        regFoliosWabco = m_db.generateWabcoSerials(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color,
                                                                                                model.Id_Volt, model.Id_rev, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(),
                                                                                                valIni, model.Id_side, model.Id_flux1, model.Id_Volt1, model.Id_color1, model.Id_model).ToList();

                        if (regFoliosWabco.First().printFolio == -1)
                            result = false;
                        else
                        {
                            foreach (generateWabcoSerials_Result resFolios in regFoliosWabco)
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
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool printSerials(CModelPrint modelToPrint, List<CFolios> allFolios, string f, string c, string v, int idPrinter)
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
            getGeneratedSerialsByDJ_LED2_V3_Result serial;

            foreach (CFolios folios in allFolios)
            {

                serial = m_db.getGeneratedSerialsByDJ_LED2_V3(modelToPrint.Dj_group, folios.PrintFolio, modelToPrint.Id_side,model.se_id_model).First();

                if (serial == null)
                    result = false;
                else
                {
                    getLabelByModel_Result label = m_db.getLabelByModel(modelToPrint.Id_model, f, c, v).First();
                    if (m_printer.searchPrinter(ref namePrinter))
                    {
                        getTemplateLblPrint_Result template = m_db.getTemplateLblPrint(folios.Full_template, folios.Line_bot_template).First();
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