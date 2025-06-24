namespace ControleFinanceiro.Domain.Models;

public class TipoTransacaoModel
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
