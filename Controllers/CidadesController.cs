﻿using System;
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
    public class CidadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cidades
        public ActionResult Index()
        {
            var cidades = db.Cidades.Include(c => c.Estado);
            return View(cidades.ToList());
        }

        // GET: Cidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome");
            return View();
        }

        // POST: Cidades/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CidadeId,EstadoId,Nome")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                db.Cidades.Add(cidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // POST: Cidades/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CidadeId,EstadoId,Nome")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cidade cidade = db.Cidades.Find(id);
            db.Cidades.Remove(cidade);
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
