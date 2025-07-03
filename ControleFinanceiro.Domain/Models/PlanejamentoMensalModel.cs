namespace ControleFinanceiro.Domain.Models;

public class PlanejamentoMensalModel
{
    public int Id { get; set; }

    #region Foreign Keys

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public int UsuarioId { get; set; }

    public UsuarioModel? Usuario { get; set; }

    #endregion

    #region Collections

    public ICollection<GastoFixoModel> GastosFixos { get; set; } = new List<GastoFixoModel>();

    #endregion

    public int Ano { get; set; }

    public int Mes { get; set; }

    public decimal SaldoInicial { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
