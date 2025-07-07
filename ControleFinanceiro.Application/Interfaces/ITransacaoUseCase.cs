using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransacaoUseCase
{
    List<TransacaoModel> ListarTodos(int usuarioId);

    TransacaoModel BuscarPorId(int transacaoId, int usuarioId);

    void Criar(TransacaoModel transacaoModel);

    void Atualizar(TransacaoModel transacaoModel);

    void Deletar(int transacaoId, int usuarioId);
}
