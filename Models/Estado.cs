using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <summary>
    /// State
    /// </summary>
    public class Estado
    {
        [DisplayName("Estado")]
        public int EstadoId { get; set; } //Id
        public int PaisId { get; set; } //Country Id
        public Pais Pais { get; set; } //Country navigation property
        [DisplayName("Estado")]
        public String Nome { get; set; } //Name
        public IList<Cidade> Cidades { get; set; } //Cities
    }
}