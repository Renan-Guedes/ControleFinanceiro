using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels;

public class CategoriaViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Título é obrigatório")]
    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }
}
