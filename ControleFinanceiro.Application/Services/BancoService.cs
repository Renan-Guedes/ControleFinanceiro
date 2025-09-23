using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Service;

public class BancoService : IBancoService
{
    private readonly IBancoRepository _bancoRepository;

    public BancoService(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }
    
    public void Criar(BancoModel bancoModel) 
        => _bancoRepository.Criar(bancoModel);

    public void Atualizar(BancoModel bancoModel) 
        => _bancoRepository.Atualizar(bancoModel);
    
    public void Deletar(int bancoId, int usuarioId) 
        => _bancoRepository.Deletar(bancoId, usuarioId);
    
    public List<BancoModel> ListarTodos(int usuarioId) 
        => _bancoRepository.ListarTodos(usuarioId);

    public List<BancoModel> ListarAtivos(int usuarioId)
        => _bancoRepository.ListarAtivos(usuarioId);

    public BancoModel? BuscarPorId(int bancoId, int usuarioId) 
        => _bancoRepository.BuscarPorId(bancoId, usuarioId);

    public List<BancoModel> ListarDespesasAtivas(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public List<BancoModel> ListarReceitasAtivas(int usuarioId)
    {
        throw new NotImplementedException();
    }
}
