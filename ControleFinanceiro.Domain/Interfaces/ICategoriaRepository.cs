using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories;

public interface ICategoriaRepository : IDisposable
{
    List<Categoria> Listar();
    Categoria ListarPorId(int id);
    List<Categoria> ListarPorNome(string categoria);
    void Criar(Categoria categoria);
    void Atualizar(Categoria categoria);
    void Deletar(int id);
}
