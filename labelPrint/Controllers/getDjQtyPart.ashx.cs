using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de getDjQtyPart
    /// </summary>
    public class getDjQtyPart : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";
            String qty = "";
            String qtyPcb = "";
            String totLbl = "";
            String hasPanel = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                int djNo = Convert.ToInt32(context.Request.Form["djNo"]);
                int pcbDjQty = Convert.ToInt32(context.Request.Form["pcbDjQty"]);

                var djQty = m_db.getDjQtyPart(idModel, djNo.ToString(),pcbDjQty);
                foreach (getDjQtyPart_Result row in djQty)
                {
                    qty = row.QTY.ToString();
                    qtyPcb = row.PCBQTY.ToString();
                    totLbl = row.LBLQTY.ToString();
                    hasPanel = row.hasPanel.ToString();
                }
                if (qtyPcb != "-1")
                {
                    json += "\"result\":\"true\",";
                    json += "\"qty\":\"" + qty + "\",";
                    json += "\"qtyPCB\":\"" + qtyPcb + "\",";
                    json += "\"totLbl\":\"" + totLbl + "\",";
                    json += "\"hasPanel\":\"" + hasPanel + "\",";
                    json += "\"html\":\"" + html + "\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"MessageError\":\"" + "La cantidad introducida no debe exceder la cantidad marcada por la DJ: " + qty + "\"";
                }
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message.Replace("\"","") + "\"";
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