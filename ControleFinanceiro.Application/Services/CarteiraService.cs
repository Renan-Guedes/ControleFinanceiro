using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Service;

public class CarteiraService : ICarteiraService
{
    private readonly ICarteiraRepository _repository;

    public CarteiraService(ICarteiraRepository repository)
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