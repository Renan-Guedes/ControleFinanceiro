using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface ITransacaoUseCase
{
    List<Transacao> ListarTodas();
    Transacao PesquisarPorId(long id);
    void Criar(Transacao transacao);
    void Atualizar(Transacao transacao);
    void Deletar(long id);
}
