namespace ControleFinanceiro.Domain.Models;

public class Categoria
{
    public long Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public long UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;
}
