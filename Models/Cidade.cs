using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <summary>
    /// City
    /// </summary>
    public class Cidade
    {
        [DisplayName("Cidade")]
        public int CidadeId { get; set; } //Id
        public int EstadoId { get; set; } //StateId
        public Estado Estado { get; set; } //State - navigation property

        [DisplayName("Cidade")]
        public String Nome { get; set; } //Name
    }
}