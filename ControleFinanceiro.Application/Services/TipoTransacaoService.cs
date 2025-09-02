using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Service;

public class TipoTransacaoService : ITipoTransacaoService
{
    private readonly ITipoTransacaoRepository _tipoTransacaoRepository;

    public TipoTransacaoService(ITipoTransacaoRepository tipoTransacaoRepository)
    {
        _tipoTransacaoRepository = tipoTransacaoRepository;
    }

    public List<TipoTransacaoModel> ListarTodos() => _tipoTransacaoRepository.ListarTodos();
}
