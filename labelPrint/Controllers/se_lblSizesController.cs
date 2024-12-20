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
    public class se_lblSizesController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_lblSizes
        public ActionResult Index()
        {
            return View(db.siixsem_lblSizes_t.ToList());
        }

        // GET: se_lblSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblSizes_t siixsem_lblSizes_t = db.siixsem_lblSizes_t.Find(id);
            if (siixsem_lblSizes_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblSizes_t);
        }

        // GET: se_lblSizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_lblSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_size,se_description,se_height,se_width")] siixsem_lblSizes_t siixsem_lblSizes_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_lblSizes_t.Add(siixsem_lblSizes_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_lblSizes_t);
        }

        // GET: se_lblSizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblSizes_t siixsem_lblSizes_t = db.siixsem_lblSizes_t.Find(id);
            if (siixsem_lblSizes_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblSizes_t);
        }

        // POST: se_lblSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_size,se_description,se_height,se_width")] siixsem_lblSizes_t siixsem_lblSizes_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_lblSizes_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_lblSizes_t);
        }

        // GET: se_lblSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblSizes_t siixsem_lblSizes_t = db.siixsem_lblSizes_t.Find(id);
            if (siixsem_lblSizes_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblSizes_t);
        }

        // POST: se_lblSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_lblSizes_t siixsem_lblSizes_t = db.siixsem_lblSizes_t.Find(id);
            db.siixsem_lblSizes_t.Remove(siixsem_lblSizes_t);
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
