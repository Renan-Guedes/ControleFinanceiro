using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ITipoTransacaoService
{
    List<TipoTransacaoModel> ListarTodos();
}