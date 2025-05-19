using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories;

public interface ITransacaoRepository : IDisposable
{
    List<Transacao> Listar();
    Transacao ListarPorId(long id);
    void Criar(Transacao transacao);
    void Atualizar(Transacao transacao);
    void Deletar(long id);
}