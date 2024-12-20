using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de insertSideModel
    /// </summary>
    public class insertSideModel : IHttpHandler
    {

        private siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                String descr = context.Request.Form["lblTag"];
                String oracle = context.Request.Form["oracle"];
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                int idSide = Convert.ToInt32(context.Request.Form["idSide"]);


                if (m_dbM.insertSideModel(idModel,idSide,oracle.Replace("-","").Replace("/","").Replace("_","").Replace(".","").Replace(" ",""),descr,oracle).First().RESULT != -1)
                {
                    json += "\"result\":\"true\",";
                    json += "\"Message\":\"Se asigno el lado al modelo.\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"Message\":\"No se pudo asignar el lado al modelo.\"";
                }

            }
            catch (Exception ex)
            {
                json += "\"result\":\"false\",";
                json += "\"Message\":\"" + ex.Message + "\"";
            }
            json += "}";
            context.Response.ContentType = "texto/normal";
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