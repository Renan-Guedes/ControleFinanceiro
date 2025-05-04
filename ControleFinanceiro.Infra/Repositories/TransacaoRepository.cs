using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
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

    public void Criar(Transacao transacao)
    {
        _db.Transacoes.Add(transacao);
        _db.SaveChanges();
    }

    public void Atualizar(Transacao transacao)
    {
        transacao.DataAtualizacao = DateTime.Now;
        _db.Entry<Transacao>(transacao).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public void Deletar(int id)
    {
        var transacao = _db.Transacoes.Find(id);

        if (transacao != null)
        {
            transacao.DataExclusao = DateTime.Now;
            _db.SaveChanges();
        }
    }

    public List<Transacao> Listar()
    {
        return _db.Transacoes
            .Where(x => x.DataExclusao == null)
            .ToList();
    }

    public Transacao ListarPorId(int id)
    {
        return _db.Transacoes
            .FirstOrDefault(x => x.Id == id && x.DataExclusao == null);
    }

    public List<Transacao> ListarPorNome(string transacao)
    {
        return _db.Transacoes
            .Where(x => x.Titulo.Contains(transacao) && x.DataExclusao == null)
            .ToList();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}