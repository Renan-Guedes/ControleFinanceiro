using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class TipoTransacaoUseCase : ITipoTransacaoUseCase
{
    private readonly ITipoTransacaoRepository _tipoTransacaoRepository;

    public TipoTransacaoUseCase(ITipoTransacaoRepository tipoTransacaoRepository)
    {
        _tipoTransacaoRepository = tipoTransacaoRepository;
    }

    public List<TipoTransacaoModel> ListarTodos() => _tipoTransacaoRepository.ListarTodos();
}
