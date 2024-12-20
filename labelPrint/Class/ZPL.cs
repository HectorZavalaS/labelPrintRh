using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace labelPrint.Class
{
    public class ZPL
    {
        CLblWEBService m_webservice;
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();

        printer m_printer = new printer();

        /***************************************************************************************/
        /***************************************************************************************/
        /***************************************************************************************/
        public String getUniqueSidePreviewLed2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, string bin, string c, string v, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            double valIni;// = Convert.ToInt32(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);

            if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
            {
                valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            }
            else
                if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            }
            else
                valIni = 1;

            double valFin = valIni + 6;

            for (double i = valIni; i < valFin; i += 2)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                //if (template_codeL.idModel == 117)
                //{
                //    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", i));
                //    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", i + 1));
                //}
                if (template_codeL.idModel == 167 || template_codeL.idModel == 168 || template_codeL.idModel == 169 || template_codeL.idModel == 170 || template_codeL.idModel == 182
                    || template_codeL.idModel == 120 || template_codeL.idModel == 118 || template_codeL.idModel == 119 || template_codeL.idModel == 196 || template_codeL.idModel == 200 || template_codeL.idModel == 201 || template_codeL.idModel == 202)
                {
                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i+1));
                }
                else
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
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
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", i + 1));

                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeL, tmpR, label.zpl_preview);


                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsTop += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
                        }

                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            double tmp = valIni + num_lbls - 5;
            valFin = valIni + num_lbls;

            for (double j = tmp; j <= valFin; j += 2)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", j));
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", j));
                //if (template_codeL.idModel == 117)
                //{
                //    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", j));
                //    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", j + 1));
                //}
                if (template_codeL.idModel == 167 || template_codeL.idModel == 168 || template_codeL.idModel == 169 || template_codeL.idModel == 170 || template_codeL.idModel == 182
                    || template_codeL.idModel == 120 || template_codeL.idModel == 118 || template_codeL.idModel == 119 || template_codeL.idModel == 196 || template_codeL.idModel == 200 || template_codeL.idModel == 201 || template_codeL.idModel == 202)
                {
                    tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", j));
                    tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", j + 1));
                }
                else
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j + 1));
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
                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeL, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsBot += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
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

        public String getLRSidesPreviewLed2(generate_lblCode_templateLed2_Result template_codeL, generate_lblCode_templateLed2_Result template_codeR, Double num_lbls, string bin, String c, string v, int isRem, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";
            int valIni = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            }
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
                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeR, tmpR, label.zpl_preview);


                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsTop += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
                        }

                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            double tmp = num_lbls + valIni - 2;
            for (double j = tmp; j <= valIni + num_lbls; j++)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", j));
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", j));
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

                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeR, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsBot += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
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
        public String sendUniqueSideToPrinterLed2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string bin, string c, string v, ref CFolios folios, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR ="";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            if (m_printer.GetAllPrinterList(ref namePrinter, ref printers))
            {
                //valIni = Convert.ToDouble(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);

                if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                {
                    valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                }
                else
                    if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                {
                    valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                }
                else
                    valIni = 1;
                valFin = valIni + num_lbls;

                for (i = valIni; i < valFin; i += 2)
                {
                    //if (template_codeL.idModel == 117 || template_codeL.idModel == 116)
                    //{
                    //    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", i));
                    //    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:000000}", i + 1));
                    //}
                    if (template_codeL.idModel == 182 || template_codeL.idModel == 120 || template_codeL.idModel == 189 || template_codeL.idModel == 119 || template_codeL.idModel == 118 || template_codeL.idModel == 196 || template_codeL.idModel == 200 || template_codeL.idModel == 201 || template_codeL.idModel == 202)
                    {
                        tmpL = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i));
                        tmpR = template_codeL.line2.Replace("XXXXXX", String.Format("{0:000000}", i + 1));
                    }
                    else
                    {
                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
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
                    strZPL = replaceLabel(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);
                    if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage))
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                        result = false;
                        //break;
                    }
                }
                folios.Init_folio = Convert.ToInt32(valIni);
                folios.Last_folio = Convert.ToInt32(valFin);
                folios.Curr_folio = Convert.ToInt32(valFin);
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + valFin + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            }
            json += "}";
            return json;
        }
        public String sendUniqueSideToPrinterLed2_2(generate_lblCode_templateLed2_Result template_codeL, Double num_lbls, ref bool result, string bin, string c, string v, ref CFolios folios, DateTime dateDJ, int idSide)
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

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            if (m_printer.GetAllPrinterList(ref namePrinter, ref printers))
            {
                //valIni = Convert.ToDouble(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);

                if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                {
                    valIni = Convert.ToDouble(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                }
                else
                    if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
                {
                    valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
                }
                else
                    valIni = 1;
                valFin = valIni + num_lbls;

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
                    strZPL = replaceLabel(template_codeL, template_codeL, tmpL, tmpR, tmpRR, label.zpl_two);
                    if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage))
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                        result = false;
                        //break;
                    }
                }
                folios.Init_folio = Convert.ToInt32(valIni);
                folios.Last_folio = Convert.ToInt32(valFin);
                folios.Curr_folio = Convert.ToInt32(valFin);
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + valFin + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            }
            json += "}";
            return json;
        }

        public String sendLRSidesLed2ToPrinter(generate_lblCode_templateLed2_Result template_codeL, generate_lblCode_templateLed2_Result template_codeR, Double num_lbls, ref bool result, string bin, string c, string v, ref CFolios folios, int isRem, DateTime dateDJ, int idSide)
        {
            String json = "{";
            String tmpL = "", tmpR = "", tmpRR="";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            int valIni = 0;

            if (m_db.checkDateDJLed2(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString(), idSide).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            }
            else
            if (isRem == 1)
                valIni = Convert.ToInt32(m_db.getLastSerialLed2(template_codeL.idModel).First().lastSerial);
            else valIni = 1;

            int i = 0;
            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            if (m_printer.GetAllPrinterList(ref namePrinter, ref printers))
            {
                if (m_printer.isOnline(namePrinter))
                {
                    // for (int i = 2210; i < num_lbls; i++)
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
                        strZPL = replaceLabel(template_codeL, template_codeR, tmpL, tmpR,tmpRR, label.zpl_two);
                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage))
                        {
                            json += "\"result\" : \"false\",";
                            json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                            result = false;
                            //break;
                        }
                    }
                    //m_db.insertSpec(template_codeL.idModel)
                    folios.Init_folio = valIni + 1;
                    folios.Last_folio = i;
                    folios.Curr_folio = i;
                    json += "\"result\" : \"true\",";
                    json += "\"initfolio\" : \"" + valIni + "\",";
                    json += "\"lastfolio\" : \"" + i.ToString() + "\",";
                    json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
                    result = true;
                }
                else
                {
                    json += "\"result\" : \"false\",";
                    json += "\"messageError\":\"" + "La impresora esta apagada. Imposible imprimir. " + "\"";
                    result = false;
                }
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
                result = false;
            }
            json += "}";

            return json;
        }

        /***************************************************************************************/
        /***************************************************************************************/
        /***************************************************************************************/

        public String getLRSidesPreview(generate_lblCode_template2_Result template_codeL, generate_lblCode_template2_Result template_codeR, Double num_lbls, string bin, String c, string v, int isRem, DateTime dateDJ)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";
            int valIni = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin,c,v).First();

            if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            }
            else 
                if (isRem == 1)
                    valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
                else valIni = 1;

            for (int i = valIni; i < valIni + 3; i++)
            {
                tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                byte[] zplL = getBytesString(template_codeL,tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeR,tmpR, label.zpl_preview);


                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width,label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsTop += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
                        }

                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel,bin,c,v).First();

            double tmp = num_lbls + valIni - 2;
            for (double j = tmp; j <= valIni + num_lbls; j++)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", j));
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", j));
                tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", j));

                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeR, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsBot += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeR.line1 + tmpR);
                            }
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
        public String getUniqueSidePreview(generate_lblCode_template2_Result template_codeL,  Double num_lbls,string bin,string c, string v, DateTime dateDJ)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String rowsTop = "";
            String rowsBot = "";
            String tblRows = "";

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel,bin,c,v).First();

            double valIni;// = Convert.ToInt32(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);

            if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
            {
                valIni = Convert.ToDouble(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            }
            else
                if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            }
            else
                valIni = 1;

            double valFin = valIni + 6;

            for (double i = valIni; i < valFin; i+=2)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", i + 1));

                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeL, tmpR, label.zpl_preview);


                rowsTop += "<div class='row' style='margin:0px;'>";
                tblRows += "<tr>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        rowsTop += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsTop += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
                        }

                    }
                }
                tblRows += "</tr>";
                rowsTop += "</div>";
            }
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";
            tblRows += "<tr><center>";
            tblRows += getRowTbl("--  --  --  --  --", "--  --  --  --  --");
            tblRows += "</center></tr>";

            label = m_db.getLabelByModel(template_codeL.idModel,bin,c,v).First();

            double tmp = valIni + num_lbls - 5;
            valFin = valIni + num_lbls;

            for (double j = tmp; j <= valFin; j+=2)
            {

                //tmpL = template_codeL.template.Replace("XXXXX", String.Format("{0:00000}", j));
                //tmpR = template_codeR.template.Replace("XXXXX", String.Format("{0:00000}", j));
                tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j));
                tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", j + 1));

                byte[] zplL = getBytesString(template_codeL, tmpL, label.zpl_preview);
                byte[] zplR = getBytesString(template_codeL, tmpR, label.zpl_preview);

                tblRows += "<tr>";
                rowsBot += "<div class='row' style='margin:0px;'>";
                m_webservice = new CLblWEBService(zplL, label.width, label.height);
                if (m_webservice.send())
                {
                    if (m_webservice.receive())
                    {
                        Thread.Sleep(500);
                        rowsBot += getHtmlLbl(m_webservice.FileName);
                        m_webservice = new CLblWEBService(zplR, label.width, label.height);
                        if (m_webservice.send())
                        {
                            if (m_webservice.receive())
                            {
                                rowsBot += getHtmlLbl(m_webservice.FileName);
                                tblRows += getRowTbl(template_codeL.line1 + tmpL, template_codeL.line1 + tmpR);
                            }
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
        private String getHtmlLbl(String fileName)
        {
            return "<div class='columnprev'>" +
                    "     <div class='lblBox10x10'><center><img class='prevLbl' src='/aprinting/Labels/" + fileName + "' style='width: 100%;'></center></div>" +
                    "</div>";
        }
        private String getRowTbl(String codel, String coder)
        {
            return "<td>" + codel + "</td>" +
                   "<td>" + coder + "</td>";
        }
        private byte[] getBytesString(generate_lblCode_template2_Result template,String line2, String strZpl)
        {
            if (template.idModel == 92)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1);

            }
            if (template.idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", template.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", line2);
            }

            if (template.idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", template.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", line2);
            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", template.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);

            return Encoding.UTF8.GetBytes(strZpl);

        }

        private byte[] getBytesString(generate_lblCode_templateLed2_Result template, String line2, String strZpl)
        {
            if(template.idModel==117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
            }
            if (template.idModel == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
            }
            if (template.idModel == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
            }
            if (template.idModel == 46)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1U611.Replace("XXXX",line2));
            }
            if (template.idModel == 167 || template.idModel == 168 || template.idModel == 169 || template.idModel == 170)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1U611);

            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", template.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);

            return Encoding.UTF8.GetBytes(strZpl);

        }
        /***************************************************************************************/
        /***************************************************************************************/
        /***************************************************************************************/

        #region SEND_TO_PRINTER

        private String replaceLabel(generate_lblCode_template2_Result templateL, generate_lblCode_template2_Result templateR, String lineL2, String lineR2, String strZpl)
        {
            if (templateL.idModel == 92)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1);
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateR.line1);
            }
            if (templateL.idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (templateL.idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", templateR.line1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            return strZpl;

        }
        private String replaceLabel(generate_lblCode_templateLed2_Result templateL, generate_lblCode_templateLed2_Result templateR, String lineL2, String lineR2,String line3, String strZpl)
        {
            if (templateL.idModel == 117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0600");
            }
            if (templateL.idModel == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0590");
            }
            if (templateL.idModel == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0580");
            }

            if (templateL.idModel == 46)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611.Replace("XXXX",lineL2));
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", templateR.line1U611.Replace("XXXX",lineR2));
            }
            if (templateL.idModel == 167 || templateL.idModel == 168 || templateL.idModel == 169 || templateL.idModel == 170 || templateL.idModel == 171 || templateL.idModel == 172)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611);
                strZpl = strZpl.Replace("_SSSSSSSSSSSS_", templateR.line1);
                strZpl = strZpl.Replace("_TTTTTTTTTT_", line3);
                //_SSSSSSSSSSSS__TTTTTTTTTT_
            }

            //if (templateL.idModel == 171 || templateL.idModel == 172)
            //{
            //    strZpl = strZpl.Replace("_SSSSSSSSSSSS_", templateR.line1);
            //    strZpl = strZpl.Replace("_TTTTTTTTTT_", line3);
            //    //_SSSSSSSSSSSS__TTTTTTTTTT_
            //}
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", templateR.line1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);


            return strZpl;

        }
        public String sendLRSidesToPrinter(generate_lblCode_template2_Result template_codeL, generate_lblCode_template2_Result template_codeR, Double num_lbls, ref bool result, string bin, string c, string v, ref CFolios folios, int isRem, DateTime dateDJ)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            int valIni = 0;
            
            if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            }
            else 
            if (isRem == 1)
                valIni = Convert.ToInt32(m_db.getLastSerialByDate(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().lastSerial);
            else valIni = 1;

            int i = 0;
            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin,c,v).First();

            if (m_printer.GetAllPrinterList(ref namePrinter, ref printers))
            {
                if (m_printer.isOnline(namePrinter))
                {
                   // for (int i = 2210; i < num_lbls; i++)
                    for (i = valIni; i < num_lbls + valIni; i++)
                    {

                        tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                        strZPL = replaceLabel(template_codeL, template_codeR, tmpL, tmpR, label.zpl_two);
                        if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage))
                        {
                            json += "\"result\" : \"false\",";
                            json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                            result = false;
                            //break;
                        }
                    }
                    //m_db.insertSpec(template_codeL.idModel)
                    folios.Init_folio = valIni + 1;
                    folios.Last_folio = i;
                    folios.Curr_folio = i;
                    json += "\"result\" : \"true\",";
                    json += "\"initfolio\" : \"" + valIni + "\",";
                    json += "\"lastfolio\" : \"" + i.ToString() + "\",";
                    json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
                    result = true;
                }
                else
                {
                    json += "\"result\" : \"false\",";
                    json += "\"messageError\":\"" + "La impresora esta apagada. Imposible imprimir. " + "\"";
                    result = false;
                }
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
                result = false;
            }
            json += "}";
            
            return json;
        }
        public String sendUniqueSideToPrinter(generate_lblCode_template2_Result template_codeL, Double num_lbls, ref bool result, string bin, string c, string v,  ref CFolios folios, DateTime dateDJ)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String errorMessage = "";

            String strZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel,bin,c,v).First();

            if (m_printer.GetAllPrinterList(ref namePrinter, ref printers))
            {
                //valIni = Convert.ToDouble(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);

                if (m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1)
                {
                    valIni = Convert.ToDouble(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
                }
                else
                    if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
                {
                    valIni = Convert.ToInt32(m_db.getLastSerialByDate(template_codeL.idModel, dateDJ.Month.ToString() + "/" + dateDJ.Day.ToString() + "/" + dateDJ.Year.ToString()).First().lastSerial);
                }
                else
                    valIni = 1;
                valFin = valIni + num_lbls;

                for (i = valIni; i < valFin; i+=2)
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    strZPL = replaceLabel(template_codeL, template_codeL, tmpL, tmpR, label.zpl_two);
                    if (!m_printer.sendToPrinter(namePrinter, strZPL, ref errorMessage))
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                        result = false;
                       //break;
                    }
                }
                folios.Init_folio = Convert.ToInt32(valIni);
                folios.Last_folio = Convert.ToInt32(valFin);
                folios.Curr_folio = Convert.ToInt32(valFin);
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \""+valFin+"\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            }
            json += "}";
            return json;
        }

        public String sendLRSidesToPrinterIP(generate_lblCode_template2_Result template_codeL, generate_lblCode_template2_Result template_codeR, Double num_lbls, ref bool result, string bin, string c, string v, getPrintrById_Result printer, ref CFolios folios, DateTime dateDJ)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String errorMessage = "";
            int countZPL = 0;

            String strZPL = "";
            String zebraZPL = "";
            String printZPL = "";

            // num_lbls =  2460;
            int valIni = 0;

            if (m_db.checkDateDJ(template_codeL.idModel, dateDJ.Day.ToString() + "/" + dateDJ.Month.ToString() + "/" + dateDJ.Year.ToString()).First().resp == "Y")
            {
                valIni = Convert.ToInt32(m_db.getLastSerial(template_codeL.idModel).First().lastSerial);
            }
            else valIni = 0;

            int i = 0;
            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();
            if (printer.se_code == "110Xi4")
                zebraZPL = label.zpl_two;
            else
                zebraZPL = label.zpl_two_zt;

            if (m_printer.ZebraIsOnline(printer.se_ip))
            {
                // for (int i = 2210; i < num_lbls; i++)
                for (i = valIni; i < num_lbls + valIni; i++)
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    tmpR = template_codeR.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    strZPL = replaceLabel(template_codeL, template_codeR, tmpL, tmpR, zebraZPL);
                    countZPL++;
                    if (countZPL == 500) {
                        if (!m_printer.sendToPrinterIP(printer.se_ip, printZPL, ref errorMessage))
                        {
                            json += "\"result\" : \"false\",";
                            json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                            result = false;
                        }
                        countZPL = 0;
                        printZPL = "";
                    }
                    else
                    {
                        printZPL += strZPL;
                    }
                }
                folios.Init_folio = valIni + 1;
                folios.Last_folio = i;
                folios.Curr_folio = i;
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + i.ToString() + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\""; result = true;
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "La impresora esta apagada. Imposible imprimir. " + "\"";
                result = false;
            }

            json += "}";

            return json;
        }
        public String sendUniqueSideToPrinterIP(generate_lblCode_template2_Result template_codeL, Double num_lbls, ref bool result, string bin, string c, string v, getPrintrById_Result printer, ref CFolios folios)
        {
            String json = "{";
            String tmpL = "", tmpR = "";
            String errorMessage = "";

            String strZPL = "";
            String zebraZPL = "";
            String namePrinter = "";
            String printers = "";
            double i = 0;
            double valIni = 0;
            double valFin = 0;

            getLabelByModel_Result label = m_db.getLabelByModel(template_codeL.idModel, bin, c, v).First();

            if (printer.se_code == "110Xi4")
                zebraZPL = label.zpl_two;
            else
                zebraZPL = label.zpl_two_zt;

            if (m_printer.ZebraIsOnline(printer.se_ip))
            {
                valIni = Convert.ToDouble(m_dbM.isSerialContinuos(template_codeL.idModel).First().isContinuos == 1 ? m_db.getLastSerial(template_codeL.idModel).First().lastSerial : 1);
                valFin = valIni + num_lbls;

                for (i = valIni; i < valFin; i += 2)
                {
                    tmpL = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i));
                    tmpR = template_codeL.line2.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    strZPL = replaceLabel(template_codeL, template_codeL, tmpL, tmpR, zebraZPL);
                    
                    if (!m_printer.sendToPrinterIP(printer.se_ip, strZPL, ref errorMessage))
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\":\"" + "Error: " + errorMessage + "\"";
                        result = false;
                        //break;
                    }
                }
                json += "\"result\" : \"true\",";
                json += "\"initfolio\" : \"" + valIni + "\",";
                json += "\"lastfolio\" : \"" + valFin + "\",";
                json += "\"messageSuccess\":\"" + "Se imprimieron con éxito las etiquetas. " + "\"";
            }
            else
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\":\"" + "No se encontro una impresora Zebra instalada. Impresoras disponibles: " + printers + "\"";
            }
            json += "}";
            return json;
        }
        #endregion

        /***************************************************************************************/
        /***************************************************************************************/
        /***************************************************************************************/
    }

}