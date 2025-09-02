using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class CarteiraUseCase : ICarteiraUseCase
{
    private readonly ICarteiraRepository _repository;

    public CarteiraUseCase(ICarteiraRepository repository)
    {
        _repository = repository;
    }

    public void Criar(CarteiraModel carteiraModel)
        => _repository.Criar(carteiraModel);

    public void Atualizar(CarteiraModel carteiraModel)
        => _repository.Atualizar(carteiraModel);

    public void Deletar(int carteiraId, int usuarioId)
        => _repository.Deletar(carteiraId, usuarioId);

    public List<CarteiraModel> ListarTodos(int usuarioId)
        => _repository.ListarTodos(usuarioId);

    public CarteiraModel? BuscarPorId(int carteiraId, int usuarioId)
        => _repository.BuscarPorId(carteiraId, usuarioId);
}