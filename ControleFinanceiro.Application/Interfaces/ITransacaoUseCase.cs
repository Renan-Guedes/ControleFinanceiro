using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransacaoUseCase
{
    List<TransacaoModel> ListarTodos();

    TransacaoModel? BuscarPorId(int id);

    void Criar(TransacaoModel transacao);

    void Atualizar(TransacaoModel transacao);

    void Deletar(int id);
}
