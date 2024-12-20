using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace labelPrint.Class
{
    public class CPrintZPL_Led2
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

        #region PREVIEW_METHODS
        /***************************************************************************************/
        /***************************************************************************************/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template_codeL"></param>
        /// <param name="num_lbls"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="dateDJ"></param>
        /// <returns></returns>

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
                                if(template_codeL.idModel == 314)
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


        /***************************************************************************************/
        /***************************************************************************************/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template_codeL"></param>
        /// <param name="template_codeR"></param>
        /// <param name="num_lbls"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="isRem"></param>
        /// <param name="dateDJ"></param>
        /// <returns></returns>

        public String getLRSidesPreviewLed2(generate_lblCode_templateLed2_Result template_codeL, generate_lblCode_templateLed2_Result template_codeR, Double num_lbls, string f, String c, string v, int isRem, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";
            int valIni = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();

            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
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


        #endregion

        #region PRINT_METHODS
        /***************************************************************************************/
        /***************************************************************************************/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template_codeL"></param>
        /// <param name="num_lbls"></param>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="folios"></param>
        /// <param name="dateDJ"></param>
        /// <returns></returns>
        /// 

        public String sendUniqueSideToPrinterLed2_2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, DateTime dateDJ, int idPrinter, String djGrp, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;
            int existsL = 0;
            int existsR = 0;
            int printFolio = 0;
            int MAXSERIAL = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            { }

            //if (m_printer.searchPrinter(ref namePrinter))
            //{
                if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                    valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                else
                    if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                        valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                    else
                        valIni = 1;
                valFin = valIni + num_lbls;
                String serialIni = "";
                MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialByDj(djGrp,template_codeL.idModel).First().MAXSERIAL);

                if (MAXSERIAL > 0)
                {
                    if (MAXSERIAL > valIni)
                    {
                        valIni = MAXSERIAL;
                        valFin = valIni + num_lbls;
                    }
                }
                int print = 1;
                for (i = valIni; i < valFin; i += 3)
                {
                    if (template_codeL.idModel == 167 || template_codeL.idModel == 168 || template_codeL.idModel == 169 || template_codeL.idModel == 170 || template_codeL.idModel == 182 || template_codeL.idModel == 200 || template_codeL.idModel == 201
                        || template_codeL.idModel == 118 || template_codeL.idModel == 119 || template_codeL.idModel == 120 || template_codeL.idModel == 196 || template_codeL.idModel == 189 || template_codeL.idModel == 202)
                    {
                        tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                        tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i + 1));
                        tmpRR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i + 2));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                        tmpRR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 2));
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
                    if (i == valIni) serialIni = tmpL;
                    //print = 1;

                    if(m_db.insertSerialV2(template_codeL.line1 + tmpL, djGrp, template_codeL.line1, tmpL,3,printFolio,(int)i, template_codeL.idModel).First().RESULT == 1)
                        existsL = 0;
                    else existsL = 1;

                    if(m_db.insertSerialV2(template_codeL.line1 + tmpR, djGrp, template_codeL.line1, tmpR,3,printFolio,(int)i + 1, template_codeL.idModel).First().RESULT == 1)
                        existsR = 0;
                    else existsR = 1;

                    strZPL += Utils.replaceLabel(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);

                    if( print == 5)
                    {
                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                        {
                            json += "\"result\" : \"false\",";
                            json += "\"messageError\":\"" + "Error: " + errorMessage + "\",";
                            result = false;
                        }
                        print = 1;
                        //Thread.Sleep(800);
                        serialIni = tmpL;
                        strZPL = "";
                    }
                    print++;
                }
                folios.Init_folio = Convert.ToInt32(valIni);
                folios.Last_folio = Convert.ToInt32(valFin);
                folios.Curr_folio = Convert.ToInt32(valFin);
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + valFin + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
            //}
            //else
            //{
            //    json += "\"result\" : \"false\",";
            //    json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            //}
            //json += "}";
            return json;
        }

        /***************************************************************************************/
        /***************************************************************************************/

            /// <summary>
            /// 
            /// </summary>
            /// <param name="template_codeL"></param>
            /// <param name="num_lbls"></param>
            /// <param name="result"></param>
            /// <param name="f"></param>
            /// <param name="c"></param>
            /// <param name="v"></param>
            /// <param name="folios"></param>
            /// <param name="dateDJ"></param>
            /// <returns></returns>

        public String sendUniqueSideToPrinterLed2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, DateTime dateDJ, int idPrinter, String djGrp, String type_print, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;
            int existsL = 0;
            int existsR = 0;
            int printFolio = 0;
            int print = 1;
            int MAXSERIAL = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            if (type_print == "FOLIO")
                printFolio = Convert.ToInt32(m_db.getPrintFolioDJ_Led2(djGrp).First().PRINTFOLIO);
            else
                printFolio = 0;

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            { }


            //if (m_printer.searchPrinter(ref namePrinter))
            //{
                if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                    valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                else
                    if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                        valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                    else
                        valIni = 1;
                valFin = valIni + num_lbls;
                String serialIni = "";

                MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialByDj(djGrp, template_codeL.idModel).First().MAXSERIAL);

                if (MAXSERIAL > 0)
                {
                    if (MAXSERIAL > valIni)
                    {
                        valIni = MAXSERIAL;
                        valFin = valIni + num_lbls;
                    }
                }

                for (i = valIni; i <= valFin; i += 2)
                {

                    if (template_codeL.idModel == 182 || template_codeL.idModel == 120 || template_codeL.idModel == 189 || template_codeL.idModel == 119 || template_codeL.idModel == 118
                    || template_codeL.idModel == 196 || template_codeL.idModel == 200 || template_codeL.idModel == 201 || template_codeL.idModel == 202 || template_codeL.idModel == 305)
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
                    try
                    {
                        if(template_codeL.idModel == 314)
                        {
                            if(m_db.insertSerialV2(tmpL + template_codeL.line2, djGrp, template_codeL.line1, tmpL, 3, printFolio, (int)i, template_codeL.idModel).First().RESULT == 1)
                                existsL = 0;
                            else existsL = 1;
                            if(m_db.insertSerialV2(tmpR + template_codeL.line2, djGrp, template_codeL.line1, tmpR, 3, printFolio, (int)i + 1, template_codeL.idModel).First().RESULT == 1)
                                existsR = 0;
                            else existsR = 1;
                        }
                        else
                        {
                            if(m_db.insertSerialV2(template_codeL.line1 + tmpL, djGrp, template_codeL.line1, tmpL, 3, printFolio, (int)i, template_codeL.idModel).First().RESULT == 1)
                                existsL = 0;
                            else existsL = 1;
                            if(m_db.insertSerialV2(template_codeL.line1 + tmpR, djGrp, template_codeL.line1, tmpR, 3, printFolio, (int)i + 1, template_codeL.idModel).First().RESULT == 1)
                                existsR = 0;
                            else existsR = 1;
                        }
                    }
                    catch(Exception ex)
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\":\"" + "Error: " + ex.Message + "\",";
                        result = false;
                    }

                    if (i == valIni) serialIni = tmpL;

                    if (existsL == 0 && existsR == 0){
                        strZPL += Utils.replaceLabel(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);
                        //print = 1;
                        if (print == 5)
                        {
                            if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGrp + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                            {
                                json += "\"result\" : \"false\",";
                                json += "\"messageError\":\"" + "Error: " + errorMessage + "\",";
                                result = false;
                            }
                            print = 1;
                            //Thread.Sleep(600);
                            strZPL = "";
                            serialIni = tmpL;
                        }
                        print++;
                    }
                    else valFin+=2;
                }
                if (!String.IsNullOrEmpty(strZPL))
                {
                    if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGrp + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                        result = false;
                }
                folios.Init_folio = Convert.ToInt32(valIni);
                folios.Last_folio = Convert.ToInt32(valFin);
                folios.Curr_folio = Convert.ToInt32(valFin);
                folios.PrintFolio = printFolio;
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + valFin + "\",";
                json += "\"lastfolio\" : \"" + folios.PrintFolio + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
            //}
            //else
            //{
            //    json += "\"result\" : \"false\",";
            //    json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            //}
            //json += "}";
            return json;
        }

        /***************************************************************************************/
        /***************************************************************************************/

            /// <summary>
            /// 
            /// </summary>
            /// <param name="template_codeL"></param>
            /// <param name="template_codeR"></param>
            /// <param name="num_lbls"></param>
            /// <param name="result"></param>
            /// <param name="f"></param>
            /// <param name="c"></param>
            /// <param name="v"></param>
            /// <param name="folios"></param>
            /// <param name="isRem"></param>
            /// <param name="dateDJ"></param>
            /// <returns></returns>
            /// 

        public String sendLRSidesLed2ToPrinter(generate_lblCode_templateLed2_Result template_codeL, generate_lblCode_templateLed2_Result template_codeR, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, int isRem, DateTime dateDJ, int idPrinter, String djGroup, String type_print, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            int valIni = 0;
            int existsL = 0, existsR = 0;
            int printFolio = 0;
            int zplPrint = 1;
            int MAXSERIAL = 0;

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            { }

            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                if (isRem == 1)
                    valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                else valIni = 1;

            int i = 0;
            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            if (type_print == "FOLIO")
                printFolio = Convert.ToInt32(m_db.getPrintFolioDJ_Led2(djGroup).First().PRINTFOLIO);
            else
                printFolio = 0;
            String serialIni = "";
            MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialByDj(djGroup, template_codeL.idModel).First().MAXSERIAL);

            if (MAXSERIAL > 0)
            {
                if (MAXSERIAL > valIni)
                    valIni = MAXSERIAL;
            }
            //if (m_printer.searchPrinter(ref namePrinter))
            //{
                    for (i = valIni; i < num_lbls + valIni; i++)
                    {
                        if (template_codeL.idModel == 46)
                        {
                            tmpL = template_codeL.line2.Replace("XXXX", String.Format("{0:0000}", i));
                            tmpR = template_codeR.line2.Replace("XXXX", String.Format("{0:0000}", i));
                        }
                        else
                        {
                            tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                            tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
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
                        if (i == valIni) serialIni = tmpL;

                        if (m_db.existSerial_Led2(template_codeL.line1 + tmpL).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(template_codeL.line1 + tmpL, djGroup, template_codeL.line1, tmpL, 1, printFolio, i,  template_codeL.idModel);
                            existsL = 0;
                        }
                        else existsL = 1;

                        if (m_db.existSerial_Led2(template_codeR.line1 + tmpR).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(template_codeR.line1 + tmpR, djGroup, template_codeR.line1, tmpR, 2, printFolio, i,template_codeR.idModel);
                            existsR = 0;
                        }
                        else existsR = 1;
                        if (existsL == 0 && existsR == 0)
                        {
                            strZPL += Utils.replaceLabel(template_codeL, template_codeR, tmpL, tmpR, tmpRR, label.zpl_two);
                            if (zplPrint == 30)
                            {
                                if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGroup + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                                {
                                    json += "\"result\" : \"false\",";
                                    json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                                    result = false;
                                }
                                //Thread.Sleep(600);
                                strZPL = "";
                                serialIni = tmpL;
                                zplPrint=1;
                            }
                            zplPrint++;
                        }
                        else num_lbls++;
                        
                    }
                    if (!String.IsNullOrEmpty(strZPL))
                    {
                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGroup + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                            result = false;
                    }
                    folios.Init_folio = valIni;
                    folios.Last_folio = i - 1;
                    folios.Curr_folio = i - 1;
                    folios.PrintFolio = printFolio;
                    json += "\"result\" : \"true\",";
                    json += "\"initfolio\" : \"" + folios.Init_folio + "\",";
                    json += "\"lastfolio\" : \"" + folios.Last_folio + "\",";
                    json += "\"printfolio\" : \"" + folios.PrintFolio + "\",";
                    json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
                    result = true;

            //}
            //else
            //{
            //    json += "\"result\" : \"false\",";
            //    json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada" + "\"";
            //    result = false;
            //}
            //json += "}";

            return json;
        }

        /***************************************************************************************/
        /***************************************************************************************/
        /// <summary>
        /// IMPRESION DE MODELOS LDM EN ETIQUETA TRES AL PASO SOLO PARA U725 KIT Y U725 CHSML
        /// </summary>
        /// <param name="template_codeL"></param>
        /// <param name="num_lbls"></param>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="folios"></param>
        /// <param name="dateDJ"></param>
        /// <param name="idPrinter"></param>
        /// <param name="djGrp"></param>
        /// <param name="type_print"></param>
        /// <returns></returns>

        public String sendUniqueSideToPrinterLed2T(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, DateTime dateDJ, int idPrinter, String djGrp, String type_print, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;
            int existsL = 0;
            int existsR = 0;
            int existsRR = 0;
            int printFolio = 0;
            int print = 1;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            if (type_print == "FOLIO")
                printFolio = Convert.ToInt32(m_db.getPrintFolioDJ_Led2(djGrp).First().PRINTFOLIO);
            else
                printFolio = 0;

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            { }


            //if (m_printer.searchPrinter(ref namePrinter))
            //{
            if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                valIni = 1;
            valFin = valIni + num_lbls;
            String serialIni = "";

            for (i = valIni; i <= valFin; i += 3)
            {

                if (template_codeL.idModel == 314)
                {
                    tmpL = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    tmpRR = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i + 2));
                }
                else
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                }

                try
                {
                    if (template_codeL.idModel == 314)
                    {
                        if (m_db.existSerial_Led2(tmpL + template_codeL.line2).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(tmpL + template_codeL.line2, djGrp, template_codeL.line1, tmpL, 3, printFolio, (int)i, template_codeL.idModel);
                            existsL = 0;
                        }
                        else existsL = 1;
                        if (m_db.existSerial_Led2(tmpR + template_codeL.line2).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(tmpR + template_codeL.line2, djGrp, template_codeL.line1, tmpR, 3, printFolio, (int)i + 1, template_codeL.idModel);
                            existsR = 0;
                        }
                        else existsR = 1;
                        if (m_db.existSerial_Led2(tmpRR + template_codeL.line2).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(tmpRR + template_codeL.line2, djGrp, template_codeL.line1, tmpRR, 3, printFolio, (int)i + 2, template_codeL.idModel);
                            existsRR = 0;
                        }
                        else existsRR = 1;
                    }
                    else
                    {
                        if (m_db.existSerial_Led2(template_codeL.line1 + tmpL).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(template_codeL.line1 + tmpL, djGrp, template_codeL.line1, tmpL, 3, printFolio, (int)i, template_codeL.idModel);
                            existsL = 0;
                        }
                        else existsL = 1;
                        if (m_db.existSerial_Led2(template_codeL.line1 + tmpR).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(template_codeL.line1 + tmpR, djGrp, template_codeL.line1, tmpR, 3, printFolio, (int)i + 1, template_codeL.idModel);
                            existsR = 0;
                        }
                        else existsR = 1;
                        if (m_db.existSerial_Led2(template_codeL.line1 + tmpRR).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(template_codeL.line1 + tmpRR, djGrp, template_codeL.line1, tmpRR, 3, printFolio, (int)i + 1, template_codeL.idModel);
                            existsRR = 0;
                        }
                        else existsRR = 1;
                    }


                }
                catch (Exception ex)
                {
                    json += "\"result\" : \"false\",";
                    json += "\"messageError\":\"" + "Error: " + ex.Message + "\"";
                    result = false;
                }

                if (i == valIni) serialIni = tmpL;

                if (existsL == 0 && existsR == 0 && existsRR == 0)
                {
                    if(template_codeL.idModel == 314)
                        strZPL += Utils.replaceLabelTr(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);
                    else
                        strZPL += Utils.replaceLabel(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);
                    //print = 1;
                    if (print == 5)
                    {
                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGrp + ", " + serialIni + template_codeL.line2 + " to " + tmpL + template_codeL.line2))
                        {
                            json += "\"result\" : \"false\",";
                            json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                            result = false;
                        }
                        print = 1;
                        //Thread.Sleep(600);
                        strZPL = "";
                        serialIni = tmpL;
                    }
                    print++;
                }
                else valFin += 3;
            }
            if (!String.IsNullOrEmpty(strZPL))
            {
                if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGrp + ", " + serialIni + template_codeL.line2 + " to " + tmpL + template_codeL.line2))
                    result = false;
            }
            folios.Init_folio = Convert.ToInt32(valIni);
            folios.Last_folio = Convert.ToInt32(valFin);
            folios.Curr_folio = Convert.ToInt32(valFin);
            folios.PrintFolio = printFolio;
            json += "\"result\" : \"true\",";
            json += "\"initfolio\" : \"" + valIni + "\",";
            json += "\"lastfolio\" : \"" + valFin + "\",";
            json += "\"lastfolio\" : \"" + folios.PrintFolio + "\",";
            json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
            //}
            //else
            //{
            //    json += "\"result\" : \"false\",";
            //    json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            //}
            //json += "}";
            return json;
        }

        /// <summary>
        /// IMPRESION DE ETIQUETAS 1 AL PASO
        /// </summary>
        /// <param name="template_codeL"></param>
        /// <param name="num_lbls"></param>
        /// <param name="result"></param>
        /// <param name="f"></param>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="folios"></param>
        /// <param name="isRem"></param>
        /// <param name="dateDJ"></param>
        /// <param name="idPrinter"></param>
        /// <param name="djGroup"></param>
        /// <param name="type_print"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public String sendLRSidesToPrinterV2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string f, string c, string v, ref CFolios folios, int isRem, DateTime dateDJ, int idPrinter, String djGroup, String type_print, int side)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String errorMessage = "";
            int MAXSERIAL = 0;
            String strZPL = "";
            String namePrinter = "";
            int valIni = 0;
            int existsL = 0, existsR = 0;
            int printFolio = 0;
            int zplPrint = 1;

            try
            {
                namePrinter = m_dbM.getPrintrById(idPrinter).First().se_description;
            }
            catch (Exception ex)
            { }

            if (type_print == "FOLIO")
                printFolio = Convert.ToInt32(m_db.getPrintFolioDJ(djGroup).First().PRINTFOLIO);
            else
                printFolio = 0;

            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString(),side).First().resp == "Y")
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else
                if (isRem == 1)
                valIni = Convert.ToInt32(m_db.getLastSerialByDateLed2(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().lastSerial);
            else valIni = 1;

            int i = 0;
            MAXSERIAL = Convert.ToInt32(m_db.getMaxSerialLed2ByDj(djGroup,template_codeL.idModel, side).First().MAXSERIAL);

            if (MAXSERIAL > 0)
            {
                if (MAXSERIAL > valIni)
                    valIni = MAXSERIAL;
            }
            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, f, c, v).First();
            String serialIni = "";

            if (m_printer.searchPrinter(ref namePrinter))
            {
                DateTime today = DateTime.Now;

                int hh = today.Hour;
                int mm = today.Minute;
                int ss = today.Second;

                for (i = valIni; i < num_lbls + valIni; i++)
                {
                    if (template_codeL.idModel == 428 || template_codeL.idModel == 429 || template_codeL.idModel == 430 || template_codeL.idModel == 431 || template_codeL.idModel == 432 || template_codeL.idModel == 433)
                    {
                        if(ss<60)
                        {
                            String serial = String.Format("{0:00}", hh) + String.Format("{0:00}", mm) + String.Format("{0:00}", ss);
                            tmpL = template_codeL.line2.Replace("XXXXXX", serial);
                        }

                        if (ss == 60)
                        {
                            ss = 0;
                            if (mm == 60)
                            {
                                mm = 0;
                                if (hh == 60) hh = 0;
                                else hh++;
                            }
                            else mm++;
                        }
                        else ss++;


                    }
                    else
                        if (template_codeL.idModel == 182 || template_codeL.idModel == 120 || template_codeL.idModel == 189 || template_codeL.idModel == 119 || template_codeL.idModel == 118
    ||                      template_codeL.idModel == 196 || template_codeL.idModel == 200 || template_codeL.idModel == 201 || template_codeL.idModel == 202 || template_codeL.idModel == 305)
                        {
                            tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                        }
                        else
                            if (template_codeL.idModel == 314)
                                tmpL = template_codeL.line1.Replace("XXXXX", String.Format("{0:00000}", i));
                            else
                                tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    //tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    if (i == valIni) serialIni = tmpL;
                    if (template_codeL.idModel == 314)
                    {
                        if (m_db.existSerial_Led2(tmpL + template_codeL.line2).First().RESULT == -1)
                        {
                            m_db.insertSerialV2(tmpL + template_codeL.line2, djGroup, tmpL, template_codeL.line2, 3, printFolio, (int)i, template_codeL.idModel);
                            existsL = 0;
                        }
                        else existsL = 1;
                    }
                    else
                    if (m_db.insertSerialV2(template_codeL.line1 + tmpL, djGroup, template_codeL.line1, tmpL, side, printFolio, i, template_codeL.idModel).First().RESULT == 1)
                        existsL = 0;
                    else existsL = 1;


                    if (existsL == 0)
                    {
                        if (idPrinter == 17 || idPrinter == 13)
                            strZPL += Utils.replaceLabelOSLed2(template_codeL, tmpL, label.zpl_two_cab);
                        else
                            strZPL += Utils.replaceLabelOSLed2(template_codeL, tmpL, label.zpl_one);
                        if (zplPrint == 5)
                        {
                            if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGroup + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                            {
                                json += "\"result\" : \"false\",";
                                json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                                result = false;
                            }
                            //Thread.Sleep(800);
                            serialIni = tmpL;
                            strZPL = "";
                            zplPrint = 1;
                        }
                        zplPrint++;
                    }
                    else num_lbls++;
                }
                if (!String.IsNullOrEmpty(strZPL))
                {
                    if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage, template_codeL.model_desc + ", DJ Group: " + djGroup + ", " + template_codeL.line1 + serialIni + " to " + template_codeL.line1 + tmpL))
                        result = false;
                }
                folios.Init_folio = valIni;
                folios.Last_folio = i - 1;
                folios.Curr_folio = i - 1;
                folios.PrintFolio = printFolio;
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + folios.Init_folio + "\",";
                json += "\"lastfolio\" : \"" + folios.Last_folio + "\",";
                json += "\"printfolio\" : \"" + folios.PrintFolio + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\",";
                result = true;
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro la impresora instalada." + "\",";
                result = false;
            }
            //json += "}";

            return json;
        }
 
        #endregion
    }
}