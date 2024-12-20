using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using labelPrint.Class;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for genRegLbls
    /// </summary>
    public class genRegLbls : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            CLblWEBService m_webservice;
            try
            {
                int nSides = Convert.ToInt32(context.Request.Form["nSides"]);
                String templateLblCodeL = context.Request.Form["templateLblCodeL"];
                String templateLblCodeL1 = context.Request.Form["templateLblCodeL1"];
                String templateLblCodeL2 = context.Request.Form["templateLblCodeL2"];
                String templateLblCodeR = context.Request.Form["templateLblCodeR"];
                String templateLblCodeR1 = context.Request.Form["templateLblCodeR1"];
                String templateLblCodeR2 = context.Request.Form["templateLblCodeR2"];


                String tmpL = "", tmpR = "";
                String rowsTop = "";
                String rowsBot = "";
                String tblRows = "";
                //String tblRowsBot = "";
                Double num_lbls = Convert.ToInt32(context.Request.Form["num_lbls"]);
                num_lbls = num_lbls + 100;

                //tmp = templateLblCode.Replace("XXXXX", String.Format("{0:00000}", num_lbls));

                for (int i = 0; i < 3; i++)
                {

                    tmpL = templateLblCodeL.Replace("XXXXX", String.Format("{0:00000}", i + 1));
                    tmpR = templateLblCodeR.Replace("XXXXX", String.Format("{0:00000}", i + 1));

                    byte[] zplL = getBytesString(tmpL);
                    byte[] zplR = getBytesString(tmpR);


                    rowsTop += "<div class='row' style='margin:0px;'>";
                    tblRows += "<tr>";
                    m_webservice = new CLblWEBService(zplL, 0.393701, 0.393701);
                    if (m_webservice.send())
                    {
                        if (m_webservice.receive())
                        {
                            rowsTop += getHtmlLbl(m_webservice.FileName);
                            m_webservice = new CLblWEBService(zplR, 0.393701, 0.393701);
                            if (m_webservice.send())
                            {
                                if (m_webservice.receive())
                                {
                                    rowsTop += getHtmlLbl(m_webservice.FileName);
                                    tblRows += getRowTbl(tmpL, tmpR);
                                }
                            }

                        }
                    }
                    tblRows += "</tr>";
                    rowsTop += "</div>";
                }
                tblRows += "<tr>";
                tblRows += getRowTbl("---------", "---------");
                tblRows += "</tr>";
                tblRows += "<tr>";
                tblRows += getRowTbl("---------", "---------");
                tblRows += "</tr>";
                double tmp = num_lbls - 2;
                for (double j = tmp; j <= num_lbls; j++)
                {

                    tmpL = templateLblCodeL.Replace("XXXXX", String.Format("{0:00000}", j));
                    tmpR = templateLblCodeR.Replace("XXXXX", String.Format("{0:00000}", j));

                    byte[] zplL = getBytesString(tmpL);
                    byte[] zplR = getBytesString(tmpR);

                    tblRows += "<tr>";
                    rowsBot += "<div class='row' style='margin:0px;'>";
                    m_webservice = new CLblWEBService(zplL, 0.393701, 0.393701);
                    if (m_webservice.send())
                    {
                        if (m_webservice.receive())
                        {
                            Thread.Sleep(500);
                            rowsBot += getHtmlLbl(m_webservice.FileName);
                            m_webservice = new CLblWEBService(zplR, 0.393701, 0.393701);
                            if (m_webservice.send())
                            {
                                if (m_webservice.receive())
                                {
                                    rowsBot += getHtmlLbl(m_webservice.FileName);
                                    tblRows += getRowTbl(tmpL, tmpR);
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
        private String getHtmlLbl(String fileName)
        {
            return "<div class='column45'>" +
                    "     <div class='lblBox10x10'><center><img class='prevLbl' src='/Labels/" + fileName + "' style='width: 100%;'></center></div>" +
                    "</div>";
        }
        private String getRowTbl(String codel, String coder)
        {
            return "<td>" + codel + "</td>" +
                   "<td>" + coder + "</td>";
        }
        private byte[] getBytesString(String code)
        {
            return Encoding.UTF8.GetBytes("^XA" +
                                        "^MMT" +
                                        "^PW80" +
                                        "^LL0080" +
                                        "^LS0" +
                                        "^BY48,48^FT15,77^BXN,3,200,0,0,1,~" +
                                        "^FH\\^FD" + code + "^FS" +
                                        "^FT8,22^A0N,11,12^FB70,2,0,,^FH\\^FD" + code + "^FS" +
                                        "^PQ1,0,1,Y" +
                                        "^XZ");
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