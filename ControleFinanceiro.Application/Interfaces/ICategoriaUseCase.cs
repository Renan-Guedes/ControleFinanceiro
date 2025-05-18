using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICategoriaUseCase
{
    List<Categoria> ListarTodas();
    Categoria PesquisarPorId(long id);
    List<Categoria> ListarPorNome(string categoria);
    void Criar(Categoria categoria);
    void Atualizar(Categoria categoria);
    void Deletar(long id);
}
