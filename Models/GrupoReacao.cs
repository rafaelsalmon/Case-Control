using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <summary>
    /// Reaction Type Group
    /// </summary>
    public class GrupoReacao
    {
        [DisplayName("Grupo de Reação")]
        public int GrupoReacaoId { get; set; } //Id
        [DisplayName("Grupo")]
        public String Nome { get; set; } //Nome
    }
}