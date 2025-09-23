using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;

namespace ControleFinanceiro.Infra.Repositories;

public class BancoRepository : IBancoRepository
{
    private readonly AppDbContext _db;

    public BancoRepository(AppDbContext db)
    {
        _db = db;
    }

    #region Métodos Básicos

    public void Criar(BancoModel bancoModel)
    {
        _db.Bancos.Add(bancoModel);
        _db.SaveChanges();
    }

    public void Atualizar(BancoModel bancoModel)
    {
        bancoModel.DataAtualizacao = DateTime.Now;
        _db.Bancos.Update(bancoModel);
        _db.SaveChanges();
    }

    public void Deletar(int bancoId, int usuarioId)
    {
        var banco = _db.Bancos
            .FirstOrDefault(b => b.Id == bancoId && b.UsuarioId == usuarioId);

        if (banco != null)
        {
            banco.DataExclusao = DateTime.Now;
            _db.Bancos.Update(banco);
            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Banco não encontrado.");
        }
    }

    #endregion

    #region Métodos de Consulta

    public List<BancoModel> ListarTodos(int usuarioId)
    {
        return _db.Bancos
            .Where(b => b.UsuarioId == usuarioId && b.DataExclusao == null)
            .ToList();
    }

    public List<BancoModel> ListarAtivos(int usuarioId)
    {
        return ListarTodos(usuarioId).Where(b => b.Ativo == true).ToList();
    }

    public BancoModel? BuscarPorId(int bancoId, int usuarioId)
    {
        return _db.Bancos
            .FirstOrDefault(b => b.Id == bancoId && b.UsuarioId == usuarioId && b.DataExclusao == null);
    }

    #endregion
}
