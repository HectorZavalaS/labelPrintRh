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
    public class se_densitiesController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_densities
        public ActionResult Index()
        {
            return View(db.siixsem_densities_t.ToList());
        }

        // GET: se_densities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_densities_t siixsem_densities_t = db.siixsem_densities_t.Find(id);
            if (siixsem_densities_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_densities_t);
        }

        // GET: se_densities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_densities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_density,se_density")] siixsem_densities_t siixsem_densities_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_densities_t.Add(siixsem_densities_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_densities_t);
        }

        // GET: se_densities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_densities_t siixsem_densities_t = db.siixsem_densities_t.Find(id);
            if (siixsem_densities_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_densities_t);
        }

        // POST: se_densities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_density,se_density")] siixsem_densities_t siixsem_densities_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_densities_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_densities_t);
        }

        // GET: se_densities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_densities_t siixsem_densities_t = db.siixsem_densities_t.Find(id);
            if (siixsem_densities_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_densities_t);
        }

        // POST: se_densities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_densities_t siixsem_densities_t = db.siixsem_densities_t.Find(id);
            db.siixsem_densities_t.Remove(siixsem_densities_t);
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
