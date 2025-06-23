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

    public void Criar(CategoriaModel categoria) => _repository.Criar(categoria);

    public void Atualizar(CategoriaModel categoria) => _repository.Atualizar(categoria);

    public void Deletar(int id) => _repository.Deletar(id);

    public List<CategoriaModel> ListarTodos() => _repository.Listar();
}
