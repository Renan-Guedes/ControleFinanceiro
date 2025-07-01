namespace ControleFinanceiro.Domain.Models;

public class BancoModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}