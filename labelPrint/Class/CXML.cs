using labelPrint.Models;
using pendingProds.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public class CXML
    {
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

        public String sendLRSidesToXML(generate_lblCode_template2_Result template_codeL, generate_lblCode_template2_Result template_codeR, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, int isRem, DateTime dateDJ, int idPrinter, String djGroup, String type_print)
        {

            String json = "{";
            String XML = "";
            String tmpL = "", tmpR = "";
            String errorMessage = "";
            int valIni = 0;
            int existsL = 0, existsR = 0;
            int printFolio = 0;
            int MAXSERIAL = 0;
            COracle m_oracle = new COracle("172.25.0.15", "MXPRD");

            if (type_print == "FOLIO")
                printFolio = Convert.ToInt32(m_db.getPrintFolioDJ(djGroup).First().PRINTFOLIO);
            else
                printFolio = 0;

            if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            else
                if (isRem == 1)
                valIni = Convert.ToInt32(m_db.getLastSerialByDate(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().lastSerial);
            else valIni = 1;
            MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialByDj(djGroup, template_codeL.idModel).First().MAXSERIAL);

            if (MAXSERIAL > 0)
            {
                if (MAXSERIAL > valIni)
                {
                    valIni = MAXSERIAL;
                }
            }
            int i = 0;
            //getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            getXMLByModel_Result varXML = m_db.getXMLByModel(template_codeL.idModel).First();
            DataTable modelInfo = m_oracle.getQtyPanelsByDJGroup(djGroup);
            String serialIni = "";

            if (modelInfo != null)
            {
                XML = "<prints>";

                for (i = valIni; i < num_lbls + valIni; i++)
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    if (i == valIni) serialIni = tmpL;

                    if (m_db.insertSerialV2(template_codeL.line1 + tmpL, djGroup, template_codeL.line1, tmpL, 1, printFolio, i, template_codeL.idModel).First().RESULT == 1)
                        existsL = 0;
                    else existsL = 1;
                    if (m_db.insertSerialV2(template_codeR.line1 + tmpR, djGroup, template_codeR.line1, tmpR, 2, printFolio, i, template_codeR.idModel).First().RESULT == 1)
                        existsR = 0;
                    else existsR = 1;

                    if (existsL == 0 && existsR == 0)
                    {

                    }
                    else num_lbls++;
                }

                folios.Init_folio = valIni;
                folios.Last_folio = i - 1;
                folios.Curr_folio = i - 1;
                folios.PrintFolio = printFolio;
            }
            json += "\"result\" : \"true\",";
            json += "\"initfolio\" : \"" + folios.Init_folio + "\",";
            json += "\"lastfolio\" : \"" + folios.Last_folio + "\",";
            json += "\"printfolio\" : \"" + folios.PrintFolio + "\",";
            json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
            result = true;

            json += "}";

            return json;
        }
    }
}