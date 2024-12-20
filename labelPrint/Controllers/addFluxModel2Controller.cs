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
    public class addFluxModel2Controller : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: addFluxModel2
        public ActionResult Index()
        {
            var siixsem_flxsModel_led2_td = db.siixsem_flxsModel_led2_td.Include(s => s.siixsem_flxb_t).Include(s => s.siixsem_models_t);
            return View(siixsem_flxsModel_led2_td.ToList());
        }

        // GET: addFluxModel2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td = db.siixsem_flxsModel_led2_td.Find(id);
            if (siixsem_flxsModel_led2_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_led2_td);
        }

        // GET: addFluxModel2/Create
        public ActionResult Create()
        {
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description");
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            return View();
        }

        // POST: addFluxModel2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_flxsModel_led2_td.Add(siixsem_flxsModel_led2_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_led2_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_led2_td.se_id_model);
            return View(siixsem_flxsModel_led2_td);
        }

        // GET: addFluxModel2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td = db.siixsem_flxsModel_led2_td.Find(id);
            if (siixsem_flxsModel_led2_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_led2_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_led2_td.se_id_model);
            return View(siixsem_flxsModel_led2_td);
        }

        // POST: addFluxModel2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_flxsModel_led2_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_led2_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_led2_td.se_id_model);
            return View(siixsem_flxsModel_led2_td);
        }

        // GET: addFluxModel2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td = db.siixsem_flxsModel_led2_td.Find(id);
            if (siixsem_flxsModel_led2_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_led2_td);
        }

        // POST: addFluxModel2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_flxsModel_led2_td siixsem_flxsModel_led2_td = db.siixsem_flxsModel_led2_td.Find(id);
            db.siixsem_flxsModel_led2_td.Remove(siixsem_flxsModel_led2_td);
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
