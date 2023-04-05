using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ControleDeCasos.Models
{
    /// <summary>
    /// Country
    /// </summary>
    public class Pais
    {
        [DisplayName("Pais")]
        public int PaisId { get; set; } //Id

        [DisplayName("País")]
        public String Nome { get; set; } //Name
        public IList<Caso> Casos { get; set; } //Cases list, navigation property -> makes it easier to search/filter cases by Country
        public IList<Estado> Estados { get; set; } //States
    }
}