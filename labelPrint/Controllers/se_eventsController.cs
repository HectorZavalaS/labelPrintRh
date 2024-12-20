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
    public class se_eventsController : Controller
    {
        private siixsem_sys_lblPrint_dbEntities db = new siixsem_sys_lblPrint_dbEntities();

        // GET: se_events
        public ActionResult Index()
        {
            return View(db.siixseem_events_t.ToList());
        }

        // GET: se_events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_events_t siixseem_events_t = db.siixseem_events_t.Find(id);
            if (siixseem_events_t == null)
            {
                return HttpNotFound();
            }
            return View(siixseem_events_t);
        }

        // GET: se_events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_event,se_description")] siixseem_events_t siixseem_events_t)
        {
            if (ModelState.IsValid)
            {
                db.siixseem_events_t.Add(siixseem_events_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixseem_events_t);
        }

        // GET: se_events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_events_t siixseem_events_t = db.siixseem_events_t.Find(id);
            if (siixseem_events_t == null)
            {
                return HttpNotFound();
            }
            return View(siixseem_events_t);
        }

        // POST: se_events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_event,se_description")] siixseem_events_t siixseem_events_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixseem_events_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixseem_events_t);
        }

        // GET: se_events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixseem_events_t siixseem_events_t = db.siixseem_events_t.Find(id);
            if (siixseem_events_t == null)
            {
                return HttpNotFound();
            }
            return View(siixseem_events_t);
        }

        // POST: se_events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixseem_events_t siixseem_events_t = db.siixseem_events_t.Find(id);
            db.siixseem_events_t.Remove(siixseem_events_t);
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
