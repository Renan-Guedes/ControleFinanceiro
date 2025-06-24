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

    public void Criar(CategoriaModel categoria)
    {
        _db.Categorias.Add(categoria);
        _db.SaveChanges();
    }

    public void Atualizar(CategoriaModel categoria)
    {
        categoria.DataAtualizacao = DateTime.Now;
        _db.Categorias.Update(categoria);
        _db.SaveChanges();
    }

    public void Deletar(int id)
    {
        var categoria = _db.Categorias.Find(id);

        if(categoria != null )
        {
            categoria.DataExclusao = DateTime.Now;
            _db.Categorias.Update(categoria);
            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Categoria não encontrada.");
        }
    }

    public List<CategoriaModel> Listar()
    {
        return _db.Categorias
            .Where(c => c.DataExclusao == null)
            .ToList();
    }

    public CategoriaModel? BuscarPorId(int id)
    {
        return _db.Categorias
            .FirstOrDefault(c => c.Id == id && c.DataExclusao == null);
    }
}
