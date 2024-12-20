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
    public class se_usersController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_users
        public ActionResult Index()
        {
            var siixsem_users_t = db.siixsem_users_t.Include(s => s.siixsem_user_role_t);
            return View(siixsem_users_t.ToList());
        }

        // GET: se_users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_users_t siixsem_users_t = db.siixsem_users_t.Find(id);
            if (siixsem_users_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_users_t);
        }

        // GET: se_users/Create
        public ActionResult Create()
        {
            ViewBag.se_id_rol = new SelectList(db.siixsem_user_role_t, "se_id_rol", "se_code");
            return View();
        }

        // POST: se_users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_user,se_name,se_pass,se_id_rol")] siixsem_users_t siixsem_users_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_users_t.Add(siixsem_users_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_rol = new SelectList(db.siixsem_user_role_t, "se_id_rol", "se_code", siixsem_users_t.se_id_rol);
            return View(siixsem_users_t);
        }

        // GET: se_users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_users_t siixsem_users_t = db.siixsem_users_t.Find(id);
            if (siixsem_users_t == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_rol = new SelectList(db.siixsem_user_role_t, "se_id_rol", "se_code", siixsem_users_t.se_id_rol);
            return View(siixsem_users_t);
        }

        // POST: se_users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_user,se_name,se_pass,se_id_rol")] siixsem_users_t siixsem_users_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_users_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.se_id_rol = new SelectList(db.siixsem_user_role_t, "se_id_rol", "se_code", siixsem_users_t.se_id_rol);
            return View(siixsem_users_t);
        }

        // GET: se_users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_users_t siixsem_users_t = db.siixsem_users_t.Find(id);
            if (siixsem_users_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_users_t);
        }

        // POST: se_users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_users_t siixsem_users_t = db.siixsem_users_t.Find(id);
            db.siixsem_users_t.Remove(siixsem_users_t);
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
