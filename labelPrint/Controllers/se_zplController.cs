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
    public class se_zplController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_zpl
        public ActionResult Index()
        {
            return View(db.siixsem_lblZPL_t.ToList());
        }

        // GET: se_zpl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblZPL_t siixsem_lblZPL_t = db.siixsem_lblZPL_t.Find(id);
            if (siixsem_lblZPL_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblZPL_t);
        }

        // GET: se_zpl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_zpl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_zpl,se_str_zpl_one,se_str_zpl_two,se_name,se_str_zpl_preview")] siixsem_lblZPL_t siixsem_lblZPL_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_lblZPL_t.Add(siixsem_lblZPL_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_lblZPL_t);
        }

        // GET: se_zpl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblZPL_t siixsem_lblZPL_t = db.siixsem_lblZPL_t.Find(id);
            if (siixsem_lblZPL_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblZPL_t);
        }

        // POST: se_zpl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_zpl,se_str_zpl_one,se_str_zpl_two,se_name,se_str_zpl_preview")] siixsem_lblZPL_t siixsem_lblZPL_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_lblZPL_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_lblZPL_t);
        }

        // GET: se_zpl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_lblZPL_t siixsem_lblZPL_t = db.siixsem_lblZPL_t.Find(id);
            if (siixsem_lblZPL_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_lblZPL_t);
        }

        // POST: se_zpl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_lblZPL_t siixsem_lblZPL_t = db.siixsem_lblZPL_t.Find(id);
            db.siixsem_lblZPL_t.Remove(siixsem_lblZPL_t);
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
