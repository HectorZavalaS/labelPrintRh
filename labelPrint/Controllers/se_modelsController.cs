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
    public class se_modelsController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_models
        public ActionResult Index()
        {
            var siixsem_models_t = db.siixsem_models_t.Include(s => s.siixsem_families_t);
            return View(siixsem_models_t.ToList());
        }

        // GET: se_models/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_models_t);
        }

        // GET: se_models/Create
        public ActionResult Create()
        {
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description");
            return View();
        }

        // POST: se_models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_model,se_description,se_id_family,se_is_special")] siixsem_models_t siixsem_models_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_models_t.Add(siixsem_models_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // GET: se_models/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // POST: se_models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_model,se_description,se_id_family,se_is_special")] siixsem_models_t siixsem_models_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_models_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // GET: se_models/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_models_t);
        }

        // POST: se_models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            db.siixsem_models_t.Remove(siixsem_models_t);
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
