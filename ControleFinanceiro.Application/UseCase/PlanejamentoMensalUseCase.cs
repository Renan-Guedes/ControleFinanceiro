using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Application.UseCase;

public class PlanejamentoMensalUseCase : IPlanejamentoMensalUseCase
{
    private readonly IPlanejamentoMensalRepository _repository;

    public PlanejamentoMensalUseCase(IPlanejamentoMensalRepository repository)
    {
        _repository = repository;
    }

    public void Criar(PlanejamentoMensalModel planejamentoMensalModel)
    {
        // Impede que um planejamento mensal seja criado no mesmo período para o mesmo banco
        var existePlanejamento = _repository
            .ListarTodos(planejamentoMensalModel.UsuarioId)
            .Any(p => p.Ano == planejamentoMensalModel.Ano
                && p.Mes == planejamentoMensalModel.Mes
                && p.BancoId == planejamentoMensalModel.BancoId);

        if (existePlanejamento)
            throw new Exception("Já existe um planejamento para este mês");
        else
            _repository.Criar(planejamentoMensalModel);
    }

    public void Atualizar(PlanejamentoMensalModel planejamentoMensalModel)
        => _repository.Atualizar(planejamentoMensalModel);
    

    public void Deletar(int planejamentoMensalId, int usuarioId)
        => _repository.Deletar(planejamentoMensalId, usuarioId);

    public List<PlanejamentoMensalModel> ListarTodos(int usuarioId)
        => _repository.ListarTodos(usuarioId);

    public PlanejamentoMensalModel? BuscarPorId(int planejamentoMensalId, int usuarioId)
        => _repository.BuscarPorId(planejamentoMensalId, usuarioId);
}