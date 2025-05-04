using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories;

public interface ITransacaoRepository : IDisposable
{
    List<Transacao> Listar();
    Transacao ListarPorId(int id);
    List<Transacao> ListarPorNome(string nome);
    void Criar(Transacao transacao);
    void Atualizar(Transacao transacao);
    void Deletar(int id);
}