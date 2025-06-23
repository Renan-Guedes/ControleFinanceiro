namespace ControleFinanceiro.Domain.Models;

public class FinanceiroModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; } = string.Empty;
    
    public double Valor { get; set; }
    
    public DateTime DataOperacao { get; set; }

    public int CategoriaId { get; set; }
    
    public CategoriaModel Categoria { get; set; }

    public int TransacaoId { get; set; }

    public TransacaoModel Transacao { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;
    
    public DateTime? DataAtualizacao { get; set; }
    
    public DateTime? DataExclusao { get; set; }
}