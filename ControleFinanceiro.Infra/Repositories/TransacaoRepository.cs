using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _db;

    public TransacaoRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(TransacaoModel transacaoModel)
    {
        _db.Transacoes.Add(transacaoModel);
        _db.SaveChanges();
    }

    public void Atualizar(TransacaoModel transacaoModel)
    {
        _db.Transacoes.Update(transacaoModel);
        _db.SaveChanges();
    }

    public void Deletar(int transacaoId, int usuarioId)
    {
        var transacao = _db.Transacoes
            .FirstOrDefault(t => t.Id == transacaoId && t.UsuarioId == usuarioId);

        if (transacao != null)
        {
            transacao.DataExclusao = DateTime.Now;

            _db.Transacoes.Update(transacao);

            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Transação não encontrado ou não pertence ao usuário.");
        }
    }

    public List<TransacaoModel> ListarTodos(int usuarioId)
    {
        return _db.Transacoes
            .Where(t => t.DataExclusao == null && t.UsuarioId == usuarioId)
            .Include(c => c.Categoria)
            .Include(t => t.TipoTransacao)
            .Include(b => b.Banco)
            .ToList();
    }

    public TransacaoModel? BuscarPorId(int transacaoId, int usuarioId)
    {
        return _db.Transacoes
            .FirstOrDefault(t => t.Id == transacaoId && t.UsuarioId == usuarioId);
    }
}
