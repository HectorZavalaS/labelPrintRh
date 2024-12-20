using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace labelPrint.Controllers
{
    public class addFluxController : Controller
    {
        // GET: addFlux
        public ActionResult Index()
        {
            return View();
        }

        // GET: addFlux/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: addFlux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: addFlux/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: addFlux/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: addFlux/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: addFlux/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: addFlux/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
