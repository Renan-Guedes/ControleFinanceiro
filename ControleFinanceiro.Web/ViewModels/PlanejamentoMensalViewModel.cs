using ControleFinanceiro.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels
{
    public class PlanejamentoMensalViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Banco")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um banco válido.")]
        public int BancoId { get; set; }

        public string BancoNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Obrigatório")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "* Obrigatório")]
        [Display(Name = "Mês")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "* Obrigatório")]
        [Display(Name = "Saldo Inicial")]
        public decimal SaldoInicial { get; set; }
    }
}