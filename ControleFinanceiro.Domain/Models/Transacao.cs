using ControleFinanceiro.Domain.Enums;

namespace ControleFinanceiro.Domain.Models;

public class Transacao
{
    public long Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.Now;
    
    public DateTime? PagoOuRecebidoEm { get; set; }

    public EtipoTransacao Tipo { get; set; } = EtipoTransacao.Saida;

    public decimal Valor { get; set; }
    
    public long CategoriaId { get; set; }

    public Categoria Categoria { get; set; } = null!;
    
    public long UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
