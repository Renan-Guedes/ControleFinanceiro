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

    public void Criar(BancoModel banco)
    {
        _db.Bancos.Add(banco);
        _db.SaveChanges();
    }

    public void Atualizar(BancoModel banco)
    {
        banco.DataAtualizacao = DateTime.Now;
        _db.Bancos.Update(banco);
        _db.SaveChanges();
    }

    public void Deletar(int id)
    {
        var banco = _db.Bancos.Find(id);

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

    public List<BancoModel> Listar()
    {
        return _db.Bancos
            .Where(b => b.DataExclusao == null)
            .ToList();
    }

    public BancoModel? BuscarPorId(int id)
    {
        return _db.Bancos
            .FirstOrDefault(b => b.Id == id && b.DataExclusao == null);
    }
}
