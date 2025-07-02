namespace ControleFinanceiro.Domain.Models;

public class PlanejamentoMensalModel
{
    public int Id { get; set; }
    
    public int Ano { get; set; }
    
    public int Mes { get; set; }
    
    public decimal SaldoInicial { get; set; }

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
