using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ITransacaoUseCase _transacaoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;
        private readonly ITipoTransacaoUseCase _tipoTransacaoUseCase;
        private readonly IBancoUseCase _bancoUseCase;

        public TransacaoController(ITransacaoUseCase transacaoUseCase, ICategoriaUseCase categoriaUseCase, ITipoTransacaoUseCase tipoTransacaoUseCase, IBancoUseCase bancoUseCase)
        {
            _transacaoUseCase = transacaoUseCase;
            _categoriaUseCase = categoriaUseCase;
            _tipoTransacaoUseCase = tipoTransacaoUseCase;
            _bancoUseCase = bancoUseCase;
        }

        // GET: /Transacao
        public IActionResult Index()
        {
            var transacoes = _transacaoUseCase.ListarTodos().ToList();

            var viewModels = transacoes.Select(t => new TransacaoViewModel
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Fatura = t.Fatura,
                ValorPlanejado = t.ValorPlanejado,
                ValorPago = t.ValorPago,
                CategoriaId = t.CategoriaId,
                CategoriaNome = t.Categoria?.Nome ?? "Categoria Não Encontrada",
                BancoId = t.BancoId,
                BancoNome = t.Banco?.Nome ?? "Banco Não Encontrado",
                TipoTransacaoId = t.TipoTransacaoId,
                TipoTransacaoNome = t.TipoTransacao?.Nome ?? "Tipo de Transação Não Encontrada", 
                DataVencimento = t.DataVencimento,
                DataTransacao = t.DataTransacao
            }).ToList();

            return View(viewModels);
        }

        //GET: /Transacao/Criar
        public IActionResult Criar()
        {
            PreencherViewBags();
            return View();
        }

        //POST: /Transacao/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(TransacaoViewModel transacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                PreencherViewBags();
                return View(transacaoViewModel);
            }

            try
            {
                var novaTransacao = new TransacaoModel
                {
                    Descricao = transacaoViewModel.Descricao,
                    TipoTransacaoId = transacaoViewModel.TipoTransacaoId,
                    Fatura = transacaoViewModel.Fatura,
                    ValorPlanejado = transacaoViewModel.ValorPlanejado,
                    ValorPago = transacaoViewModel.ValorPago,
                    CategoriaId = transacaoViewModel.CategoriaId,
                    BancoId = transacaoViewModel.BancoId,
                    DataVencimento = transacaoViewModel.DataVencimento,
                    DataTransacao = transacaoViewModel.DataTransacao
                };

                _transacaoUseCase.Criar(novaTransacao);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // logar o erro aqui se possível
                ModelState.AddModelError(string.Empty, "Erro ao salvar transação: " + ex.Message);
                PreencherViewBags();
                return View(transacaoViewModel);
            }
        }

        private void PreencherViewBags()
        {
            ViewBag.Categorias = _categoriaUseCase.ListarTodos().ToList();

            ViewBag.TipoTransacao = _tipoTransacaoUseCase.ListarTodos().ToList();

            ViewBag.Bancos = _bancoUseCase.ListarTodos().ToList();
        }

        // GET: /Transacao/Editar/{id}
        public IActionResult Editar(int id)
        {
            var transacao = _transacaoUseCase.BuscarPorId(id);
            if (transacao == null) return NotFound();

            var vm = new TransacaoViewModel
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                TipoTransacaoId = transacao.TipoTransacaoId,
                Fatura = transacao.Fatura,
                ValorPlanejado = transacao.ValorPlanejado,
                ValorPago = transacao.ValorPago,
                CategoriaId = transacao.CategoriaId,
                BancoId = transacao.BancoId,
                DataVencimento = transacao.DataVencimento,
                DataTransacao = transacao.DataTransacao
            };

            PreencherViewBags();
            return View(vm);
        }

        // POST: /Transacao/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TransacaoViewModel transacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                PreencherViewBags();
                return View(transacaoViewModel);
            }

            var transacao = new TransacaoModel
            {
                Id = transacaoViewModel.Id,
                Descricao = transacaoViewModel.Descricao,
                TipoTransacaoId = transacaoViewModel.TipoTransacaoId,
                Fatura = transacaoViewModel.Fatura,
                ValorPlanejado = transacaoViewModel.ValorPlanejado,
                ValorPago = transacaoViewModel.ValorPago,
                CategoriaId = transacaoViewModel.CategoriaId,
                BancoId = transacaoViewModel.BancoId,
                DataVencimento = transacaoViewModel.DataVencimento,
                DataTransacao = transacaoViewModel.DataTransacao
            };

            _transacaoUseCase.Atualizar(transacao);

            return RedirectToAction("Index");
        }

        // GET: /Transacao/Deletar/{id}
        public IActionResult Deletar(int id)
        {
            var transacao = _transacaoUseCase.BuscarPorId(id);
            if (transacao == null) return NotFound();

            var vm = new TransacaoViewModel
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                TipoTransacaoId = transacao.TipoTransacaoId,
                ValorPago = transacao.ValorPago,
                DataTransacao = transacao.DataTransacao
            };

            return View(vm);
        }

        // POST: /Categoria/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int id)
        {
            var transacao = _transacaoUseCase.BuscarPorId(id);

            if (transacao is null)
                return NotFound();

            _transacaoUseCase.Deletar(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
