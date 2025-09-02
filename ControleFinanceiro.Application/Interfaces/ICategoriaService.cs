using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICategoriaService
{
    List<CategoriaModel> ListarTodos(int usuarioId);

    CategoriaModel? BuscarPorId(int categoriaId, int usuarioId);

    void Criar(CategoriaModel categoriaModel);

    void Atualizar(CategoriaModel categoriaModel);

    void Deletar(int categoriaId, int usuarioId);
}
