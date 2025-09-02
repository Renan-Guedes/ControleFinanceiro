using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ICarteiraRepository
{
    List<CarteiraModel> ListarTodos(int usuarioId);

    CarteiraModel? BuscarPorId(int carteiraId, int usuarioId);

    void Criar(CarteiraModel carteiraModel);

    void Atualizar(CarteiraModel carteiraModel);

    void Deletar(int carteiraId, int usuarioId);
}
