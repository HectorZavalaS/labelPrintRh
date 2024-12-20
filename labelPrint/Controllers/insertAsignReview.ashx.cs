using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for insertAsignReview
    /// </summary>
    public class insertAsignReview : IHttpHandler
    {

        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String res = "";

            try
            {
                int id_model = Convert.ToInt32(context.Request.Form["id_model"]);
                int id_review = Convert.ToInt32(context.Request.Form["id_review"]);

                int result = m_db.insertAsignRevModel(id_model, id_review).First().RESULT;

                if (result < 0)
                {
                    switch (result)
                    {
                        case -1:
                            res = "La revisión no existe.";
                            break;
                        case -2:
                            res = "El modelo no existe.";
                            break;
                        case -3:
                            res = "La revisión ya fue asignada a este modelo.";
                            break;
                        default:

                            break;
                    }
                    json += "\"html\":\"" + res + "\",";
                    json += "\"result\":\"false\"";
                }
                else
                {
                    res = "Se asigno con existo la etiqueta.";
                    json += "\"html\":\"" + res + "\",";
                    json += "\"result\":\"true\"";
                }
            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"html\":\"" + ex.Message + "\"";
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