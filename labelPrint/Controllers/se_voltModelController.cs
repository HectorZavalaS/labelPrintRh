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
    public class se_voltModelController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_voltModel
        public ActionResult Index()
        {
            var siixsem_VoltsModel_td = db.siixsem_VoltsModel_td.Include(s => s.siixsem_models_t).Include(s => s.siixsem_voltageb_t);
            return View(siixsem_VoltsModel_td.ToList());
        }

        // GET: se_voltModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_VoltsModel_td siixsem_VoltsModel_td = db.siixsem_VoltsModel_td.Find(id);
            if (siixsem_VoltsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_VoltsModel_td);
        }

        // GET: se_voltModel/Create
        public ActionResult Create()
        {
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            ViewBag.se_id_volt = new SelectList(db.siixsem_voltageb_t, "se_id_volt", "se_description");
            return View();
        }

        // POST: se_voltModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_detail,se_id_model,se_id_volt")] siixsem_VoltsModel_td siixsem_VoltsModel_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_VoltsModel_td.Add(siixsem_VoltsModel_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_VoltsModel_td.se_id_model);
            ViewBag.se_id_volt = new SelectList(db.siixsem_voltageb_t, "se_id_volt", "se_description", siixsem_VoltsModel_td.se_id_volt);
            return View(siixsem_VoltsModel_td);
        }

        // GET: se_voltModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_VoltsModel_td siixsem_VoltsModel_td = db.siixsem_VoltsModel_td.Find(id);
            if (siixsem_VoltsModel_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_VoltsModel_td.se_id_model);
            ViewBag.se_id_volt = new SelectList(db.siixsem_voltageb_t, "se_id_volt", "se_description", siixsem_VoltsModel_td.se_id_volt);
            return View(siixsem_VoltsModel_td);
        }

        // POST: se_voltModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_detail,se_id_model,se_id_volt")] siixsem_VoltsModel_td siixsem_VoltsModel_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_VoltsModel_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_VoltsModel_td.se_id_model);
            ViewBag.se_id_volt = new SelectList(db.siixsem_voltageb_t, "se_id_volt", "se_description", siixsem_VoltsModel_td.se_id_volt);
            return View(siixsem_VoltsModel_td);
        }

        // GET: se_voltModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_VoltsModel_td siixsem_VoltsModel_td = db.siixsem_VoltsModel_td.Find(id);
            if (siixsem_VoltsModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_VoltsModel_td);
        }

        // POST: se_voltModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_VoltsModel_td siixsem_VoltsModel_td = db.siixsem_VoltsModel_td.Find(id);
            db.siixsem_VoltsModel_td.Remove(siixsem_VoltsModel_td);
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
