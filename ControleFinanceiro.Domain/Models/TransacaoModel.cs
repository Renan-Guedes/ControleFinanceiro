namespace ControleFinanceiro.Domain.Models;

public class TransacaoModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public DateTime DataInclusao { get; set; } = DateTime.Now;
    
    public DateTime? DataAtualizacao { get; set; }
    
    public DateTime? DataExclusao { get; set; }
}