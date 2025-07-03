namespace ControleFinanceiro.Domain.Models;

public class GastoFixoModel
{
    public int Id { get; set; }

    #region Foreign Keys

    public int CategoriaId { get; set; }

    public CategoriaModel? Categoria { get; set; }

    public int TipoTransacaoId { get; set; } = 2; // Despesa por padrão

    public TipoTransacaoModel? TipoTransacao { get; set; }

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public int PlanejamentoMensalId { get; set; }

    public PlanejamentoMensalModel? PlanejamentoMensal { get; set; }

    public int UsuarioId { get; set; }

    public UsuarioModel? Usuario { get; set; }

    #endregion

    public string? Descricao { get; set; } = string.Empty;
    
    public decimal Valor { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
