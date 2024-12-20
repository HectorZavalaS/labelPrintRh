using labelPrint.Models;
using pendingProds.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace labelPrint.Class
{
    public class COpXML
    {
        private CLblWEBService m_webservice;
        private siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        private siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();
        private siixsem_laser_mark_cfgEntities m_dbLM = new siixsem_laser_mark_cfgEntities();
        private CPrintController m_printer = new CPrintController();
        private Utils m_utils = new Utils();
        CCogiscanCGS m_db2 = new CCogiscanCGS();
        ftp m_ftp;// = new ftp(, "ftpUser", "ftpUser");

        public CLblWEBService Webservice { get => m_webservice; set => m_webservice = value; }
        public siixsem_sys_lblPrint_dbEntities Db { get => m_db; set => m_db = value; }
        public siixsem_main_dbEntities DbM { get => m_dbM; set => m_dbM = value; }
        public CPrintController Printer { get => m_printer; set => m_printer = value; }
        public Utils Utils { get => m_utils; set => m_utils = value; }

        public bool generateSerials(CModelPrint model, ref List<CFolios> allFolios)
        {
            bool result = false;
            int valIni = 0;
            int MAXSERIAL = 0;
            DateTime temp;
            List<generateLaserSerials_Result> regFolios = new List<generateLaserSerials_Result>();
            List<generateWabcoSerials_Result> regFoliosWabco = new List<generateWabcoSerials_Result>();

            try
            {
                if (DateTime.TryParse(model.Date_plan, out temp))
                {
                    if (m_dbM.isSerialContinuos(model.Id_model).First().isContinuos == 1)
                        valIni = Convert.ToInt32(m_db.getLastSerialLaserMark(model.Id_model).First().lastSerial);
                    else
                        //if (m_db.checkDateDJV2(model.Id_model, temp.Day.ToString() + "/" + temp.Month.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                        if (m_db.checkDateDJLed2(model.Id_model, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(), model.Id_side).First().resp == "Y")
                        valIni = Convert.ToInt32(m_db.getLastSerialBDJLaserM(model.Dj_group, model.Id_model).First().lastSerial);
                    else
                        valIni = 1;

                    MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialLaserM(model.Dj_group, model.Id_model, model.Id_side).First().MAXSERIAL);

                    if (MAXSERIAL > 0)
                        if (MAXSERIAL > valIni)
                            valIni = MAXSERIAL;

                    //generateSerialsByDJV2_Result resultSerials = m_db.generateSerialsByDJV2(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color, 
                    //                                                                        model.Id_Volt, model.Id_rev, model.Date_plan,valIni, model.Id_side).First();


                    regFolios = m_db.generateLaserSerials(model.Dj_group, model.Num_labels, model.Id_flux, model.Id_color,
                                                                                            model.Id_Volt, model.Id_rev, temp.Month.ToString() + "/" + temp.Day.ToString() + "/" + temp.Year.ToString(),
                                                                                            valIni, model.Id_side, model.Id_flux1, model.Id_Volt1, model.Id_color1, model.Id_model).ToList();
                    if (regFolios.First().printFolio == -1)
                        result = false;
                    else
                    {
                        foreach (generateLaserSerials_Result resFolios in regFolios)
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
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool markPCBSerials(CModelPrint modelToPrint, List<CFolios> allFolios, string f, string c, string v, int idPrinter, String path)
        {
            bool result = false;
            String errorMessage = "";


            getLaserMark_Result laser_mark = m_dbLM.getLaserMark(idPrinter).First();

            m_ftp = new ftp(laser_mark.se_ip, "ftpUser", "ftpUser");

            String xml = "";
            String namePrinter = "";
            CModelInfo modelInfo;

            getModelByID_Result model = m_dbM.getModelByID(modelToPrint.Id_model).First();
            getGeneratedLaserSerials_Result serial;
            siixsem_laser_mark_cfgEntities m_lm_db = new siixsem_laser_mark_cfgEntities();
            modelInfo = m_db2.getInfoModel(m_dbM.getNameCogiscan(modelToPrint.Id_model).First().COGISCANNAME);

            foreach (CFolios folios in allFolios)
            {
                m_lm_db.setConfigToLM(idPrinter, modelInfo.FG_NAME, modelInfo.REV, modelInfo.ROUTE, m_lm_db.getLaserPrg(modelToPrint.Id_model).First().se_prg_name, modelToPrint.Dj_group,
                    modelToPrint.Num_panels,modelToPrint.Num_labels, model.se_has_panelLbl, 0, modelToPrint.Num_labels);
                    
                serial = m_db.getGeneratedLaserSerials(modelToPrint.Dj_group, folios.PrintFolio, modelToPrint.Id_side, modelToPrint.Id_model).First();

                if (serial == null)
                    result = false;
                else
                {
                    getXMLPanel_Result panel; 
                    int count = 0;
                    int folioInicial = folios.Init_folio;
                    int folio_final = folios.Last_folio;
                    for(;count < modelToPrint.Num_panels; count++)
                    {
                        //xml = m_db.getXMLPanel(folios.Init_folio, folios.Full_template, modelToPrint.Id_model, modelToPrint.Dj_group, modelToPrint.Num_labels,
                        //    modelToPrint.Id_flux, modelToPrint.Id_color, modelToPrint.Id_Volt, modelToPrint.Id_rev, modelToPrint.Date_plan, modelToPrint.Id_side, modelToPrint.Id_flux1, modelToPrint.Id_Volt1, modelToPrint.Id_color1).First().XML;

                        panel = m_db.getXMLPanel(folioInicial, folios.Full_template, modelToPrint.Id_model, modelToPrint.Dj_group, modelToPrint.Num_labels,
                                                   modelToPrint.Id_flux, modelToPrint.Id_color, modelToPrint.Id_Volt, modelToPrint.Id_rev, modelToPrint.Date_plan, modelToPrint.Id_side, modelToPrint.Id_flux1, modelToPrint.Id_Volt1, modelToPrint.Id_color1).First();

                        writeXML(modelToPrint, panel,path);
                        folioInicial = Convert.ToInt32(panel.LAST_SERIAL);
                        m_dbLM.setTaskToLM(idPrinter, "MARKING", "IN_PROGRESS");
                        waitForMarkComplete(idPrinter);
                        //folios.Last_folio = model.se_has_panelLbl == 0 ? Convert.ToInt32(folios.Init_folio + modelToPrint.Num_labels) : Convert.ToInt32(folios.Init_folio + modelToPrint.Num_labels) + 1;
                    }

                }
            }

            return result;
        }
        private bool writeXML(CModelPrint model, getXMLPanel_Result panel, String path)
        {
            bool result = false;

            path += "\\" + model.Dj_group.Trim() + "\\";
            String filename = m_dbLM.getLaserPrg(model.Id_model).First().se_prg_name + "_" + panel.SERIAL_PANEL + ".xml";
            String fullpath = path + filename; 
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if(File.Exists(fullpath)){
                File.Delete(fullpath);
            }
            File.WriteAllText(fullpath, panel.XML);
            try
            {
                m_ftp.upload(filename, fullpath);
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }
        private void waitForMarkComplete(int idLM)
        {
            String Task = "";
            getStatus_Result m_statusLM;

            while (!Task.Contains("MARK_COMPLETE")) {
                m_statusLM = m_dbLM.getStatus(idLM).First();
                Task = m_statusLM.se_task;
                Thread.Sleep(1000);
            }

        }
    }
}