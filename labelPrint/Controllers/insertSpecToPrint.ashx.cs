using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for insertSpecToPrint
    /// </summary>
    public class insertSpecToPrint : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["idModel"]);
                int id_flx = Convert.ToInt32(context.Request.Form["idFlx"]);
                int id_color = Convert.ToInt32(context.Request.Form["idColor"]);
                int id_volt = Convert.ToInt32(context.Request.Form["idVolt"]);
                int id_user = Convert.ToInt32(context.Request.Form["idUser"]);
                int cantTotal = Convert.ToInt32(context.Request.Form["cantTot"]);
                string djNo = context.Request.Form["djNo"];
                string assy_name = context.Request.Form["aName"];
                int initFolio = Convert.ToInt32(context.Request.Form["initFolio"]);
                int lastfolio = Convert.ToInt32(context.Request.Form["lastfolio"]);
                String dateDJ = context.Request.Form["dateDj"].ToString().Trim();
                int is_rem = Convert.ToInt32(context.Request.Form["isRem"].ToString().Trim());

                //lastfolio
                DateTime fechaDJ = DateTime.Parse(dateDJ);

                ////insertSpec2_Result m_Result = m_db.insertSpec2(id_model, id_flx, id_color, id_volt, id_user, cantTotal, initFolio, lastfolio, lastfolio, djNo,assy_name,fechaDJ,is_rem).First();

                //if(m_Result.RESULT > 0)
                //    json += "\"result\":\"true\",";
                //else
                //    json += "\"result\":\"false\",";

                //json += "\"messagge\":\"" + m_Result.TXTRESULT + "\"";


                //@iniFolio AS INTEGER,
                //@finFolio AS INTEGER,
                //@currFolio AS INTEGER
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