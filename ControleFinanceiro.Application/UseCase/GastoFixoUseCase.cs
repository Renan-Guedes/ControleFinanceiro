using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class GastoFixoUseCase : IGastoFixoUseCase
{
    private readonly IGastoFixoRepository _repository;

    public GastoFixoUseCase(IGastoFixoRepository repository)
    {
        _repository = repository;
    }

    public void Criar(GastoFixoModel gastoFixoModel)
        => _repository.Criar(gastoFixoModel);

    public void Atualizar(GastoFixoModel gastoFixoModel)
        => _repository.Atualizar(gastoFixoModel);

    public void Deletar(int gastoFixoId, int usuarioId)
        => _repository.Deletar(gastoFixoId, usuarioId);

    public List<GastoFixoModel> ListarTodos(int usuarioId, int? bancoId = null, int? categoriaId = null, int? tipoTransacaoId = null)
        => _repository.ListarTodos(usuarioId);

    public GastoFixoModel? BuscarPorId(int gastoFixoId, int usuarioId)
        => _repository.BuscarPorId(gastoFixoId, usuarioId);
}
