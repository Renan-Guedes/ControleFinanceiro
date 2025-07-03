namespace ControleFinanceiro.Domain.Models;

public class BancoModel
{
    public int Id { get; set; }

    #region Foreign Keys

    public int UsuarioId { get; set; }

    public UsuarioModel? Usuario { get; set; }

    #endregion

    #region Collections

    public ICollection<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();

    public ICollection<GastoFixoModel> GastosFixos { get; set; } = new List<GastoFixoModel>();

    #endregion

    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}