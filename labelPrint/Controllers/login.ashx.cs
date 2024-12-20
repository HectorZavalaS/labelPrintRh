using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace labelPrint.Controllers
{
    /// <summary>
    /// Summary description for login
    /// </summary>
    public class login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string json = "{";
            try
            {

                string userN = context.Request.Form["userN"];
                string userP = GetSHA1(context.Request.Form["userP"]);
                siixsem_sys_lblPrint_dbEntities lblDB = new siixsem_sys_lblPrint_dbEntities();
                siixsem_main_dbEntities db = new siixsem_main_dbEntities();
                NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
                //string pass = MD5Encrypt(model.Password);
                validate_user_Result result;// = (ObjectResult)null;
                int res = 0;
                json = "{";

                result = db.validate_user(userN, userP).First();

                res = result.RESULT;
                if (res > 0)
                {
                    HttpCookie cookie = new HttpCookie("_idU");
                    cookie.Value = Convert.ToString(result.se_id_user);
                    cookie.Secure = true;
                    context.Response.Cookies.Add(cookie);

                    cookie = new HttpCookie("_cd");
                    cookie.Value = Convert.ToString(result.code);
                    cookie.Secure = true;
                    context.Response.Cookies.Add(cookie);

                    cookie = new HttpCookie("_lvl");
                    cookie.Value = Convert.ToString(result.se_level);
                    cookie.Secure = true;
                    context.Response.Cookies.Add(cookie);

                    cookie = new HttpCookie("_nm");
                    cookie.Value = Convert.ToString(result.se_name);
                    cookie.Secure = true;
                    context.Response.Cookies.Add(cookie);

                    lblDB.insertEventLog("LOGIN", result.se_id_user, "Inicio de sesion");
                    logger.Info("Sesión iniciada: " + result.se_name);

                    FormsAuthentication.SetAuthCookie(result.se_name, true);
                    json += "\"result\" : \"true\",";
                    json += "\"messageSuccess\" : \"Bienvenido.\"";
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