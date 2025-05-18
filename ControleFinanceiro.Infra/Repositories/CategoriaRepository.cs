using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _db;

    public CategoriaRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(Categoria categoria)
    {
        _db.Categorias.Add(categoria);
        _db.SaveChanges();
    }

    public void Atualizar(Categoria categoria)
    {
        categoria.DataAtualizacao = DateTime.Now;
        _db.Entry<Categoria>(categoria).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public void Deletar(long id)
    {
        var categoria = _db.Categorias.Find(id);

        if (categoria != null)
        {
            categoria.DataExclusao = DateTime.Now;
            _db.SaveChanges();
        }
    }

    public List<Categoria> Listar()
    {
        return _db.Categorias
            .Where(x => x.DataExclusao == null)
            .ToList();
    }

    public Categoria ListarPorId(long id)
    {
        return _db.Categorias
            .FirstOrDefault(x => x.Id == id && x.DataExclusao == null);
    }

    public List<Categoria> ListarPorNome(string categoria)
    {
        return _db.Categorias
            .Where(x => x.Titulo.Contains(categoria) && x.DataExclusao == null)
            .ToList();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}