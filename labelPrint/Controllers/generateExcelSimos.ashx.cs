using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for generateExcelSimos
    /// </summary>
    public class generateExcelSimos : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();
        Utils m_utils = new Utils();
        excel m_excel = new excel();
        public void ProcessRequest(HttpContext context)
        {
            string json = "{";
            DataTable dt = new DataTable();
            try
            {
                string djNo = context.Request.Form["dj_no"];

                //m_excel.FileUpload("");

                int isEspecial = m_db.getModelByDJ(djNo).First().se_is_special;

                var dataExcel = (ObjectResult<generateExcelSimos_Result>)null;
                var dataExcelLed2 = (ObjectResult<generateExcelSimos_Led2_Result>)null;
                if (isEspecial == 0)
                {
                    dataExcel = m_db.generateExcelSimos(djNo);
                    dt = m_utils.ToDataTable<generateExcelSimos_Result>(dataExcel.ToList());
                }
                else
                {
                    dataExcelLed2 = m_db.generateExcelSimos_Led2(djNo);
                    dt = m_utils.ToDataTable<generateExcelSimos_Led2_Result>(dataExcelLed2.ToList());
                }

                m_excel.write_fileOLE(dt, djNo + ".xlsx", context.Server.MapPath("~/DJ_LINK"));

                json += "\"result\" : \"true\",";
                json += "\"messageSuccess\":\"" + "Se genero el archivo excel de manera exitosa. " + "\"";
            }
            catch(Exception ex)
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