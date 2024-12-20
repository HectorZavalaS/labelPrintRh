using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for validateUsrPrint
    /// </summary>
    public class validateUsrPrint : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string json = "{";
            string userN = context.Request.Form["userN"];
            string userP = GetSHA1(context.Request.Form["userP"]);
            siixsem_sys_lblPrint_dbEntities lblDB = new siixsem_sys_lblPrint_dbEntities();
            siixsem_main_dbEntities db = new siixsem_main_dbEntities();
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            validate_user_Result result;
            try
            {
                int res = 0;
                json = "{";
                result = db.validate_user(userN, userP).First();
                res = result.RESULT;
                if (res > 0)
                {
                    if (result.code.Equals("PROD_ADMIN"))
                    {
                        json += "\"result\" : \"true\",";
                        json += "\"messageSuccess\" : \"Reimpresión Autorizada. Da click en el boton de cerrar para imprimir.\"";
                    }
                    else
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\" : \"Usuario no autorizado.\"";
                    }
                }
                else
                {
                    if (res == -1)
                    {
                        json += "\"result\" : \"false\",";
                        json += "\"messageError\" : \"El usuario o password es incorrecto.\"";
                    }
                }
            }
            catch (Exception ex)
            {
                json += "\"result\" : \"false\",";
                json += "\"messageError\" : \"" + ex.Message + "\"";
            }
            json += "}";
            context.Response.ContentType = "texto/normal";
            context.Response.Write(json);
        }
        public static string GetSHA1(String texto)
        {
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
            Byte[] hash = sha1.ComputeHash(textOriginal);
            StringBuilder cadena = new StringBuilder();
            foreach (byte i in hash)
            {
                cadena.AppendFormat("{0:x2}", i);
            }
            return cadena.ToString();
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