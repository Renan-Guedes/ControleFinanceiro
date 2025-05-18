using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.UseCases;

public class CategoriaUseCase : ICategoriaUseCase
{
    private readonly ICategoriaRepository _repository;

    public CategoriaUseCase(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    #region Métodos de Alteração

    public void Criar(Categoria categoria) => _repository.Criar(categoria);

    public void Atualizar(Categoria categoria) => _repository.Atualizar(categoria);

    public void Deletar(long id) => _repository.Deletar(id);

    #endregion

    #region Métodos de Pesquisa

    public List<Categoria> ListarTodas() => _repository.Listar();

    public Categoria PesquisarPorId(long id) => _repository.ListarPorId(id);

    public List<Categoria> ListarPorNome(string categoria) => _repository.ListarPorNome(categoria);

    #endregion
}
