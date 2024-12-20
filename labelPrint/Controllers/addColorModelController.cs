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
    public class addColorModelController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: addColorModel
        public ActionResult Index()
        {
            var siixsem_colorsb_td = db.siixsem_colorsb_td.Include(s => s.siixsem_lblTagColor_t).Include(s => s.siixsem_models_t).Include(s => s.siixsem_colorb_t);
            return View(siixsem_colorsb_td.ToList());
        }

        // GET: addColorModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_colorsb_td siixsem_colorsb_td = db.siixsem_colorsb_td.Find(id);
            if (siixsem_colorsb_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_colorsb_td);
        }

        // GET: addColorModel/Create
        public ActionResult Create()
        {
            ViewBag.se_id_lblTag = new SelectList(db.siixsem_lblTagColor_t, "se_id_lblTag", "se_lblTag");
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            ViewBag.se_id_colorb = new SelectList(db.siixsem_colorb_t, "se_id_cb", "se_description");
            return View();
        }

        // POST: addColorModel/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id,se_id_colorb,se_id_model,se_id_lblTag")] siixsem_colorsb_td siixsem_colorsb_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_colorsb_td.Add(siixsem_colorsb_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_lblTag = new SelectList(db.siixsem_lblTagColor_t, "se_id_lblTag", "se_lblTag", siixsem_colorsb_td.se_id_lblTag);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_colorsb_td.se_id_model);
            ViewBag.se_id_colorb = new SelectList(db.siixsem_colorb_t, "se_id_cb", "se_description", siixsem_colorsb_td.se_id_colorb);
            return View(siixsem_colorsb_td);
        }

        // GET: addColorModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_colorsb_td siixsem_colorsb_td = db.siixsem_colorsb_td.Find(id);
            if (siixsem_colorsb_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_lblTag = new SelectList(db.siixsem_lblTagColor_t, "se_id_lblTag", "se_lblTag", siixsem_colorsb_td.se_id_lblTag);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_colorsb_td.se_id_model);
            ViewBag.se_id_colorb = new SelectList(db.siixsem_colorb_t, "se_id_cb", "se_description", siixsem_colorsb_td.se_id_colorb);
            return View(siixsem_colorsb_td);
        }

        // POST: addColorModel/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id,se_id_colorb,se_id_model,se_id_lblTag")] siixsem_colorsb_td siixsem_colorsb_td)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_colorsb_td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_lblTag = new SelectList(db.siixsem_lblTagColor_t, "se_id_lblTag", "se_lblTag", siixsem_colorsb_td.se_id_lblTag);
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_colorsb_td.se_id_model);
            ViewBag.se_id_colorb = new SelectList(db.siixsem_colorb_t, "se_id_cb", "se_description", siixsem_colorsb_td.se_id_colorb);
            return View(siixsem_colorsb_td);
        }

        // GET: addColorModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_colorsb_td siixsem_colorsb_td = db.siixsem_colorsb_td.Find(id);
            if (siixsem_colorsb_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_colorsb_td);
        }

        // POST: addColorModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_colorsb_td siixsem_colorsb_td = db.siixsem_colorsb_td.Find(id);
            db.siixsem_colorsb_td.Remove(siixsem_colorsb_td);
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
