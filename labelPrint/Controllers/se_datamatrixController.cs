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
    public class se_datamatrixController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_datamatrix
        public ActionResult Index()
        {
            var siixsem_datamatrix_t = db.siixsem_datamatrix_t.Include(s => s.siixsem_densities_t).Include(s => s.siixsem_lblSizes_t);
            return View(siixsem_datamatrix_t.ToList());
        }

        // GET: se_datamatrix/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_datamatrix_t siixsem_datamatrix_t = db.siixsem_datamatrix_t.Find(id);
            if (siixsem_datamatrix_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_datamatrix_t);
        }

        // GET: se_datamatrix/Create
        public ActionResult Create()
        {
            ViewBag.se_id_density = new SelectList(db.siixsem_densities_t, "se_id_density", "se_id_density");
            ViewBag.se_id_size = new SelectList(db.siixsem_lblSizes_t, "se_id_size", "se_description");
            return View();
        }

        // POST: se_datamatrix/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_dm,se_id_density,se_id_size,se_description")] siixsem_datamatrix_t siixsem_datamatrix_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_datamatrix_t.Add(siixsem_datamatrix_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_density = new SelectList(db.siixsem_densities_t, "se_id_density", "se_id_density", siixsem_datamatrix_t.se_id_density);
            ViewBag.se_id_size = new SelectList(db.siixsem_lblSizes_t, "se_id_size", "se_description", siixsem_datamatrix_t.se_id_size);
            return View(siixsem_datamatrix_t);
        }

        // GET: se_datamatrix/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_datamatrix_t siixsem_datamatrix_t = db.siixsem_datamatrix_t.Find(id);
            if (siixsem_datamatrix_t == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_density = new SelectList(db.siixsem_densities_t, "se_id_density", "se_id_density", siixsem_datamatrix_t.se_id_density);
            ViewBag.se_id_size = new SelectList(db.siixsem_lblSizes_t, "se_id_size", "se_description", siixsem_datamatrix_t.se_id_size);
            return View(siixsem_datamatrix_t);
        }

        // POST: se_datamatrix/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_dm,se_id_density,se_id_size,se_description")] siixsem_datamatrix_t siixsem_datamatrix_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_datamatrix_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_density = new SelectList(db.siixsem_densities_t, "se_id_density", "se_id_density", siixsem_datamatrix_t.se_id_density);
            ViewBag.se_id_size = new SelectList(db.siixsem_lblSizes_t, "se_id_size", "se_description", siixsem_datamatrix_t.se_id_size);
            return View(siixsem_datamatrix_t);
        }

        // GET: se_datamatrix/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_datamatrix_t siixsem_datamatrix_t = db.siixsem_datamatrix_t.Find(id);
            if (siixsem_datamatrix_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_datamatrix_t);
        }

        // POST: se_datamatrix/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_datamatrix_t siixsem_datamatrix_t = db.siixsem_datamatrix_t.Find(id);
            db.siixsem_datamatrix_t.Remove(siixsem_datamatrix_t);
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
