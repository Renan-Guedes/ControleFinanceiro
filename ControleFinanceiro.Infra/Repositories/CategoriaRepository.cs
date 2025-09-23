using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _db;

    public CategoriaRepository(AppDbContext db)
    {
        _db = db;
    }

    #region Métodos Básicos

    public void Criar(CategoriaModel categoriaModel)
    {
        _db.Categorias.Add(categoriaModel);
        _db.SaveChanges();
    }

    public void Atualizar(CategoriaModel categoriaModel)
    {
        categoriaModel.DataAtualizacao = DateTime.Now;
        _db.Categorias.Update(categoriaModel);
        _db.SaveChanges();
    }

    public void Deletar(int categoriaId, int usuarioId)
    {
        var categoria = _db.Categorias
            .FirstOrDefault(c => c.Id == categoriaId && c.UsuarioId == usuarioId);

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

    #endregion

    #region Métodos de Consulta

    public List<CategoriaModel> ListarTodos(int usuarioId)
    {
        return _db.Categorias
            .Where(c => c.UsuarioId == usuarioId && c.DataExclusao == null)
            .Include(t => t.TipoTransacao)
            .OrderBy(c => c.Nome)
            .ToList();
    }

    public List<CategoriaModel> ListarAtivos(int usuarioId)
    {
        return ListarTodos(usuarioId)
            .Where(c => c.Ativo == true)
            .ToList();
    }

    public List<CategoriaModel> ListarDespesasAtivas(int usuarioId)
    {
        return ListarAtivos(usuarioId).Where(c => c.TipoTransacaoId == 2).ToList();
    }

    public List<CategoriaModel> ListarReceitasAtivas(int usuarioId)
    {
        return ListarAtivos(usuarioId).Where(c => c.TipoTransacaoId == 1).ToList();
    }

    public CategoriaModel? BuscarPorId(int categoriaId, int usuarioId)
    {
        return _db.Categorias
            .FirstOrDefault(c => c.Id == categoriaId && c.UsuarioId == usuarioId && c.DataExclusao == null);
    }

    #endregion
}