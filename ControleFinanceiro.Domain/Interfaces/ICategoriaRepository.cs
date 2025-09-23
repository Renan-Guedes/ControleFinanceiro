using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ICategoriaRepository
{
    List<CategoriaModel> ListarTodos(int usuarioId);

    List<CategoriaModel> ListarAtivos(int usuarioId);

    List<CategoriaModel> ListarDespesasAtivas(int usuarioId);

    List<CategoriaModel> ListarReceitasAtivas(int usuarioId);

    CategoriaModel? BuscarPorId(int categoriaId, int usuarioId);

    void Criar(CategoriaModel categoriaModel);
    
    void Atualizar(CategoriaModel categoriaModel);
    
    void Deletar(int categoriaId, int usuarioId);
}