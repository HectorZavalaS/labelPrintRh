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
    public class fluxesController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: fluxes
        public ActionResult Index()
        {
            return View(db.siixsem_flxb_t.ToList());
        }

        // GET: fluxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxb_t siixsem_flxb_t = db.siixsem_flxb_t.Find(id);
            if (siixsem_flxb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxb_t);
        }

        // GET: fluxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fluxes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_fb,se_description,se_flx_lbl")] siixsem_flxb_t siixsem_flxb_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_flxb_t.Add(siixsem_flxb_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_flxb_t);
        }

        // GET: fluxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxb_t siixsem_flxb_t = db.siixsem_flxb_t.Find(id);
            if (siixsem_flxb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxb_t);
        }

        // POST: fluxes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_fb,se_description,se_flx_lbl")] siixsem_flxb_t siixsem_flxb_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_flxb_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_flxb_t);
        }

        // GET: fluxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxb_t siixsem_flxb_t = db.siixsem_flxb_t.Find(id);
            if (siixsem_flxb_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxb_t);
        }

        // POST: fluxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_flxb_t siixsem_flxb_t = db.siixsem_flxb_t.Find(id);
            db.siixsem_flxb_t.Remove(siixsem_flxb_t);
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
