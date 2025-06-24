using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;

namespace ControleFinanceiro.Infra.Repositories;

public class TipoTransacaoRepository : ITipoTransacaoRepository
{
    private readonly AppDbContext _db;

    public TipoTransacaoRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<TipoTransacaoModel> ListarTodos()
    {
        return _db.TipoTransacoes
            .Where(t => t.DataExclusao == null)
            .ToList();
    }
}
