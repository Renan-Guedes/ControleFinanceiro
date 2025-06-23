using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels;

public class CategoriaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O Título da Categoria é obrigatório")]
    public string Nome { get; set; }
    
    public string Descricao { get; set; }
    
    public DateTime? DataExclusao { get; set; }
    
    public bool Ativo { get; set; }
}
