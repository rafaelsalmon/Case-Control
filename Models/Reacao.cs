using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <name>
    /// Reaction
    /// </name>
    /// <summary>
    /// This is an actual patient's reaction reported to the doctor.
    /// </summary>
    public class Reacao
    {
        public int ReacaoId { get; set; } // Id
        public int? TipoReacaoId { get; set; } //Type Id
        public TipoReacao TipoReacao { get; set; } //Type navigation property
        public int CasoId { get; set; } //Case Id
        public Caso Caso { get; set; } //Case navigation prpoperty
        public String EmailUsuario { get; set; } //User e-mail

    }
}