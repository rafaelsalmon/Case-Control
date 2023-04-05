using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <name>
    /// Reaction Type
    /// </name>
    /// <summary>
    /// This is a possible type of reaction (for instance, "headache"). It is part of a reaction type group.
    /// </summary>
    public class TipoReacao
    {
        public int TipoReacaoId { get; set; } //Id

        [DisplayName("Nome da Reação")]
        public string Nome { get; set; } //Name

        public ICollection<Reacao> Reacoes { get; set; }

        [DisplayName("Grupo de Reação")] //Reaction Group Id
        public int? GrupoReacaoId { get; set; }

        public GrupoReacao GrupoReacao { get; set; } //Reaction Group navigation property

        public String CodigoAnotacao { get; set; } //Annotation Code

    }
}