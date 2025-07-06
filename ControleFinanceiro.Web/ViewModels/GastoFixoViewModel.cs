using ControleFinanceiro.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels
{
    public class GastoFixoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* obrigatório")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public string CategoriaNome { get; set; } = string.Empty;

        public int TipoTransacaoId { get; set; } = 2; // Despesa por padrão

        public string TipoTransacaoNome => TipoTransacaoId == 1 ? "Receita" : "Despesa";

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "* obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "* obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "* obrigatório")]
        [Display(Name = "Banco")]
        public int BancoId { get; set; }

        public string BancoNome { get; set; } = string.Empty;
    }
}