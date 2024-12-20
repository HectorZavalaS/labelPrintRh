using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using labelPrint.Models;
using NLog;

namespace labelPrint.Controllers
{
    public class zplModelsController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: zplModels
        public ActionResult Index()
        {
            var siixsem_lblModels_td = db.siixsem_lblModels_td.Include(s => s.siixsem_lblTemplates_t);

            return View(siixsem_lblModels_td.ToList());
        }

        // GET: zplModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblModels_td siixsem_lblModels_td = db.siixsem_lblModels_td.Find(id);
            if (siixsem_lblModels_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblModels_td);
        }

        // GET: zplModels/Create
        public ActionResult Create()
        {
            ViewBag.se_id_label = new SelectList(db.siixsem_lblTemplates_t, "se_id_label", "se_id_label");
            return View();
        }

        // POST: zplModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id,se_id_model,se_id_label")] siixsem_lblModels_td siixsem_lblModels_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_lblModels_td.Add(siixsem_lblModels_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_label = new SelectList(db.siixsem_lblTemplates_t, "se_id_label", "se_id_label", siixsem_lblModels_td.se_id_label);
            return View(siixsem_lblModels_td);
        }

        // GET: zplModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblModels_td siixsem_lblModels_td = db.siixsem_lblModels_td.Find(id);
            if (siixsem_lblModels_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_label = new SelectList(db.siixsem_lblTemplates_t, "se_id_label", "se_id_label", siixsem_lblModels_td.se_id_label);
            return View(siixsem_lblModels_td);
        }

        // POST: zplModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id,se_id_model,se_id_label")] siixsem_lblModels_td siixsem_lblModels_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_lblModels_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_label = new SelectList(db.siixsem_lblTemplates_t, "se_id_label", "se_id_label", siixsem_lblModels_td.se_id_label);
            return View(siixsem_lblModels_td);
        }

        // GET: zplModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblModels_td siixsem_lblModels_td = db.siixsem_lblModels_td.Find(id);
            if (siixsem_lblModels_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblModels_td);
        }

        // POST: zplModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_lblModels_td siixsem_lblModels_td = db.siixsem_lblModels_td.Find(id);
            db.siixsem_lblModels_td.Remove(siixsem_lblModels_td);
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
