using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ITransacaoRepository
{
    List<TransacaoModel> ListarTodos(int usuarioId);

    TransacaoModel BuscarPorId(int transacaoId, int usuarioId);

    void Criar(TransacaoModel transacaoModel);

    void Atualizar(TransacaoModel transacaoModel);
    
    void Deletar(int transacaoId, int usuarioid);

    List<TransacaoModel> ListarTodasAsReceitas(int usuarioId);

    decimal ObterTotalReceitas(int usuarioId);

    decimal ObterTotalDespesas(int usuarioId);
}
