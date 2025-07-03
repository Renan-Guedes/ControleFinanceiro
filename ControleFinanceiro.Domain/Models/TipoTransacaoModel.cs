namespace ControleFinanceiro.Domain.Models;

public class TipoTransacaoModel
{
    public int Id { get; set; }

    #region Collections

    public ICollection<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();

    public ICollection<GastoFixoModel> GastosFixos { get; set; } = new List<GastoFixoModel>();

    #endregion

    public string Nome { get; set; } = string.Empty;

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
