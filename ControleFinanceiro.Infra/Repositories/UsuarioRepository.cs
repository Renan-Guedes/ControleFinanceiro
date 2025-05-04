using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;

    public UsuarioRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(Usuario usuario)
    {
        _db.Usuarios.Add(usuario);
        _db.SaveChanges();
    }

    public void Atualizar(Usuario usuario)
    {
        usuario.DataAtualizacao = DateTime.Now;
        _db.Entry<Usuario>(usuario).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public void Deletar(int id)
    {
        var usuario = _db.Usuarios.Find(id);

        if (usuario != null)
        {
            usuario.DataExclusao = DateTime.Now;
            _db.SaveChanges();
        }
    }

    public List<Usuario> Listar()
    {
        return _db.Usuarios
            .Where(x => x.DataExclusao == null)
            .ToList();
    }

    public Usuario ListarPorId(int id)
    {
        return _db.Usuarios
            .FirstOrDefault(x => x.Id == id && x.DataExclusao == null);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}