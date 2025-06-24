using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ITipoTransacaoUseCase
{
    List<TipoTransacaoModel> ListarTodos();
}