using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for getCants
    /// </summary>
    public class getCants : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String json = "{";
            String html = "";

            try
            {
                int range = Convert.ToInt32(context.Request.Form["range"]);
                Double valIni = range == 1 ? 10 : range == 2 ? 3000 : range == 3 ? 6000 : 9000;
                Double valFin = valIni + 3000;

                for (Double i = valIni; i <= valFin; i += 10)
                    html += "<option value=" + i.ToString() + ">" + i.ToString() + "</option>";

                json += "\"html\":\"" + html + "\",";
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