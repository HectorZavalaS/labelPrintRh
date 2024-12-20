using labelPrint.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace labelPrint.Controllers
{
    public class editZPLModelsController : Controller
    {
        // GET: editZPLModels
        public ActionResult Index()
        {
            siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();
            //Logger logger = LogManager.GetLogger("ZPLLOG");

            //logger.Info("Testeando el log...");
            var zpls = db.getZPLModels();
            return View(zpls.ToList());
        }
    }
}