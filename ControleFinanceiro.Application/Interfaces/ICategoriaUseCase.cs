using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICategoriaUseCase
{
    List<CategoriaModel> ListarTodos();
}
