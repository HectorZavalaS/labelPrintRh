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
    public class se_voltagebController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_voltageb
        public ActionResult Index()
        {
            return View(db.siixsem_voltageb_t.ToList());
        }

        // GET: se_voltageb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_voltageb_t siixsem_voltageb_t = db.siixsem_voltageb_t.Find(id);
            if (siixsem_voltageb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_voltageb_t);
        }

        // GET: se_voltageb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_voltageb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_volt,se_description,se_volt_lbl")] siixsem_voltageb_t siixsem_voltageb_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_voltageb_t.Add(siixsem_voltageb_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_voltageb_t);
        }

        // GET: se_voltageb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_voltageb_t siixsem_voltageb_t = db.siixsem_voltageb_t.Find(id);
            if (siixsem_voltageb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_voltageb_t);
        }

        // POST: se_voltageb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_volt,se_description,se_volt_lbl")] siixsem_voltageb_t siixsem_voltageb_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_voltageb_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_voltageb_t);
        }

        // GET: se_voltageb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_voltageb_t siixsem_voltageb_t = db.siixsem_voltageb_t.Find(id);
            if (siixsem_voltageb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_voltageb_t);
        }

        // POST: se_voltageb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_voltageb_t siixsem_voltageb_t = db.siixsem_voltageb_t.Find(id);
            db.siixsem_voltageb_t.Remove(siixsem_voltageb_t);
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
