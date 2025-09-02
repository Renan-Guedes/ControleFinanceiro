using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.Interfaces;

public interface IGastoFixoService
{
    List<GastoFixoModel> ListarTodos(int usuarioId, int? bancoId = null, int? categoriaId = null, int? tipoTransacaoId = null);

    GastoFixoModel? BuscarPorId(int gastoFixoId, int usuarioId);

    void Criar(GastoFixoModel gastoFixoModel);

    void Atualizar(GastoFixoModel gastoFixoModel);

    void Deletar(int gastoFixoId, int usuarioId);

    decimal ObterTotalGastosFixos(int usuarioId);
}
