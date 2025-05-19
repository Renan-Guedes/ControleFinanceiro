using ControleFinanceiro.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels;

public class TransacaoViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório!")]
    public string Titulo { get; set; } = string.Empty;

    [Display(Name = "Data de Pagamento / Recebimento")]
    [Required(ErrorMessage = "Campo obrigatório!")]
    public DateTime? PagoOuRecebidoEm { get; set; }

    public string PagoOuRecebidoEmFormatado => 
            PagoOuRecebidoEm.HasValue
            ? PagoOuRecebidoEm.Value.ToString("dd/MM/yyyy") 
            : string.Empty;

    public EtipoTransacao Tipo { get; set; } = EtipoTransacao.Saida;

    [Range(0.01, 100000.00, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Display(Name = "Categoria")]
    [Required(ErrorMessage = "Campo obrigatório!")]
    public long CategoriaId { get; set; }

    public string CategoriaNome { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public string DataCriacaoFormatada => DataCriacao.ToString("dd/MM/yyyy");

    public List<SelectListItem> Categorias { get; set; } = new();
}
