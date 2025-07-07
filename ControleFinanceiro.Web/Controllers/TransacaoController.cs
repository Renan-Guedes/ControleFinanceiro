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
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var transacoes = _transacaoUseCase
                .ListarTodos(usuarioId)
                .ToList();

            var vm = transacoes.Select(t => new TransacaoViewModel
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

            return View(vm);
        }

        //GET: /Transacao/Criar
        public IActionResult Criar()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            PreencherViewBags(usuarioId);
            return View(new TransacaoViewModel());
        }

        //POST: /Transacao/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(TransacaoViewModel vm)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                if (!ModelState.IsValid)
                {
                    PreencherViewBags(usuarioId);
                    return View(vm);
                }

                var novaTransacao = new TransacaoModel
                {
                    Descricao = vm.Descricao,
                    TipoTransacaoId = vm.TipoTransacaoId,
                    Fatura = vm.Fatura,
                    ValorPlanejado = vm.ValorPlanejado,
                    ValorPago = vm.ValorPago,
                    CategoriaId = vm.CategoriaId,
                    BancoId = vm.BancoId,
                    UsuarioId = usuarioId,
                    DataVencimento = vm.DataVencimento,
                    DataTransacao = vm.DataTransacao
                };

                _transacaoUseCase.Criar(novaTransacao);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar transação: " + ex.Message);
                PreencherViewBags(usuarioId);
                return View(vm);
            }
        }

        // GET: /Transacao/Editar/{id}
        public IActionResult Editar(int transacaoId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var transacao = _transacaoUseCase
                .BuscarPorId(transacaoId, usuarioId);

            if (transacao == null)
                return NotFound();

            var vm = new TransacaoViewModel
            {
                Id = transacao.Id,
                CategoriaId = transacao.CategoriaId,
                TipoTransacaoId = transacao.TipoTransacaoId,
                BancoId = transacao.BancoId,
                Descricao = transacao.Descricao,
                Fatura = transacao.Fatura,
                ValorPlanejado = transacao.ValorPlanejado,
                ValorPago = transacao.ValorPago,
                DataVencimento = transacao.DataVencimento,
                DataTransacao = transacao.DataTransacao
            };

            PreencherViewBags(usuarioId);
            return View(vm);
        }

        // POST: /Transacao/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TransacaoViewModel transacaoViewModel)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            if (!ModelState.IsValid)
            {
                PreencherViewBags(usuarioId);
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
                UsuarioId = usuarioId,
                DataVencimento = transacaoViewModel.DataVencimento,
                DataTransacao = transacaoViewModel.DataTransacao
            };

            _transacaoUseCase.Atualizar(transacao);

            return RedirectToAction("Index");
        }

        // GET: /Transacao/Deletar/{id}
        public IActionResult Deletar(int transacaoId)
        {
            var usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var transacao = _transacaoUseCase
                .BuscarPorId(transacaoId, usuarioId);

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
        public IActionResult ConfirmarDeletar(int transacaoId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                var transacao = _transacaoUseCase
                    .BuscarPorId(transacaoId, usuarioId);

                if (transacao == null)
                    return NotFound();

                _transacaoUseCase.Deletar(transacaoId, usuarioId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View();
            }
        }

        private void PreencherViewBags(int usuarioId)
        {
            ViewBag.Categorias = _categoriaUseCase
                .ListarTodos(usuarioId)
                .ToList();

            ViewBag.TipoTransacao = _tipoTransacaoUseCase
                .ListarTodos()
                .ToList();

            ViewBag.Bancos = _bancoUseCase
                .ListarTodos(usuarioId)
                .ToList();
        }
    }
}
