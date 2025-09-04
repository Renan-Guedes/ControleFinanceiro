namespace ControleFinanceiro.Domain.Models;

public class TransacaoModel
{
    public int Id { get; set; }

    #region Foreign Keys

    public int CategoriaId { get; set; }

    public CategoriaModel? Categoria { get; set; }

    public int TipoTransacaoId { get; set; }

    public TipoTransacaoModel? TipoTransacao { get; set; }

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public int UsuarioId { get; set; }

    public UsuarioModel? Usuario { get; set; }

    public int CarteiraId { get; set; }

    public CarteiraModel? Carteira { get; set; }

    public int? GastoFixoId { get; set; }

    public GastoFixoModel? GastoFixo { get; set; }

    #endregion

    public string Descricao { get; set; } = string.Empty;

    public bool Fatura { get; set; } = false;

    public decimal ValorPlanejado { get; set; }

    public decimal? ValorPago { get; set; } = null;

    public DateTime? DataVencimento { get; set; } = null;

    public DateTime? DataTransacao { get; set; } = null;

    public DateTime DataInclusao { get; set; } = DateTime.Now;
    
    public DateTime? DataAtualizacao { get; set; }
    
    public DateTime? DataExclusao { get; set; }
}