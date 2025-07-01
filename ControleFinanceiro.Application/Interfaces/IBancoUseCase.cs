using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IBancoUseCase
{
    List<BancoModel> ListarTodos();
    BancoModel? BuscarPorId(int id);
    void Criar(BancoModel banco);
    void Atualizar(BancoModel banco);
    void Deletar(int id);
}
