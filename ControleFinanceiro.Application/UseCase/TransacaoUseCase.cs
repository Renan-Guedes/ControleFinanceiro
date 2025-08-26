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

    public void Criar(TransacaoModel transacaoModel)
        => _transacaoRepository.Criar(transacaoModel);

    public void Atualizar(TransacaoModel transacaoModel)
        => _transacaoRepository.Atualizar(transacaoModel);

    public void Deletar(int transacaoId, int usuarioId)
        => _transacaoRepository.Deletar(transacaoId, usuarioId);

    public List<TransacaoModel> ListarTodos(int usuarioId)
        => _transacaoRepository.ListarTodos(usuarioId);

    public TransacaoModel BuscarPorId(int transacaoId, int usuarioId)
        => _transacaoRepository.BuscarPorId(transacaoId, usuarioId);

    public List<TransacaoModel> ListarTodasAsReceitas(int usuarioId)
        => _transacaoRepository.ListarTodasAsReceitas(usuarioId);

    public decimal ObterTotalReceitas(int usuarioId)
        => _transacaoRepository.ObterTotalReceitas(usuarioId);

    public decimal ObterTotalDespesas(int usuarioId)
        => _transacaoRepository.ObterTotalDespesas(usuarioId);

    public List<TransacaoModel> ListarTodasAsDespesas(int usuarioId)
        => _transacaoRepository.ListarTodasAsDespesas(usuarioId);
}