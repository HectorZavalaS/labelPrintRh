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
    public class se_labelsController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_labels
        public ActionResult Index()
        {
            var siixsem_lblTemplates_t = db.siixsem_lblTemplates_t.Include(s => s.siixsem_datamatrix_t).Include(s => s.siixsem_lblZPL_t);
            return View(siixsem_lblTemplates_t.ToList());
        }

        // GET: se_labels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblTemplates_t siixsem_lblTemplates_t = db.siixsem_lblTemplates_t.Find(id);
            if (siixsem_lblTemplates_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblTemplates_t);
        }

        // GET: se_labels/Create
        public ActionResult Create()
        {
            ViewBag.se_id_dm = new SelectList(db.siixsem_datamatrix_t, "se_id_dm", "se_description");
            ViewBag.se_id_zpl = new SelectList(db.siixsem_lblZPL_t, "se_id_zpl", "se_name");
            return View();
        }

        // POST: se_labels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_label,se_id_zpl,se_id_dm")] siixsem_lblTemplates_t siixsem_lblTemplates_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_lblTemplates_t.Add(siixsem_lblTemplates_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_dm = new SelectList(db.siixsem_datamatrix_t, "se_id_dm", "se_id_dm", siixsem_lblTemplates_t.se_id_dm);
            ViewBag.se_id_zpl = new SelectList(db.siixsem_lblZPL_t, "se_id_zpl", "se_str_zpl_one", siixsem_lblTemplates_t.se_id_zpl);
            return View(siixsem_lblTemplates_t);
        }

        // GET: se_labels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblTemplates_t siixsem_lblTemplates_t = db.siixsem_lblTemplates_t.Find(id);
            if (siixsem_lblTemplates_t == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_dm = new SelectList(db.siixsem_datamatrix_t, "se_id_dm", "se_id_dm", siixsem_lblTemplates_t.se_id_dm);
            ViewBag.se_id_zpl = new SelectList(db.siixsem_lblZPL_t, "se_id_zpl", "se_str_zpl_one", siixsem_lblTemplates_t.se_id_zpl);
            return View(siixsem_lblTemplates_t);
        }

        // POST: se_labels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_label,se_id_zpl,se_id_dm")] siixsem_lblTemplates_t siixsem_lblTemplates_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_lblTemplates_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_dm = new SelectList(db.siixsem_datamatrix_t, "se_id_dm", "se_id_dm", siixsem_lblTemplates_t.se_id_dm);
            ViewBag.se_id_zpl = new SelectList(db.siixsem_lblZPL_t, "se_id_zpl", "se_str_zpl_one", siixsem_lblTemplates_t.se_id_zpl);
            return View(siixsem_lblTemplates_t);
        }

        // GET: se_labels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblTemplates_t siixsem_lblTemplates_t = db.siixsem_lblTemplates_t.Find(id);
            if (siixsem_lblTemplates_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblTemplates_t);
        }

        // POST: se_labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_lblTemplates_t siixsem_lblTemplates_t = db.siixsem_lblTemplates_t.Find(id);
            db.siixsem_lblTemplates_t.Remove(siixsem_lblTemplates_t);
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
