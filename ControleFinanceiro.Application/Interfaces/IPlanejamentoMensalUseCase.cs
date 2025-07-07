using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IPlanejamentoMensalUseCase
{
    List<PlanejamentoMensalModel> ListarTodos(int usuarioId);

    PlanejamentoMensalModel? BuscarPorId(int planejamentoMensalId, int usuarioId);

    void Criar(PlanejamentoMensalModel planejamentoMensalModel);

    void Atualizar(PlanejamentoMensalModel planejamentoMensalModel);

    void Deletar(int planejamentoMensalId, int usuarioId);
}
