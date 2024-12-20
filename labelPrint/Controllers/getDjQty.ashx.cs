using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de getDjQty
    /// </summary>
    public class getDjQty : IHttpHandler
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
                var djQty = m_db.getDjQty(idModel, djNo.ToString());
                foreach (getDjQty_Result row in djQty)
                {
                    qty = row.QTY.ToString();
                    qtyPcb = row.PCBQTY.ToString();
                    totLbl = row.LBLQTY.ToString();
                    hasPanel = row.hasPanel.ToString();
                }
                json += "\"result\":\"true\",";
                json += "\"qty\":\"" + qty + "\",";
                json += "\"qtyPCB\":\"" + qtyPcb + "\",";
                json += "\"totLbl\":\"" + totLbl + "\",";
                json += "\"hasPanel\":\"" + hasPanel + "\",";
                json += "\"html\":\"" + html + "\"";
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