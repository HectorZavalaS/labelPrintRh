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
    public class se_sidesController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_sides
        public ActionResult Index()
        {
            return View(db.siixsem_sides_t.ToList());
        }

        // GET: se_sides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sides_t siixsem_sides_t = db.siixsem_sides_t.Find(id);
            if (siixsem_sides_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_sides_t);
        }

        // GET: se_sides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_sides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_side,se_description")] siixsem_sides_t siixsem_sides_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_sides_t.Add(siixsem_sides_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_sides_t);
        }

        // GET: se_sides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sides_t siixsem_sides_t = db.siixsem_sides_t.Find(id);
            if (siixsem_sides_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_sides_t);
        }

        // POST: se_sides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_side,se_description")] siixsem_sides_t siixsem_sides_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_sides_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_sides_t);
        }

        // GET: se_sides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sides_t siixsem_sides_t = db.siixsem_sides_t.Find(id);
            if (siixsem_sides_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_sides_t);
        }

        // POST: se_sides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_sides_t siixsem_sides_t = db.siixsem_sides_t.Find(id);
            db.siixsem_sides_t.Remove(siixsem_sides_t);
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
