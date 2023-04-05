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
    [Authorize]
    public class CasosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Casos
        public ActionResult Index()
        {
            string emailUsuario = HttpContext.User.Identity.Name;
            var casoes = db.Casoes.Where(c => c.EmailUsuario == emailUsuario).Include(c => c.Estado).Include(c => c.Pais);
            return View(casoes.ToList());
        }

        // GET: Casos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casoes.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais = db.Pais.Find(caso.PaisId).Nome;
            ViewBag.Estado = db.Estadoes.Find(caso.EstadoId).Nome;

            ViewBag.ReacoesSelecionadas = GetReacoes((int)id);

            return View(caso);
        }

        private IList<SelectListItem> GetReacoes(int casoId)
        {

            List<Reacao> reacoes = db.Reacaos.Where(r => r.CasoId == casoId).Include(r => r.TipoReacao).ToList();

            List<SelectListItem> listaSelect = new List<SelectListItem>();

            foreach (Reacao reacao in reacoes)
            {
                SelectListItem item = new SelectListItem { Text = reacao.TipoReacao.Nome, Value = reacao.ReacaoId.ToString() };
                listaSelect.Add(item);
            }

            return listaSelect;

        }

        // GET: Casos/Create
        public ActionResult Create()
        {
            int estadoSelecionado = 0;

            List<Cidade> cidades = db.Cidades.Where(c => c.EstadoId == estadoSelecionado).ToList();

            ViewBag.PrimeiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.SegundaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.TerceiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuartaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuintaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");

            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome");
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome");
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome");

            //Comorbidade //Comorbity

            IList<KeyValuePair<int, String>> lista = new List<KeyValuePair<int, String>>();
            KeyValuePair<int, String> item = new KeyValuePair<int, string>(0, "Sim");
            lista.Add(item);
            item = new KeyValuePair<int, string>(1, "Não");
            lista.Add(item);
            item = new KeyValuePair<int, string>(2, "Não Sabe");
            lista.Add(item);

            ViewBag.Comorbidade = new SelectList(lista, "Key", "Value");

            //Diagnóstico Anterior //Previous Diagnosis

            IList<KeyValuePair<int, String>> lista0 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Sim");
            lista0.Add(item);
            item = new KeyValuePair<int, string>(1, "Não");
            lista0.Add(item);
            item = new KeyValuePair<int, string>(2, "Não Sabe");
            lista0.Add(item);

            ViewBag.DiagnosticoAnterior = new SelectList(lista0, "Key", "Value");

            //Condição Especial /Special Condition

            IList<KeyValuePair<int, String>> lista2 = new List<KeyValuePair<int, String>>();
            KeyValuePair<int, String> item2 = new KeyValuePair<int, string>(0, "Nenhuma");
            lista2.Add(item2);
            item2 = new KeyValuePair<int, string>(1, "Gestante");
            lista2.Add(item2);
            item2 = new KeyValuePair<int, string>(2, "Lactante");
            lista2.Add(item2);

            ViewBag.CondicaoEspecial = new SelectList(lista2, "Key", "Value");

            //Estado Atual //Current State

            IList<KeyValuePair<int, String>> lista3 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Com sequelas leves");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(1, "Com sequelas moderadas");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(2, "Com sequelas significativas ou graves");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(3, "Em tratamento / em resolução");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(4, "Óbito");
            lista3.Add(item);

            ViewBag.EstadoAtual = new SelectList(lista3, "Key", "Value");

            //Sexo //Gender

            IList<KeyValuePair<int, String>> lista4 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Masculino");
            lista4.Add(item);
            item = new KeyValuePair<int, string>(1, "Feminino");
            lista4.Add(item);
            ViewBag.Sexo = new SelectList(lista4, "Key", "Value");


            return View();
        }

        // POST: Casos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CasoId,Controle,IniciaisPaciente,DataNascimento,Sexo,PaisId,EstadoId,CidadeId,ComorbidadeId,DiagnosticoAnterior,CondicaoEspecial,EstadoAtual, PrimeiraDoseLaboratorioId, PrimeiraDoseData, PrimeiraDoseLote, PrimeiraDoseErroAplicacao, SegundaDoseLaboratorioId, SegundaDoseData, SegundaDoseLote, SegundaDoseErroAplicacao, TerceiraDoseLaboratorioId, TerceiraDoseData, TerceiraDoseLote, TerceiraDoseErroAplicacao, QuartaaDoseLaboratorioId, QuartaDoseData, QuartaDoseLote, QuartaDoseErroAplicacao, QuintaDoseLaboratorioId, QuintaDoseData, QuintaDoseLote, QuintaDoseErroAplicacao")] Caso caso)
        {
            if (ModelState.IsValid)
            {
                caso.EmailUsuario = HttpContext.User.Identity.Name;
                Caso casoSalvo = db.Casoes.Add(caso);
                caso.DataCriacao = DateTime.UtcNow.AddHours(-3);
                db.SaveChanges();
                return RedirectToAction("CreateSeveral", "Reacao", new { casoId = caso.CasoId });
            }

            ViewBag.PrimeiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.SegundaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.TerceiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuartaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuintaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", caso.EstadoId);
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", caso.PaisId);
            return View(caso);
        }

        // GET: Casos/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casoes.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }

            ViewBag.PrimeiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.SegundaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.TerceiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuartaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuintaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");

            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", caso.EstadoId);
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", caso.PaisId);

            //Comorbidade //Comorbity

            IList<KeyValuePair<int, String>> lista = new List<KeyValuePair<int, String>>();
            KeyValuePair<int, String> item = new KeyValuePair<int, string>(0, "Sim");
            lista.Add(item);
            item = new KeyValuePair<int, string>(1, "Não");
            lista.Add(item);
            item = new KeyValuePair<int, string>(2, "Não Sabe");
            lista.Add(item);

            ViewBag.Comorbidade = new SelectList(lista, "Key", "Value", caso.Comorbidade);

            //Diagnóstico Anterior //Previous Diagnosis

            IList<KeyValuePair<int, String>> lista0 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Sim");
            lista0.Add(item);
            item = new KeyValuePair<int, string>(1, "Não");
            lista0.Add(item);
            item = new KeyValuePair<int, string>(2, "Não Sabe");
            lista0.Add(item);

            ViewBag.DiagnosticoAnterior = new SelectList(lista0, "Key", "Value", caso.DiagnosticoAnterior);

            //Condição Especial //Special Condition

            IList<KeyValuePair<int, String>> lista2 = new List<KeyValuePair<int, String>>();
            KeyValuePair<int, String> item2 = new KeyValuePair<int, string>(0, "Nenhuma");
            lista2.Add(item2);
            item2 = new KeyValuePair<int, string>(1, "Gestante");
            lista2.Add(item2);
            item2 = new KeyValuePair<int, string>(2, "Lactante");
            lista2.Add(item2);

            ViewBag.CondicaoEspecial = new SelectList(lista2, "Key", "Value", caso.CondicaoEspecial);

            //Estado Atual //Current State

            IList<KeyValuePair<int, String>> lista3 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Com sequelas leves");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(1, "Com sequelas moderadas");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(2, "Com sequelas significativas ou graves");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(3, "Em tratamento / em resolução");
            lista3.Add(item);
            item = new KeyValuePair<int, string>(4, "Óbito");
            lista3.Add(item);

            ViewBag.EstadoAtual = new SelectList(lista3, "Key", "Value", caso.EstadoAtual);

            //Sexo //Gender

            IList<KeyValuePair<int, String>> lista4 = new List<KeyValuePair<int, String>>();
            item = new KeyValuePair<int, string>(0, "Masculino");
            lista4.Add(item);
            item = new KeyValuePair<int, string>(1, "Feminino");
            lista4.Add(item);

            ViewBag.Sexo = new SelectList(lista4, "Key", "Value", caso.Sexo);

            return View(caso);
        }

        // POST: Casos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CasoId,EmailUsuario,Controle,IniciaisPaciente,DataNascimento,DataInicio,Hospitalizado,Severidade,Sexo,PaisId,EstadoId,CidadeId,ComorbidadeId,DiagnosticoAnterior,CondicaoEspecial, EstadoAtual, PrimeiraDoseLaboratorioId, PrimeiraDoseData, PrimeiraDoseLote, PrimeiraDoseErroAplicacao, SegundaDoseLaboratorioId, SegundaDoseData, SegundaDoseLote, SegundaDoseErroAplicacao, TerceiraDoseLaboratorioId, TerceiraDoseData, TerceiraDoseLote, TerceiraDoseErroAplicacao, QuartaDoseLaboratorioId, QuartaDoseData, QuartaDoseLote, QuartaDoseErroAplicacao, QuintaDoseLaboratorioId, QuintaDoseData, QuintaDoseLote, QuintaDoseErroAplicacao")] Caso caso)
        {
            if (ModelState.IsValid)
            {
                caso.EmailUsuario = HttpContext.User.Identity.Name;
                caso.DataUltimaAtualizacao = DateTime.UtcNow.AddHours(-3);
                db.Entry(caso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CreateSeveral", "Reacao", new { casoId = caso.CasoId });
            }
            ViewBag.PrimeiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.SegundaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.TerceiraDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuartaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.QuintaDoseLaboratorioId = new SelectList(db.Laboratorios, "LaboratorioId", "Nome");
            ViewBag.EstadoId = new SelectList(db.Estadoes, "EstadoId", "Nome", caso.EstadoId);
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome", caso.PaisId);
            return View(caso);
        }

        // GET: Casos/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casoes.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pais = db.Pais.Find(caso.PaisId).Nome;
            ViewBag.Estado = db.Estadoes.Find(caso.EstadoId).Nome;
            return View(caso);
        }

        // POST: Casos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caso caso = db.Casoes.Find(id);
            db.Casoes.Remove(caso);
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
