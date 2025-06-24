using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ITipoTransacaoRepository
{
    List<TipoTransacaoModel> ListarTodos();
}