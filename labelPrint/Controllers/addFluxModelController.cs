using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using labelPrint.Models;

namespace labelPrint.Controllers
{
    public class addFluxModelController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: addFluxModel
        public ActionResult Index()
        {
            var siixsem_flxsModel_td = db.siixsem_flxsModel_td.Include(s => s.siixsem_flxb_t).Include(s => s.siixsem_models_t);
            return View(siixsem_flxsModel_td.ToList());
        }

        // GET: addFluxModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_td);
        }

        // GET: addFluxModel/Create
        public ActionResult Create()
        {
            var fluxes = db.siixsem_flxb_t.Select(x => new {
                se_id_fb = x.se_id_fb,
                se_description = x.se_description + " - " + x.se_flx_lbl
            });
            ViewBag.se_id_flx = new SelectList(fluxes, "se_id_fb", "se_description");
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            return View();
        }

        // POST: addFluxModel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_td siixsem_flxsModel_td)
        {
            var flux = db.siixsem_flxb_t.Where(x => x.se_id_fb == siixsem_flxsModel_td.se_id_flx).Select(x => new {
                se_id_fb = x.se_id_fb,
                se_description = x.se_description + " - " + x.se_flx_lbl
            });
            if (ModelState.IsValid)
            {
                db.siixsem_flxsModel_td.Add(siixsem_flxsModel_td);
                db.SaveChanges();
                logger.Info("Se inserto el flux " + flux.First().se_description);
                return RedirectToAction("Index");
            }

            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // GET: addFluxModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // POST: addFluxModel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_td siixsem_flxsModel_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_flxsModel_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // GET: addFluxModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_td);
        }

        // POST: addFluxModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            db.siixsem_flxsModel_td.Remove(siixsem_flxsModel_td);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
