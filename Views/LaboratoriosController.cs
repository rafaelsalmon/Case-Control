using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControleDeCasos.Models;

namespace ControleDeCasos.Views
{
    public class LaboratoriosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Laboratorios
        public ActionResult Index()
        {
            return View(db.Laboratorios.ToList());
        }

        // GET: Laboratorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laboratorios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LaboratorioId,Nome")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Laboratorios.Add(laboratorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LaboratorioId,Nome")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            db.Laboratorios.Remove(laboratorio);
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
