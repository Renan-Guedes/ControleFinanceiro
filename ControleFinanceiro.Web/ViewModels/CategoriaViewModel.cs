using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels;

public class CategoriaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "* obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "* obrigatório")]
    [Display(Name = "Está Ativo?")]
    public bool Ativo { get; set; }
}
