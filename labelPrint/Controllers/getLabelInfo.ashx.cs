using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getLabelInfo
    /// </summary>
    public class getLabelInfo : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String tbl = "";
            string tmpPoka = "";
            string tmpTempl = "";
            StringCollection pokayokes = new StringCollection();
            try
            {
                String dj_no = context.Request.Form["dj_no"];
                var AllLabels = m_db.getInfoPrintLabelbyDJV2(dj_no);
                if (AllLabels.Count() > 0)
                {
                    AllLabels = m_db.getInfoPrintLabelbyDJV2(dj_no);
                    foreach (getInfoPrintLabelbyDJV2_Result row in AllLabels)
                    {
                        json += "\"model\":\"" + row.DESC_MODEL + "\",";
                        tmpPoka = row.POKAYOKE;
                        tbl += "<tr>";
                        tbl += "<td>" + row.DESC_SIDE + "</td>";
                        tbl += "<td>" + row.DESC_FLX + " / " + row.TAG_FLX + "</td>";
                        tbl += "<td>" + row.DESC_COL + " / " + row.TAG_COL + "</td>";
                        tbl += "<td>" + row.DESC_VOL + " / " + row.TAG_VOL + "</td>";
                        tbl += "<td>" + row.DESC_FLX1 + " / " + row.TAG_FLX1 + "</td>";
                        tbl += "<td>" + row.DESC_COL1 + " / " + row.TAG_COL1 + "</td>";
                        tbl += "<td>" + row.DESC_VOL1 + " / " + row.TAG_VOL1 + "</td>";
                        tbl += "<td>" + row.CANT + "</td>";
                        tbl += "<td>" + row.DATE_PRINT + "</td>";
                        tbl += "<td>" + row.DATE_DJ.Value.ToShortDateString() + "</td>";
                        tbl += "<td><input value='" + row.TEMPLATE_LABEL + "' type='text' class='inpTbl' readonly style='width:200px;'/></td>";
                        tbl += "<td><input value='" + tmpPoka + "' type='text' class='inpTbl' readonly /></td>";
                        tbl += "</tr>";
                        if (pokayokes.IndexOf(tmpPoka) < 0)
                            pokayokes.Add(tmpPoka);

                    }
                    tmpPoka = "";
                    foreach (String tmp in pokayokes)
                    {
                        tmpPoka += tmp + "|";
                    }
                    tmpPoka = tmpPoka.Substring(0, tmpPoka.Length - 1);
                }
                else
                {
                    tmpPoka = "";
                    json += "\"model\":\"No hay etiqueta impresa.\",";
                    tbl += "<tr>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "<td></td>";
                    tbl += "</tr>";
                }
                json += "\"result\":\"true\",";
                json += "\"pokayoke\":\"" + tmpPoka + "\",";
                json += "\"tbl\":\"" + tbl + "\"";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}