using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransacaoService
{
    List<TransacaoModel> ListarTodos(int usuarioId);

    TransacaoModel BuscarPorId(int transacaoId, int usuarioId);

    void Criar(TransacaoModel transacaoModel);

    void Atualizar(TransacaoModel transacaoModel);

    void Deletar(int transacaoId, int usuarioId);

    List<TransacaoModel> ListarTodasAsReceitas(int usuarioId);

    List<TransacaoModel> ListarTodasAsDespesas(int usuarioId);

    decimal ObterTotalReceitas(int usuarioId);

    decimal ObterTotalDespesas(int usuarioId);
}
