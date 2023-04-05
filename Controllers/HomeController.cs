using ControleDeCasos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleDeCasos.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Calculates / Generates Statistical Data
        /// </summary>
        public ActionResult About()
        {
            int numeroDeCasos = db.Casoes.Count();
            ViewBag.NumeroDeCasos = numeroDeCasos;

            int totalReacoesGeral = 0;
            Dictionary<string, int> grupo = new Dictionary<string, int>();
            List<GrupoReacao> gruposReacao = db.GrupoReacaos.ToList();
            foreach(GrupoReacao gr in gruposReacao) { 
                string nomeGrupoReacao = gr.Nome;
                int totalReacoesGrupo = 0;
                List<TipoReacao> tiposReacao = db.TipoReacaos.Where(tr => tr.GrupoReacaoId == gr.GrupoReacaoId).ToList();
                foreach (TipoReacao t in tiposReacao) {
                    if(!(t.Reacoes is null))
                        totalReacoesGrupo += t.Reacoes.Count();
                }
                grupo.Add(nomeGrupoReacao, totalReacoesGrupo);
                totalReacoesGeral += totalReacoesGrupo;
            }
            ViewBag.TotalReacoesGeral = totalReacoesGeral;
            return View(grupo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Acesse os nossos contatos.";

            return View();
        }
    }
}