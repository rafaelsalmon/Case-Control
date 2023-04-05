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
    public class TipoReacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoReacao
        public ActionResult Index()
        {
            return View(db.TipoReacaos.Include(tr=>tr.GrupoReacao).ToList());
        }

        // GET: TipoReacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReacao tipoReacao = db.TipoReacaos.Include(tr => tr.GrupoReacao).Where(tr => tr.TipoReacaoId == id).First();
            if(!(tipoReacao.GrupoReacao is null)) ViewBag.GrupoReacao = tipoReacao.GrupoReacao.Nome;
            if (tipoReacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoReacao);
        }

        // GET: TipoReacao/Create
        public ActionResult Create()
        {
            ViewBag.GrupoReacaoId = new SelectList(db.GrupoReacaos, "GrupoReacaoId", "Nome");
            return View();
        }

        // POST: TipoReacao/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoReacaoId,Nome, GrupoReacaoId, CodigoAnotacao")] TipoReacao tipoReacao)
        {
            if (ModelState.IsValid)
            {
                db.TipoReacaos.Add(tipoReacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoReacao);
        }

        // GET: TipoReacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReacao tipoReacao = db.TipoReacaos.Find(id);
            if (tipoReacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoReacaoId = new SelectList(db.GrupoReacaos, "GrupoReacaoId", "Nome", tipoReacao.GrupoReacaoId);
            return View(tipoReacao);
        }

        // POST: TipoReacao/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoReacaoId,Nome, GrupoReacaoId, CodigoAnotacao")] TipoReacao tipoReacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoReacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoReacao);
        }

        // GET: TipoReacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReacao tipoReacao = db.TipoReacaos.Find(id);
            if (tipoReacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoReacao);
        }

        // POST: TipoReacao/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoReacao tipoReacao = db.TipoReacaos.Find(id);
            db.TipoReacaos.Remove(tipoReacao);
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
