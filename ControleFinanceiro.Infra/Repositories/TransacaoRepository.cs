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

    public void Criar(TransacaoModel transacao)
    {
        _db.Transacoes.Add(transacao);
        _db.SaveChanges();
    }

    public void Atualizar(TransacaoModel transacao)
    {
        _db.Transacoes.Update(transacao);
        _db.SaveChanges();
    }

    public void Deletar(int id)
    {
        var transacao = _db.Transacoes.Find(id);

        if (transacao != null)
        {
            transacao.DataExclusao = DateTime.Now;

            _db.Transacoes.Update(transacao);

            _db.SaveChanges();
        }
    }

    public List<TransacaoModel> Listar()
    {
        return _db.Transacoes
            .Where(t => t.DataExclusao == null)
            .Include(c => c.Categoria)
            .Include(t => t.TipoTransacao)
            .ToList();
    }

    public TransacaoModel? BuscarPorId(int id)
    {
        return _db.Transacoes
           .FirstOrDefault(t => t.Id == id && t.DataExclusao == null);
    }
}
