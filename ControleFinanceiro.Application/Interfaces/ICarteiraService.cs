using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICarteiraService
{
    List<CarteiraModel> ListarTodos(int usuarioId);

    CarteiraModel? BuscarPorId(int carteiraId, int usuarioId);

    void Criar(CarteiraModel carteiraModel);

    void Atualizar(CarteiraModel carteiraModel);

    void Deletar(int carteiraId, int usuarioId);
}
