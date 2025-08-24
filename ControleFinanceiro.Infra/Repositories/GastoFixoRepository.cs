using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class GastoFixoRepository : IGastoFixoRepository
{
    private readonly AppDbContext _db;

    public GastoFixoRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(GastoFixoModel gastoFixoModel)
    {
        _db.GastosFixos.Add(gastoFixoModel);
        _db.SaveChanges();
    }

    public void Atualizar(GastoFixoModel gastoFixoModel)
    {
        gastoFixoModel.DataAtualizacao = DateTime.Now;
        _db.GastosFixos.Update(gastoFixoModel);
        _db.SaveChanges();
    }

    public void Deletar(int gastoFixoId, int usuarioId)
    {
        var gastoFixo = _db.GastosFixos.FirstOrDefault(g => g.Id == gastoFixoId && g.UsuarioId == usuarioId);

        if (gastoFixo != null)
        {
            gastoFixo.DataExclusao = DateTime.Now;
            _db.GastosFixos.Update(gastoFixo);
            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Gasto fixo não encontrado ou não pertence ao usuário.");
        }
    }

    public List<GastoFixoModel> ListarTodos(int usuarioId)
    {
        return _db.GastosFixos
            .Include(g => g.Banco)
            .Include(g => g.Categoria)
            .Include(g => g.TipoTransacao)
            .Where(g => g.UsuarioId == usuarioId && g.DataExclusao == null)
            .ToList();
    }

    public GastoFixoModel? BuscarPorId(int gastoFixoId, int usuarioId)
    {
        return _db.GastosFixos
            .Include(g => g.Banco)
            .Include(g => g.Categoria)
            .Include(g => g.TipoTransacao)
            .FirstOrDefault(g => g.Id == gastoFixoId && g.UsuarioId == usuarioId && g.DataExclusao == null);
    }

    public decimal ObterTotalGastosFixos(int usuarioId)
    {
        var totalGastosFixos = _db.GastosFixos
            .Where(g => g.UsuarioId == usuarioId && g.DataExclusao == null);

        return totalGastosFixos.Any() ? totalGastosFixos.Sum(g => g.Valor) : 0;
    }
}
