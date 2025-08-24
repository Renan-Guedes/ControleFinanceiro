using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Web.ViewModels
{
    public class HomeViewModel
    {
        public decimal TotalReceitas { get; set; }

        public decimal TotalDespesas { get; set; }

        public decimal SaldoAtual { get; set; }

        public decimal TotalGastosFixos { get; set; }

        public List<TransacaoViewModel> UltimasTransacoes { get; set; } = new();
    }
}
