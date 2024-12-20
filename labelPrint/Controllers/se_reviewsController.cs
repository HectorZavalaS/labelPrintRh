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
    public class se_reviewsController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_reviews
        public ActionResult Index()
        {
            return View(db.siixsem_reviews_t.ToList());
        }

        // GET: se_reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_reviews_t siixsem_reviews_t = db.siixsem_reviews_t.Find(id);
            if (siixsem_reviews_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_reviews_t);
        }

        // GET: se_reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_rev,se_description")] siixsem_reviews_t siixsem_reviews_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_reviews_t.Add(siixsem_reviews_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_reviews_t);
        }

        // GET: se_reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_reviews_t siixsem_reviews_t = db.siixsem_reviews_t.Find(id);
            if (siixsem_reviews_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_reviews_t);
        }

        // POST: se_reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_rev,se_description")] siixsem_reviews_t siixsem_reviews_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_reviews_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_reviews_t);
        }

        // GET: se_reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_reviews_t siixsem_reviews_t = db.siixsem_reviews_t.Find(id);
            if (siixsem_reviews_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_reviews_t);
        }

        // POST: se_reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_reviews_t siixsem_reviews_t = db.siixsem_reviews_t.Find(id);
            db.siixsem_reviews_t.Remove(siixsem_reviews_t);
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
