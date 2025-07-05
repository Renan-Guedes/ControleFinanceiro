using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface IPlanejamentoMensalRepository
{
    List<PlanejamentoMensalModel> ListarTodos(int usuarioId);

    PlanejamentoMensalModel? ListarPorId(int planejamentoMensalId, int usuarioId);

    void Criar(PlanejamentoMensalModel planejamentoMensalModel);

    void Atualizar(PlanejamentoMensalModel planejamentoMensalModel);

    void Deletar(int planejamentoMensalId, int usuarioId);
}
