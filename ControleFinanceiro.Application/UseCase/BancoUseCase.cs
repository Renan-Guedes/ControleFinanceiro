using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class BancoUseCase : IBancoUseCase
{
    private readonly IBancoRepository _bancoRepository;

    public BancoUseCase(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }

    public void Criar(BancoModel banco) => _bancoRepository.Criar(banco);

    public void Atualizar(BancoModel banco) => _bancoRepository.Atualizar(banco);

    public BancoModel? BuscarPorId(int id) => _bancoRepository.BuscarPorId(id);

    public void Deletar(int id) => _bancoRepository.Deletar(id);

    public List<BancoModel> ListarTodos() => _bancoRepository.Listar();
}
