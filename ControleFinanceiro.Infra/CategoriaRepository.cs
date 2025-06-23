using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;

namespace ControleFinanceiro.Infra;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _db;

    public CategoriaRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<CategoriaModel> Listar()
    {
        return _db.Categorias
            .Where(c => c.DataExclusao == null)
            .ToList();
    }
}
