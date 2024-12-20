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
    public class se_projectsController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_projects
        public ActionResult Index()
        {
            return View(db.siixsem_families_t.ToList());
        }

        // GET: se_projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_families_t siixsem_families_t = db.siixsem_families_t.Find(id);
            if (siixsem_families_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_families_t);
        }

        // GET: se_projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_family,se_description,se_car_model")] siixsem_families_t siixsem_families_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_families_t.Add(siixsem_families_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_families_t);
        }

        // GET: se_projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_families_t siixsem_families_t = db.siixsem_families_t.Find(id);
            if (siixsem_families_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_families_t);
        }

        // POST: se_projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_family,se_description,se_car_model")] siixsem_families_t siixsem_families_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_families_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_families_t);
        }

        // GET: se_projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_families_t siixsem_families_t = db.siixsem_families_t.Find(id);
            if (siixsem_families_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_families_t);
        }

        // POST: se_projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_families_t siixsem_families_t = db.siixsem_families_t.Find(id);
            db.siixsem_families_t.Remove(siixsem_families_t);
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
