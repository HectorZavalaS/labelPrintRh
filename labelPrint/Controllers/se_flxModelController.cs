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
    public class se_flxModelController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_flxModel
        public ActionResult Index()
        {
            var siixsem_flxsModel_td = db.siixsem_flxsModel_td.Include(s => s.siixsem_flxb_t).Include(s => s.siixsem_models_t);
            return View(siixsem_flxsModel_td.ToList());
        }

        // GET: se_flxModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_td);
        }

        // GET: se_flxModel/Create
        public ActionResult Create()
        {
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description");
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            return View();
        }

        // POST: se_flxModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_td siixsem_flxsModel_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_flxsModel_td.Add(siixsem_flxsModel_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // GET: se_flxModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // POST: se_flxModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_detail,se_id_model,se_id_flx")] siixsem_flxsModel_td siixsem_flxsModel_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_flxsModel_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_flx = new SelectList(db.siixsem_flxb_t, "se_id_fb", "se_description", siixsem_flxsModel_td.se_id_flx);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_flxsModel_td.se_id_model);
            return View(siixsem_flxsModel_td);
        }

        // GET: se_flxModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            if (siixsem_flxsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_flxsModel_td);
        }

        // POST: se_flxModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_flxsModel_td siixsem_flxsModel_td = db.siixsem_flxsModel_td.Find(id);
            db.siixsem_flxsModel_td.Remove(siixsem_flxsModel_td);
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
