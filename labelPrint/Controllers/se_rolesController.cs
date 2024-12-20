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
    public class se_rolesController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();

        // GET: se_roles
        public ActionResult Index()
        {
            return View(db.siixsem_user_role_t.ToList());
        }

        // GET: se_roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_user_role_t siixsem_user_role_t = db.siixsem_user_role_t.Find(id);
            if (siixsem_user_role_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_user_role_t);
        }

        // GET: se_roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: se_roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_rol,se_code,se_description,se_descr_permissions,se_level")] siixsem_user_role_t siixsem_user_role_t)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_user_role_t.Add(siixsem_user_role_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siixsem_user_role_t);
        }

        // GET: se_roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_user_role_t siixsem_user_role_t = db.siixsem_user_role_t.Find(id);
            if (siixsem_user_role_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_user_role_t);
        }

        // POST: se_roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_rol,se_code,se_description,se_descr_permissions,se_level")] siixsem_user_role_t siixsem_user_role_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siixsem_user_role_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siixsem_user_role_t);
        }

        // GET: se_roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_user_role_t siixsem_user_role_t = db.siixsem_user_role_t.Find(id);
            if (siixsem_user_role_t == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_user_role_t);
        }

        // POST: se_roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_user_role_t siixsem_user_role_t = db.siixsem_user_role_t.Find(id);
            db.siixsem_user_role_t.Remove(siixsem_user_role_t);
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
