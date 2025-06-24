using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICategoriaUseCase
{
    List<CategoriaModel> ListarTodos();

    CategoriaModel? BuscarPorId(int id);

    void Criar(CategoriaModel categoria);
    
    void Atualizar(CategoriaModel categoria);
    
    void Deletar(int id);
}
