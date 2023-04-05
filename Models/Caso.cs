using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ControleDeCasos.Models
{
    /// <name>
    /// Case
    /// </name>
    /// <summary>
    /// This is the main entity. It represents a case being reported.
    /// </summary>
    /// <properties>
    /// The Case includes: 
    /// * Patient basic information (non-identifying data);
    /// * Basic patient medical information;
    /// 
    /// </properties>
    public class Caso
    {
        /// <summary>
        /// Case Id - Numeric, used internally by the software to identify a record of Case
        /// </summary>
        public int CasoId { get; set; }

        /// <summary>
        /// Validation rule: minimum brith date is 100 years ago
        /// </summary>

        public String dataNascimentoMinima = DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy");

        /// <summary>
        /// Text-based, control code used by doctors to identify the case
        /// </summary>

        [Required(ErrorMessage = "Controle é campo obrigatório.")]
        public string Controle { get; set; }

        /// <summary>
        /// Patient initials (the only reference to a patient's personal identification, so the doctor can remember)
        /// </summary>

        [Required(ErrorMessage = "Iniciais Paciente é campo obrigatório.")]

        [DisplayName("Iniciais Paciente")]
        public string IniciaisPaciente { get; set; }

        /// <summary>
        /// Birth Date
        /// </summary>

        [DisplayName("Data Nascimento")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Gender
        /// </summary>

        public int Sexo { get; set; } //EN-US: 0=male, 1=female //PT-BR: 0=masculino, 1=feminino

        [NotMapped]
        public String SexoTexto {
            get {

                if (Sexo == 0) return "Masculino";
                else return "Feminino";
            
            } 
        }

        /// <summary>
        /// Country Id
        /// </summary>
        [DisplayName("País")]
        public int PaisId { get; set; }

        /// <summary>
        /// Country navigational property
        /// </summary>
        public Pais Pais { get; set; }

        /// <summary>
        /// State Id
        /// </summary>

        [DisplayName("Estado")]
        public int EstadoId { get; set; }

        /// <summary>
        /// State navigational property
        /// </summary>

        public Estado Estado { get; set; }

        /// <summary>
        /// Comorbity
        /// </summary>
        public int Comorbidade { get; set; } //EN-US:  0=yes, 1=no, 2=unaware //PT_BR: 0=com, 1=sem, 2=não sabe

        /// <summary>
        /// Strings to be returned for Comorbity ("Comorbity Text")
        /// </summary>
        [NotMapped]
        public String ComorbidadeTexto
        {
            get
            {

                if (Comorbidade == 0) return "Com";
                else if (CondicaoEspecial == 1) return "Sem";
                else if (CondicaoEspecial == 2) return "Não Sabe";
                else return "";
            }
        }

        /// <summary>
        /// Previous Diagnostic
        /// </summary>
        [DisplayName("Diagnóstico Anterior")]
        public int DiagnosticoAnterior { get; set; } //EN-US: Covid-19 diagnosed before first dose of vaccination? Yes/No/Unaware. //PT-BR: Teve diagnosticada Covid-19 antes da primeira vacinação? sim/não/não sabe

        /// <summary>
        /// Strings to be returned for Previous Diagnostic ("Previous Diagnostic Text")
        /// </summary>
        [NotMapped]
        public String DiagnosticoAnteriorTexto
        {
            get
            {

                if (Comorbidade == 0) return "Sim";
                else if (CondicaoEspecial == 1) return "Não";
                else if (CondicaoEspecial == 2) return "Não Sabe";
                else return "";
            }
        }

        /// <summary>
        /// Special Condition. Can be None, Pregnant and Lactating.
        /// </summary>
        [DisplayName("Condição Especial")]
        public int CondicaoEspecial { get; set; }

        [NotMapped]
        public String CondicaoEspecialTexto
        {
            get
            {

                if (CondicaoEspecial == 0) return "Nennuhma";
                else if (CondicaoEspecial == 1) return "Gestante";
                else if (CondicaoEspecial == 2) return "Lactante";
                else return "";

            }
        } // Nenhuma, Gestante, Lactante

        /// <summary>
        /// Current State. Can be:
        /// * Mild sequels;
        /// * Moderate sequels;
        /// * Severe sequels;
        /// * Under treatment or resolution;
        /// * Death
        /// </summary>
        [DisplayName("Estado Atual")]
        public int EstadoAtual { get; set; } /* PT-BR:
                                              Com sequelas leves
                                                Com sequelas moderadas
                                                Com sequelas significativas ou graves
                                                Em tratamento / em resolução
                                                Óbito
                                            */

        
        /// <summary>
        /// Current State Text Strings
        /// </summary>
        [NotMapped]
        public String EstadoAtualTexto
        {
            get
            {

                if (CondicaoEspecial == 0) return "Com sequelas leves";
                else if (CondicaoEspecial == 1) return "Com sequelas moderadas";
                else if (CondicaoEspecial == 2) return "Com sequelas significativas ou graves";
                else if (CondicaoEspecial == 3) return "Em tratamento / em resolução";
                else if (CondicaoEspecial == 3) return "Óbito";
                else return "";

            }
        } // Nenhuma, Gestante, Lactante

        /// <summary>
        /// Collection of Reactions. Navigation Property.
        /// </summary>
        public ICollection<Reacao> Reacoes{ get; set; }

        
        // EN-US: Beginning of the properties related to all reactions:
        // PT-BR: Inicio dos campos relativos a todas as reações:

        /// <summary>
        /// User E-mail
        /// </summary>
        public String EmailUsuario { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>

        [DisplayName("Data de Início")]
        public DateTime? DataInicio { get; set; }

        /// <summary>
        /// Hospitalized
        /// </summary>
        public bool Hospitalizado { get; set; }

        /// <summary>
        /// Severity
        /// </summary>
        public int Severidade { get; set; } //EN-US: mild, moderate, severe // PT-BR: leve/moderada/grave

        /// <summary>
        /// Text Strings for Severity
        /// </summary>
        [NotMapped]
        public String SeveridadeTexto
        {
            get
            {
                if (Severidade == 0) return "Leve";
                else if (Severidade == 1) return "Moderada";
                else return "Grave";
            }
        }


        //EN-US:
        //The model below is not normalized because it is impossible for a patient to receive more than 5 doses
        //(the alternative would be to create a relationship with a separate "InjectedDose" object classe).


        //PT-BR:
        //Modelo abaixo desnormalizado pois não é possível haver mais do que 5 doses (a alternativa seria
        //criar um relacionamento com uma classe de objetos "Dose" separada:

        //EN-US: First Dose
        //PT-BR: Primeira Dose

        [DisplayName("Laboratório da Primeira Dose")]
        public int PrimeiraDoseLaboratorioId { get; set; } //Lab Id
        [DisplayName("Laboratório da Primeira Dose")]
        public Laboratorio PrimeiraDoseLaboratorio { get; set; } //Lab navigational property
        [DisplayName("Data da Primeira Dose")]
        public DateTime PrimeiraDoseData { get; set; } // Date of first dose
        [DisplayName("Lote da Primeira Dose")]
        public int PrimeiraDoseLote { get; set; } // Batch of first dose
        [DisplayName("Houve Erro de Aplicação da Primeira Dose?")]
        public bool PrimeiraDoseErroAplicacao { get; set; } //Did any error occur during the injection of the dose?

        //EN-US: Second Dose
        //PT-BR: Segunda Dose

        [DisplayName("Laboratório da Segunda Dose")]
        public int SegundaDoseLaboratorioId { get; set; }
        [DisplayName("Laboratório da Segunda Dose")]
        public Laboratorio SegundaDoseLaboratorio { get; set; }
        [DisplayName("Data da Segunda Dose")]
        public DateTime SegundaDoseData { get; set; }
        [DisplayName("Lote da Segunda Dose")]
        public int SegundaDoseLote { get; set; }
        [DisplayName("Houve Erro de Aplicação da Segunda Dose?")]
        public bool SegundaDoseErroAplicacao { get; set; }

        //EN-US: Third Dose
        //PT-BR: Terceira Dose

        [DisplayName("Laboratório da Terceira Dose")]
        public int TerceiraDoseLaboratorioId { get; set; }
        [DisplayName("Laboratório da Terceira Dose")]
        public Laboratorio TerceiraDoseLaboratorio { get; set; }
        [DisplayName("Data da Terceira Dose")]
        public DateTime TerceiraDoseData { get; set; }
        [DisplayName("Lote da Terceira Dose")]
        public int TerceiraDoseLote { get; set; }
        [DisplayName("Houve Erro de Aplicação da Terceira Dose?")]
        public bool TerceiraDoseErroAplicacao { get; set; }

        //EN-US: Fourth Dose
        //PT-BR: Quarta Dose

        [DisplayName("Laboratório da Quarta Dose")]
        public int QuartaDoseLaboratorioId { get; set; }
        [DisplayName("Laboratório da Quarta Dose")]
        public Laboratorio QuartaDoseLaboratorio { get; set; }
        [DisplayName("Data da Quarta Dose")]
        public DateTime QuartaDoseData { get; set; }
        [DisplayName("Lote da Quarta Dose")]
        public int QuartaDoseLote { get; set; }
        [DisplayName("Houve Erro de Aplicação da Quarta Dose?")]
        public bool QuartaDoseErroAplicacao { get; set; }

        //EN-US: Fifth Dose
        //PT-BR: Quinta Dose

        [DisplayName("Laboratório da Quinta Dose")]
        public int QuintaDoseLaboratorioId { get; set; }
        [DisplayName("Laboratório da Quinta Dose")]
        public Laboratorio QuintaDoseLaboratorio { get; set; }
        [DisplayName("Data da Quinta Dose")]
        public DateTime QuintaDoseData { get; set; }
        [DisplayName("Lote da Quinta Dose")]
        public int QuintaDoseLote { get; set; }
        [DisplayName("Houve Erro de Aplicação da Quinta Dose?")]
        public bool QuintaDoseErroAplicacao { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime? DataCriacao { get; set; }
        [DisplayName("Data da Última Atualização")]
        public DateTime? DataUltimaAtualizacao { get; set; }
    }
}