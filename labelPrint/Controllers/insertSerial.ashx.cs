using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de insertSerial
    /// </summary>
    public class insertSerial : IHttpHandler
    {
        siixsem_sys_lblPrint_dbEntities m_db = new siixsem_sys_lblPrint_dbEntities();

        public void ProcessRequest(HttpContext context)
        {
            //m_db.insertSerial()
            context.Response.ContentType = "texto/normal";
            context.Response.Write("Hola a todos");
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