namespace ControleFinanceiro.Domain.Models;

public class TransacaoModel
{
    public int Id { get; set; }
    
    public string Descricao { get; set; } = string.Empty;

    public bool Fatura { get; set; }

    public decimal ValorPlanejado { get; set; }
    
    public decimal ValorPago { get; set; }

    #region Foreign Keys

    public int CategoriaId { get; set; }

    public CategoriaModel? Categoria { get; set; }

    public int TipoTransacaoId { get; set; } = 2; // Despesa por padrão

    public TipoTransacaoModel? TipoTransacao { get; set; }

    public int BancoId { get; set; } = 1; // Alterar Depois

    public BancoModel? Banco { get; set; }

    #endregion

    public DateTime? DataVencimento { get; set; }

    public DateTime DataTransacao { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;
    
    public DateTime? DataAtualizacao { get; set; }
    
    public DateTime? DataExclusao { get; set; }
}