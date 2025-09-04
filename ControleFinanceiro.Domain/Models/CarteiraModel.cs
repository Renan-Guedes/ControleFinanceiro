using System.Diagnostics.CodeAnalysis;

namespace ControleFinanceiro.Domain.Models;

public class CarteiraModel
{
    public int Id { get; set; }

    #region Foreign Keys

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public int UsuarioId { get; set; }

    public UsuarioModel? Usuario { get; set; }

    #endregion

    #region Collections

    public ICollection<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();

    #endregion

    public int Ano { get; set; } = DateTime.Now.Year;

    public int Mes { get; set; } = DateTime.Now.Month;

    public decimal SaldoInicial { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
