using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de insertModel
    /// </summary>
    public class insertModel : IHttpHandler
    {
        //       @descr AS NVARCHAR(250),
        //@idProject AS INTEGER,
        //@isSpecial AS INTEGER,
        //@isValid AS INTEGER,
        //@isSC AS INTEGER,
        //@has2L AS INTEGER,
        //@isDFC AS INTEGER,
        //@isML AS INTEGER,
        //@hasPanel AS INTEGER,
        //@isLDM AS INTEGER,
        //@numPCB AS INTEGER
        private siixsem_main_dbEntities m_dbM = new siixsem_main_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            try
            {
                String descr = context.Request.Form["descr"];
                int idProject = Convert.ToInt32(context.Request.Form["idProject"]);
                int isSpecial = Convert.ToInt32(context.Request.Form["isSpecial"]);
                int isValid = Convert.ToInt32(context.Request.Form["isValid"]);
                int isSC = Convert.ToInt32(context.Request.Form["isSC"]);
                int has2L = Convert.ToInt32(context.Request.Form["has2L"]);
                int isDFC = Convert.ToInt32(context.Request.Form["isDFC"]);
                int isML = Convert.ToInt32(context.Request.Form["isML"]);
                int hasPanel = Convert.ToInt32(context.Request.Form["hasPanel"]);
                int isLDM = Convert.ToInt32(context.Request.Form["isLDM"]);
                int numPCB = Convert.ToInt32(context.Request.Form["numPCB"]);

                Decimal RESULT = Convert.ToDecimal(m_dbM.insertModel(descr, idProject, isSpecial, isValid, isSC, has2L, isDFC, isML, hasPanel, isLDM, numPCB).First().RESULT);

                if ( RESULT != -1)
                {
                    json += "\"result\":\"true\",";
                    json += "\"idModel\":\"" + RESULT.ToString() + "\",";
                    json += "\"Message\":\"Se agrego el modelo.\"";
                }
                else
                {
                    json += "\"result\":\"false\",";
                    json += "\"Message\":\"No se pudo agegar el modelo.\"";
                }

            }
            catch(Exception ex)
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