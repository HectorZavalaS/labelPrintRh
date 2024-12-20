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
    public class se_revModelController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_revModel
        public ActionResult Index()
        {
            var siixsem_revModel_td = db.siixsem_revModel_td.Include(s => s.siixsem_reviews_t);
            return View(siixsem_revModel_td.ToList());
        }

        // GET: se_revModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_revModel_td siixsem_revModel_td = db.siixsem_revModel_td.Find(id);
            if (siixsem_revModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_revModel_td);
        }

        // GET: se_revModel/Create
        public ActionResult Create()
        {
            ViewBag.se_id_review = new SelectList(db.siixsem_reviews_t, "se_id_rev", "se_description");
            return View();
        }

        // POST: se_revModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id,se_id_review,se_id_model")] siixsem_revModel_td siixsem_revModel_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_revModel_td.Add(siixsem_revModel_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_review = new SelectList(db.siixsem_reviews_t, "se_id_rev", "se_description", siixsem_revModel_td.se_id_review);
            return View(siixsem_revModel_td);
        }

        // GET: se_revModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_revModel_td siixsem_revModel_td = db.siixsem_revModel_td.Find(id);
            if (siixsem_revModel_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_review = new SelectList(db.siixsem_reviews_t, "se_id_rev", "se_description", siixsem_revModel_td.se_id_review);
            return View(siixsem_revModel_td);
        }

        // POST: se_revModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id,se_id_review,se_id_model")] siixsem_revModel_td siixsem_revModel_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_revModel_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_review = new SelectList(db.siixsem_reviews_t, "se_id_rev", "se_description", siixsem_revModel_td.se_id_review);
            return View(siixsem_revModel_td);
        }

        // GET: se_revModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_revModel_td siixsem_revModel_td = db.siixsem_revModel_td.Find(id);
            if (siixsem_revModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_revModel_td);
        }

        // POST: se_revModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_revModel_td siixsem_revModel_td = db.siixsem_revModel_td.Find(id);
            db.siixsem_revModel_td.Remove(siixsem_revModel_td);
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
