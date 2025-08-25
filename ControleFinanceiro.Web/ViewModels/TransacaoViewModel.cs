using ControleFinanceiro.Domain.Models;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels;

public class TransacaoViewModel
{
    public int Id { get; set; }

    [Required()]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required()]
    [DisplayName("É uma Fatura?")]
    public bool Fatura { get; set; } = false;

    [Required()]
    [DisplayName("Valor Planejado")]
    public decimal ValorPlanejado { get; set; }

    [Required()]
    [DisplayName("Valor Pago")]
    public decimal ValorPago { get; set; }

    [Required()]
    [DisplayName("Categoria")]
    public int CategoriaId { get; set; }

    public string CategoriaNome { get; set; } = string.Empty;

    [Required()]
    [DisplayName("Banco")]
    public int BancoId { get; set; }

    public string BancoNome { get; set; } = string.Empty;

    [Required()]
    [DisplayName("Tipo de Transação")]
    public int TipoTransacaoId { get; set; }

    public string? TipoTransacaoNome { get; set; } = string.Empty;

    [DisplayName("Data de Vencimento")]
    public DateTime? DataVencimento { get; set; }

    public string DataVencimentoFormatada => DataVencimento.HasValue ? DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;

    [Required()]
    [DisplayName("Data da Transação")]
    public DateTime DataTransacao { get; set; } = DateTime.Now;

    public string DataTransacaoFormatada => DataTransacao.ToString("dd/MM/yyyy");
}
