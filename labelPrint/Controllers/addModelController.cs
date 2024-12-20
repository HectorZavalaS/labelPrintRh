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
    public class addModelController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: addModel
        public ActionResult Index()
        {
            var siixsem_models_t = db.siixsem_models_t.Include(s => s.siixsem_families_t);
            return View(siixsem_models_t.ToList());
        }

        // GET: addModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_models_t);
        }

        // GET: addModel/Create
        public ActionResult Create()
        {
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description");
            return View();
        }

        // POST: addModel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_model,se_description,se_id_family,se_is_special,se_valid,se_serial_continuos,se_two_led,se_dfc_ifc,se_four_sides,se_cover_frame,se_num_pcb,se_laser_mark,se_has_panelLbl,se_is_ldm")] siixsem_models_t siixsem_models_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_models_t.Add(siixsem_models_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // GET: addModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // POST: addModel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_model,se_description,se_id_family,se_is_special,se_valid,se_serial_continuos,se_two_led,se_dfc_ifc,se_four_sides,se_cover_frame,se_num_pcb,se_laser_mark,se_has_panelLbl,se_is_ldm")] siixsem_models_t siixsem_models_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_models_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_family = new SelectList(db.siixsem_families_t, "se_id_family", "se_description", siixsem_models_t.se_id_family);
            return View(siixsem_models_t);
        }

        // GET: addModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            if (siixsem_models_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_models_t);
        }

        // POST: addModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_models_t siixsem_models_t = db.siixsem_models_t.Find(id);
            db.siixsem_models_t.Remove(siixsem_models_t);
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
