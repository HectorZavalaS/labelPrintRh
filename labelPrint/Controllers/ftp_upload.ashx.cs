using labelPrint.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Descripción breve de ftp_upload
    /// </summary>
    public class ftp_upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            ftp m_ftp = new ftp("ftp://192.168.6.52","ftpUser","ftpUser");

            m_ftp.upload("r_150bldm_35520      d210250737.xml", "C:\\ftpFilesTmp\\" + "r_150bldm_35520      d210250737.xml");

            context.Response.ContentType = "text/plain";
            context.Response.Write("");
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