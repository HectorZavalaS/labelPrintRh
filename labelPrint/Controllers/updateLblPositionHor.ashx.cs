using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de updateLblPositionHor
    /// </summary>
    public class updateLblPositionHor : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                int idUser = Convert.ToInt32(context.Request.Form["idUser"]);
                int value = Convert.ToInt32(context.Request.Form["value"]);

                int result = m_db.updateLblPositionHor(idModel,idUser, value).First().RESULT;
                if(result==1)
                    json += "\"result\":\"true\"";
                else
                {
                    json += "\"result\":\"false\"";
                    json += "\"MessageError\":\"No se encontro el modelo.\"";
                }
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"MessageError\":\"" + ex.Message.Replace("\"", "'") + "\"";
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