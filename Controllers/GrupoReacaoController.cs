using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControleDeCasos.Models;

namespace ControleDeCasos.Controllers
{
    public class GrupoReacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GrupoReacao
        public ActionResult Index()
        {
            return View(db.GrupoReacaos.ToList());
        }

        // GET: GrupoReacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoReacao grupoReacao = db.GrupoReacaos.Find(id);
            if (grupoReacao == null)
            {
                return HttpNotFound();
            }
            return View(grupoReacao);
        }

        // GET: GrupoReacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GrupoReacao/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GrupoReacaoId,Nome")] GrupoReacao grupoReacao)
        {
            if (ModelState.IsValid)
            {
                db.GrupoReacaos.Add(grupoReacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupoReacao);
        }

        // GET: GrupoReacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoReacao grupoReacao = db.GrupoReacaos.Find(id);
            if (grupoReacao == null)
            {
                return HttpNotFound();
            }
            return View(grupoReacao);
        }

        // POST: GrupoReacao/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GrupoReacaoId,Nome")] GrupoReacao grupoReacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoReacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupoReacao);
        }

        // GET: GrupoReacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoReacao grupoReacao = db.GrupoReacaos.Find(id);
            if (grupoReacao == null)
            {
                return HttpNotFound();
            }
            return View(grupoReacao);
        }

        // POST: GrupoReacao/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupoReacao grupoReacao = db.GrupoReacaos.Find(id);
            db.GrupoReacaos.Remove(grupoReacao);
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
