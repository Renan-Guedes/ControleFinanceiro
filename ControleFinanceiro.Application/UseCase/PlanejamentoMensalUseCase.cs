using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using System.Runtime.CompilerServices;

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
        var existePlanejamento = _repository.ListarTodos(planejamentoMensalModel.UsuarioId)
            .Any(p => p.Ano == planejamentoMensalModel.Ano 
                && p.Mes == planejamentoMensalModel.Mes);

        if (existePlanejamento)
        {
            throw new Exception("Já existe um planejamento para este mês");
        }
        
        _repository.Criar(planejamentoMensalModel);
    } 

    public void Atualizar(PlanejamentoMensalModel planejamentoMensalModel) => _repository.Atualizar(planejamentoMensalModel);

    public void Deletar(int planejamentoMensalId, int usuarioId) => _repository.Deletar(planejamentoMensalId, usuarioId);

    public List<PlanejamentoMensalModel> ListarTodos(int usuarioId) => _repository.ListarTodos(usuarioId);

    public PlanejamentoMensalModel? ListarPorId(int planejamentoMensalId, int usuarioId) => _repository.ListarPorId(planejamentoMensalId, usuarioId);
}