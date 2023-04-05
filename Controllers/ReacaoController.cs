using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class ReacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reacao
        public ActionResult Index()
        {
            string emailUsuario = HttpContext.User.Identity.Name;
            var reacaos = db.Reacaos.Include(r => r.Caso).Include(r => r.TipoReacao);
            return View(reacaos.ToList());
        }

        // GET: Reacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<Reacao> reacoes = db.Reacaos.Include(r => r.Caso).Include(r => r.TipoReacao).ToList<Reacao>();
            Reacao reacao = reacoes.Where(r => r.ReacaoId == id).First();
            if (reacao == null)
            {
                return HttpNotFound();
            }
            return View(reacao);
        }

        public ActionResult CreateSeveral(int? casoId)
        {
            ViewBag.TipoReacaoId = new SelectList(db.TipoReacaos, "TipoReacaoId", "Nome");

            ViewBag.Reacoes = null;

            Caso caso = null;
            if (!(casoId is null))
            {
                caso = db.Casoes.Find(casoId);
                ViewBag.IniciaisPaciente = caso.IniciaisPaciente;
                ViewBag.Controle = caso.Controle;
                ViewBag.Caso = caso;
                ViewBag.Reacoes = GetReacoes((int) casoId);

                //Severidade //Severity

                IList<KeyValuePair<int, String>> listaSeveridade = new List<KeyValuePair<int, String>>();
                KeyValuePair<int, String> item = new KeyValuePair<int, string>(0, "Leve");
                listaSeveridade.Add(item);
                item = new KeyValuePair<int, string>(1, "Moderada");
                listaSeveridade.Add(item);
                item = new KeyValuePair<int, string>(2, "Grave");
                listaSeveridade.Add(item);

                SelectList lista = new SelectList(listaSeveridade, "Key", "Value", caso.Severidade.ToString());

                ViewBag.Severidade = (IEnumerable<SelectListItem>) lista;

                ViewBag.Hospitalizado = caso.Hospitalizado;
                ViewBag.DataInicio = caso.DataInicio;

            }

            ViewBag.CasoId = new SelectList(db.Casoes, "CasoId", "Controle");

            ViewBag.TiposReacao = GetTiposReacao(ViewBag.Reacoes);

            Session["TiposReacao"] = ViewBag.TiposReacao;

            Session["CasoId"] = casoId;

            if (!(ViewBag.Caso is null)) {
                return View(caso);
            }
            return View();
        }

        [HttpPost, ActionName("CreateSeveral")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeveral(Caso caso)
        {
            int c = 0;
            String valorCampo = null;
            int? casoId = null;
            Caso casoOriginal = null;

            IList<SelectListItem> reacoes = null;
            IList<SelectListItem> tiposReacoes;

            if(!(caso is null))  
            {
                casoId = caso.CasoId; 
                Session["CasoId"] = null;
                
                casoOriginal = db.Casoes.Find(casoId);

                reacoes = GetReacoes((int)casoId);

                casoOriginal.DataInicio = caso.DataInicio;
                casoOriginal.Hospitalizado = caso.Hospitalizado;
                casoOriginal.Severidade = caso.Severidade;

            }

            if (!(Session["TiposReacao"] is null)) {
                IList<SelectListItem> tiposReacao = (IList<SelectListItem>) Session["TiposReacao"];
                c = tiposReacao.Count();
                Session["TiposReacao"] = null;
            }

            db.SaveChanges();

            Reacao reacao = null;

            IList<SelectListItem> sliSelecionadosTelaEdicao = new List<SelectListItem>();

            //Gerar lista da tela
            //Para cada item da tela selecionado
            
            //Generate list from user interface
            //For each item selected on the interface

            for (int i = 0; i < c; i++)
            {
                valorCampo = Request.Form["TipoReacaoSelect" + i];

                if (!(valorCampo is null))
                {
                    //Se já está na lista do bd, não faço nada
                    // If it's already in the database list, dones not do anything
                    if (!(reacoes is null))
                    {

                        reacao = new Reacao();
                        reacao.TipoReacaoId = int.Parse(valorCampo);
                        reacao.CasoId = (int)casoId;
                        reacao.EmailUsuario = HttpContext.User.Identity.Name;
                        bool jaCadastrado = Existe(new SelectListItem { Value = valorCampo }, (List<SelectListItem>)reacoes);
                        if (!jaCadastrado)
                        {
                            db.Reacaos.Add(reacao);
                            db.SaveChanges();

                        }
                        SelectListItem sli = new SelectListItem();
                        sli.Value = reacao.TipoReacaoId.ToString();
                        sliSelecionadosTelaEdicao.Add(sli);
                    }
                }
                
            }

            //Para cada item da lista de reações salva no BD
            // For each item in the reactions list, salve into the database
            if (!(reacoes is null))
            {
                foreach (SelectListItem re in reacoes)
                {
                    //Se está na lista da tela, não faço nada
                    //If it is on the user interface list, dosn't do anything
                    bool manteveSelecao = Existe(re, (List<SelectListItem>)sliSelecionadosTelaEdicao);
                    if (!manteveSelecao) //Se não está, excluo // If it's not, delete
                    {
                        int intValor = int.Parse(re.Value);
                        Reacao reac = db.Reacaos.Where(r => r.TipoReacaoId == intValor).Where(r => r.CasoId == casoId).First();
                        db.Reacaos.Remove(reac);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index", "Casos");
        }

        private IList<SelectListItem> GetTiposReacao(IList<SelectListItem> reacoes)
        {
            
            List<TipoReacao> tiposReacoes = db.TipoReacaos.Include(tr => tr.GrupoReacao).OrderBy(tr => tr.GrupoReacao.Nome).ToList();

            List<SelectListItem> listaSelect = new List<SelectListItem>();

            foreach (TipoReacao tipoReacao in tiposReacoes) {
                SelectListItem item = new SelectListItem { Text = tipoReacao.Nome, Value = tipoReacao.TipoReacaoId.ToString() };
                if(Existe(item, (List <SelectListItem>) reacoes)){
                    item.Selected = true;
                }
                item.Group = new SelectListGroup();
                item.Group.Name = tipoReacao.GrupoReacao.Nome;
                listaSelect.Add(item);
            }

            return listaSelect;

        }

        private IList<SelectListItem> GetReacoes(int casoId)
        {

            List<Reacao> reacoes = db.Reacaos.Include(r => r.TipoReacao).Where(r => r.CasoId == casoId).ToList();

            List<SelectListItem> listaSelect = new List<SelectListItem>();

            foreach (Reacao reacao in reacoes)
            {
                if(!(reacao.TipoReacao is null)) { 
                    SelectListItem item = new SelectListItem { Text = reacao.TipoReacao.Nome, Value = reacao.TipoReacaoId.ToString() };
                    listaSelect.Add(item);
                }
            }

            return listaSelect;

        }

        private bool Existe(SelectListItem item, List<SelectListItem> lista)
        {
            if (!(lista is null)) { 
                foreach (SelectListItem it in lista) {
                    if (it.Value == item.Value) return true;
                }
            }
            return false;
        }


        // GET: Reacao/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reacao reacao = db.Reacaos.Find(id);
            if (reacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CasoId = new SelectList(db.Casoes, "CasoId", "Controle", reacao.CasoId);
            ViewBag.TipoReacaoId = new SelectList(db.TipoReacaos, "TipoReacaoId", "Nome", reacao.TipoReacaoId);
 
            //Severidade //Severity

            IList<KeyValuePair<int, String>> lista = new List<KeyValuePair<int, String>>();

            KeyValuePair<int, String> item = new KeyValuePair<int, string>(0, "Leve");
            lista.Add(item);
            item = new KeyValuePair<int, string>(1, "Moderada");
            lista.Add(item);
            item = new KeyValuePair<int, string>(2, "Grave");
            lista.Add(item);

            return View(reacao);
        }

        // POST: Reacao/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReacaoId,DataInicio,Hospitalizado,Severidade,TipoReacaoId,CasoId")] Reacao reacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CasoId = new SelectList(db.Casoes, "CasoId", "Controle", reacao.CasoId);
            ViewBag.TipoReacaoId = new SelectList(db.TipoReacaos, "TipoReacaoId", "Nome", reacao.TipoReacaoId);
            return View(reacao);
        }

        // GET: Reacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IList<Reacao> reacoes = db.Reacaos.Include(r => r.Caso).Include(r => r.TipoReacao).ToList<Reacao>();
            Reacao reacao = reacoes.Where(r => r.ReacaoId == id).First();
            if (reacao == null)
            {
                return HttpNotFound();
            }
            return View(reacao);
        }

        // POST: Reacao/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reacao reacao = db.Reacaos.Find(id);
            db.Reacaos.Remove(reacao);
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
