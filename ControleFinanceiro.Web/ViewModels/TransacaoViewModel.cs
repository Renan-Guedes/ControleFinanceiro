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

    public string ValorPlanejadoFormatado => ValorPlanejado.ToString("C2");

    [DisplayName("Valor Pago")]
    public decimal? ValorPago { get; set; }

    public string? ValorPagoFormatado => ValorPago?.ToString("C2") ?? "-";

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

    public string DataVencimentoFormatada => DataVencimento.HasValue ? DataVencimento.Value.ToString("dd/MM/yyyy") : "-";

    [DisplayName("Data da Transação")]
    public DateTime? DataTransacao { get; set; } = null;

    public string? DataTransacaoFormatada => DataTransacao?.ToString("dd/MM/yyyy") ?? "-";

    public bool IsPago { get; set; } = false;
}
