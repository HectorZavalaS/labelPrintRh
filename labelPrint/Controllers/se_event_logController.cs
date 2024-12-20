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
    public class se_event_logController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_event_log
        public ActionResult Index()
        {
            var siixseem_event_log = db.siixseem_event_log.Include(s => s.siixseem_events_t).Include(s => s.siixsem_lblModel_spec_td);
            return View(siixseem_event_log.ToList());
        }

        // GET: se_event_log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_event_log siixseem_event_log = db.siixseem_event_log.Find(id);
            if (siixseem_event_log == null)
            {
                return HttpNotFound();
            }
            return View(siixseem_event_log);
        }

        // GET: se_event_log/Create
        public ActionResult Create()
        {
            ViewBag.se_id_event = new SelectList(db.siixseem_events_t, "se_id_event", "se_description");
            ViewBag.se_id_spec = new SelectList(db.siixsem_lblModel_spec_td, "se_id_spec", "se_id_spec");
            return View();
        }

        // POST: se_event_log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_log,se_id_event,se_id_user,se_description,se_date_event,se_id_spec")] siixseem_event_log siixseem_event_log)
        {
            if (ModelState.IsValid)
            {
                db.siixseem_event_log.Add(siixseem_event_log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_event = new SelectList(db.siixseem_events_t, "se_id_event", "se_description", siixseem_event_log.se_id_event);
            ViewBag.se_id_spec = new SelectList(db.siixsem_lblModel_spec_td, "se_id_spec", "se_id_spec", siixseem_event_log.se_id_spec);
            return View(siixseem_event_log);
        }

        // GET: se_event_log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_event_log siixseem_event_log = db.siixseem_event_log.Find(id);
            if (siixseem_event_log == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_event = new SelectList(db.siixseem_events_t, "se_id_event", "se_description", siixseem_event_log.se_id_event);
            ViewBag.se_id_spec = new SelectList(db.siixsem_lblModel_spec_td, "se_id_spec", "se_id_spec", siixseem_event_log.se_id_spec);
            return View(siixseem_event_log);
        }

        // POST: se_event_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_log,se_id_event,se_id_user,se_description,se_date_event,se_id_spec")] siixseem_event_log siixseem_event_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixseem_event_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_event = new SelectList(db.siixseem_events_t, "se_id_event", "se_description", siixseem_event_log.se_id_event);
            ViewBag.se_id_spec = new SelectList(db.siixsem_lblModel_spec_td, "se_id_spec", "se_id_spec", siixseem_event_log.se_id_spec);
            return View(siixseem_event_log);
        }

        // GET: se_event_log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_event_log siixseem_event_log = db.siixseem_event_log.Find(id);
            if (siixseem_event_log == null)
            {
                return HttpNotFound();
            }
            return View(siixseem_event_log);
        }

        // POST: se_event_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixseem_event_log siixseem_event_log = db.siixseem_event_log.Find(id);
            db.siixseem_event_log.Remove(siixseem_event_log);
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
