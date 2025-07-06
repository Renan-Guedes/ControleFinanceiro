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

        [Required(ErrorMessage = "Ano é obrigatório.")]
        [Range(2000, 2100, ErrorMessage = "O Ano deve estar entre 2000 e 2100")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Mês é obrigatório.")]
        [Range(1, 12, ErrorMessage = "Mês inválido.")]
        public int Mes { get; set; }

        [Display(Name = "Saldo Inicial")]
        [Required(ErrorMessage = "Saldo Inicial é obrigatório.")]
        public decimal SaldoInicial { get; set; }
    }
}