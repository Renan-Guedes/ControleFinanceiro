using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ITransacaoRepository
{
    List<TransacaoModel> Listar();

    TransacaoModel BuscarPorId(int id);

    void Criar(TransacaoModel transacao);

    void Atualizar(TransacaoModel transacao);
    
    void Deletar(int id);
}
