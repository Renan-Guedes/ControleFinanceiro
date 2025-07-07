using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class PlanejamentoMensalRepository : IPlanejamentoMensalRepository
{
    private readonly AppDbContext _db;

    public PlanejamentoMensalRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(PlanejamentoMensalModel planejamentoMensalModel)
    {
        _db.PlanejamentosMensais.Add(planejamentoMensalModel);
        _db.SaveChanges();
    }

    public void Atualizar(PlanejamentoMensalModel planejamentoMensalModel)
    {
        _db.PlanejamentosMensais.Update(planejamentoMensalModel);
        _db.SaveChanges();
    }

    public void Deletar(int planejamentoMensalId, int usuarioId)
    {
        var planejamentoMensal = _db.PlanejamentosMensais
            .Include(b => b.Banco)
            .FirstOrDefault(p => p.Id == planejamentoMensalId && p.UsuarioId == usuarioId);

        if (planejamentoMensal != null)
        {
            planejamentoMensal.DataExclusao = DateTime.Now;
            _db.PlanejamentosMensais.Update(planejamentoMensal);
            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Planejamento mensal não encontrado ou não pertence ao usuário.");
        }
    }

    public List<PlanejamentoMensalModel> ListarTodos(int usuarioId)
    {
        return _db.PlanejamentosMensais
            .Include(b => b.Banco)
            .Where(p => p.UsuarioId == usuarioId && p.DataExclusao == null)
            .ToList();
    }

    public PlanejamentoMensalModel? BuscarPorId(int planejamentoMensalId, int usuarioId)
    {
        return _db.PlanejamentosMensais
            .Include(b => b.Banco)
            .FirstOrDefault(p => p.Id == planejamentoMensalId && p.UsuarioId == usuarioId && p.DataExclusao == null);
    }
}
