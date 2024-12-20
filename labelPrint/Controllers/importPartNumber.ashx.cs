using labelPrint.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for importPartNumber
    /// </summary>
    public class importPartNumber : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string json = "{";
            Utils m_utils = new Utils();
            String info = "";
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFile file = files[i];
                        string fname = context.Server.MapPath("~/cargas/" + file.FileName);
                        file.SaveAs(fname);
                        if (m_utils.updatePartNumber(fname, ref info))
                        {
                            json += "\"result\" : \"true\",\n";
                            json += "\"messageSuccess\" : \"Se actualizaron los números de parte correctamente.\"\n";
                        }
                        else
                        {
                            json += "\"result\" : \"false\",\n";
                            json += "\"messageError\":\"" + info + "\"";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                json += "\"result\" : \"false\",\n";
                json += "\"messageError\":\"" + ex.Message + "\"";
            }
            json += "}";

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
            context.Response.End();
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