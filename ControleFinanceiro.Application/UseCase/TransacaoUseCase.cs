using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class TransacaoUseCase : ITransacaoUseCase
{
    private readonly ITransacaoRepository _transacaoRepository;

    public TransacaoUseCase(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public void Criar(TransacaoModel transacao) => _transacaoRepository.Criar(transacao);

    public void Atualizar(TransacaoModel transacao) => _transacaoRepository.Atualizar(transacao);

    public void Deletar(int id) => _transacaoRepository.Deletar(id);

    public List<TransacaoModel> ListarTodos() => _transacaoRepository.Listar();

    public TransacaoModel? BuscarPorId(int id) => _transacaoRepository.BuscarPorId(id);    
}