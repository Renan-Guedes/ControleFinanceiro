using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class CategoriaUseCase : ICategoriaUseCase
{
    private readonly ICategoriaRepository _repository;

    public CategoriaUseCase(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public void Criar(CategoriaModel categoriaModel)
        => _repository.Criar(categoriaModel);
    public void Atualizar(CategoriaModel categoriaModel)
        => _repository.Atualizar(categoriaModel);

    public void Deletar(int categoriaId, int usuarioId)
        => _repository.Deletar(categoriaId, usuarioId);

    public List<CategoriaModel> ListarTodos(int usuarioId)
        => _repository.ListarTodos(usuarioId);

    public CategoriaModel? BuscarPorId(int categoriaId, int usuarioId)
        => _repository.BuscarPorId(categoriaId, usuarioId);
}
