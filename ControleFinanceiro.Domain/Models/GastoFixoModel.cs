namespace ControleFinanceiro.Domain.Models;

public class GastoFixoModel
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public int CategoriaId { get; set; }

    public int TipoTransacaoId { get; set; } = 2; // Despesa por padrão

    public int BancoId { get; set; }

    public BancoModel? Banco { get; set; }

    public int PlanjamentoMensalId { get; set; }

    public PlanejamentoMensalModel? PlanejamentoMensal { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
