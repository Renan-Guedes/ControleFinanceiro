using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface IBancoRepository
{
    List<BancoModel> ListarTodos(int usuarioId);
    BancoModel? BuscarPorId(int bancoId, int usuarioId);
    void Criar(BancoModel bancoModel);
    void Atualizar(BancoModel bancoModel);
    void Deletar(int bancoId, int usuarioId);
}
