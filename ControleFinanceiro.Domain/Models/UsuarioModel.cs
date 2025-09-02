namespace ControleFinanceiro.Domain.Models;

public class UsuarioModel
{
    public int Id { get; set; }

    #region Collections 

    public ICollection<BancoModel> Bancos { get; set; } = new List<BancoModel>();

    public ICollection<CategoriaModel> Categorias { get; set; } = new List<CategoriaModel>();

    public ICollection<GastoFixoModel> GastosFixos { get; set; } = new List<GastoFixoModel>();

    public ICollection<CarteiraModel> Carteiras { get; set; } = new List<CarteiraModel>();

    public ICollection<TransacaoModel> Transacoes { get; set; } = new List<TransacaoModel>();

    #endregion

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}
