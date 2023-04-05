using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControleDeCasos.Models;
using Newtonsoft.Json;

namespace ControleDeCasos.Controllers
{
    public class EstadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estados
        public ActionResult Index()
        {
            var estadoes = db.Estadoes.Include(e => e.Pais);
            return View(estadoes.ToList());
        }

        //GET: Estados por id do país
        public JsonResult GetALL(int? paisId)
        {
            var estadoes = db.Estadoes.Where(e => e.PaisId == paisId );
            List<Estado> lest = estadoes.ToList<Estado>();
            return Json(lest, JsonRequestBehavior.AllowGet);
        }

        // GET: Estados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estadoes.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // GET: Estados/Create
        public ActionResult Create()
        {
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome");
            return View();
        }

        // POST: Estados/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadoId,PaisId,Nome")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Estadoes.Add(estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // GET: Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estadoes.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // POST: Estados/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadoId,PaisId,Nome")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", estado.PaisId);
            return View(estado);
        }

        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estadoes.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estados/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estado estado = db.Estadoes.Find(id);
            db.Estadoes.Remove(estado);
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
