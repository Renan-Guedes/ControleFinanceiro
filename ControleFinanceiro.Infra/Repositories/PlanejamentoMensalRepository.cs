using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Repositories;

public class CarteiraRepository : ICarteiraRepository
{
    private readonly AppDbContext _db;

    public CarteiraRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Criar(CarteiraModel carteiraModel)
    {
        _db.Carteiras.Add(carteiraModel);
        _db.SaveChanges();
    }

    public void Atualizar(CarteiraModel carteiraModel)
    {
        _db.Carteiras.Update(carteiraModel);
        _db.SaveChanges();
    }

    public void Deletar(int carteiraId, int usuarioId)
    {
        var carteira = _db.Carteiras
            .Include(b => b.Banco)
            .FirstOrDefault(p => p.Id == carteiraId && p.UsuarioId == usuarioId);

        if (carteira != null)
        {
            carteira.DataExclusao = DateTime.Now;
            _db.Carteiras.Update(carteira);
            _db.SaveChanges();
        }
        else
        {
            throw new Exception("Carteira não encontrada ou não pertence ao usuário.");
        }
    }

    public List<CarteiraModel> ListarTodos(int usuarioId)
    {
        return _db.Carteiras
            .Include(b => b.Banco)
            .Where(p => p.UsuarioId == usuarioId && p.DataExclusao == null)
            .ToList();
    }

    public CarteiraModel? BuscarPorId(int carteiraId, int usuarioId)
    {
        return _db.Carteiras
            .Include(b => b.Banco)
            .FirstOrDefault(p => p.Id == carteiraId && p.UsuarioId == usuarioId && p.DataExclusao == null);
    }
}
