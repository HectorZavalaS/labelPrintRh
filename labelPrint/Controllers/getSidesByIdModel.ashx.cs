using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getSidesByIdModel
    /// </summary>
    public class getSidesByIdModel : IHttpHandler
    {
        siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();
        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String htmlL = "";
            String htmlR = "";
            try
            {
                int idModel = Convert.ToInt32(context.Request.Form["idModel"]);
                var AllSides = m_db.getSidesByIdModel(idModel).ToList();
                int numSides = Convert.ToInt32(m_db.getNumSidesByIdModel(idModel).First().numSides);

                if (numSides == 2)
                {
                    getSidesByIdModel_Result rowL = AllSides.ElementAt(0);
                    htmlL += "<option value='" + rowL.se_id_side + "'>" + rowL.se_description + "</option>";
                    getSidesByIdModel_Result rowR = (getSidesByIdModel_Result)AllSides.ElementAt(1);
                    htmlR += "<option value='" + rowR.se_id_side + "'>" + rowR.se_description + "</option>";
                    json += "\"htmlL\":\"" + htmlL + "\",";
                    json += "\"htmlR\":\"" + htmlR + "\",";
                    json += "\"nSides\":\"" + "2" + "\",";
                }
                else if (numSides == 1)
                {
                    getSidesByIdModel_Result rowL = AllSides.First();
                    htmlL += "<option value='" + rowL.se_id_side + "'>" + rowL.se_description + "</option>";
                    json += "\"htmlL\":\"" + htmlL + "\",";
                    json += "\"htmlR\":\"" + "NA" + "\",";
                    json += "\"nSides\":\"" + "1" + "\",";

                }
                //foreach (getSidesByIdModel_Result row in AllSides)
                //{
                //    htmlL += "<option value='" + row.se_id_side + "'>" + row.se_description + "</option>";
                //}
                json += "\"result\":\"true\"";

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