using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.UseCases
{
    public class TransacaoUseCase : ITransacaoUseCase
    {
        private readonly ITransacaoRepository _repository;

        public TransacaoUseCase(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        #region Métodos de Alteração

        public void Criar(Transacao transacao) => _repository.Criar(transacao);

        public void Atualizar(Transacao transacao) => _repository.Atualizar(transacao);

        public void Deletar(long id) => _repository.Deletar(id);

        #endregion

        #region Métodos de Pesquisa

        public List<Transacao> ListarTodas() => _repository.Listar();

        public Transacao PesquisarPorId(long id) => _repository.ListarPorId(id);

        #endregion
    }
}
