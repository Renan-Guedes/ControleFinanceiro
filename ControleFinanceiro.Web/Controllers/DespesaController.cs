using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.Web.Controllers
{
    public class DespesaController : Controller
    {
        private readonly ITransacaoUseCase _transacaoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;
        private readonly ITipoTransacaoUseCase _tipoTransacaoUseCase;
        private readonly IBancoUseCase _bancoUseCase;

        public DespesaController(ITransacaoUseCase transacaoUseCase, ICategoriaUseCase categoriaUseCase, ITipoTransacaoUseCase tipoTransacaoUseCase, IBancoUseCase bancoUseCase)
        {
            _transacaoUseCase = transacaoUseCase;
            _categoriaUseCase = categoriaUseCase;
            _tipoTransacaoUseCase = tipoTransacaoUseCase;
            _bancoUseCase = bancoUseCase;
        }


        // GET: /Despesa
        public IActionResult Index()
        {
            int usuarioId = 1; // Trocar pelo usuário autenticado

            var despesas = _transacaoUseCase.ListarTodasAsDespesas(usuarioId);

            var vm = despesas.Select(r => new TransacaoViewModel()
            {
                Id = r.Id,
                BancoNome = r.Banco?.Nome ?? "Banco não encontrado",
                CategoriaNome = r.Categoria?.Nome ?? "Categoria não encontrada",
                DataTransacao = r.DataTransacao,
                DataVencimento = r.DataVencimento,
                Descricao = r.Descricao ?? string.Empty,
                TipoTransacaoNome = r.TipoTransacao?.Nome ?? "Tipo não encontrado",
                ValorPago = r.ValorPago,
                ValorPlanejado = r.ValorPlanejado
            }).ToList();

            return View(vm);
        }

        //GET: /Despesa/Criar
        public IActionResult Criar()
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            PreencherViewBags(usuarioId);
            return View(new TransacaoViewModel());
        }

        // POST: /Despesa/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(TransacaoViewModel viewModel)
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            if (ModelState.IsValid)
            {
                var despesa = new TransacaoModel
                {
                    CategoriaId = viewModel.CategoriaId,
                    TipoTransacaoId = 2, // Sempre Despesa
                    BancoId = viewModel.BancoId,
                    UsuarioId = usuarioId,
                    Descricao = viewModel.Descricao,
                    Fatura = viewModel.Fatura,
                    DataVencimento = viewModel.DataVencimento,
                    ValorPlanejado = viewModel.ValorPlanejado,
                    ValorPago = viewModel.ValorPago,
                    DataTransacao = viewModel.DataTransacao
                };

                _transacaoUseCase.Criar(despesa);

                return RedirectToAction(nameof(Index));
            }

            PreencherViewBags(usuarioId);
            return View(viewModel);
        }

        // GET: /Despesa/Detalhes/{despesaId}
        public IActionResult Detalhes(int despesaId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var transacao = _transacaoUseCase.BuscarPorId(despesaId, usuarioId);
            if (transacao == null) return NotFound();

            var vm = new TransacaoViewModel
            {
                Id = transacao.Id,
                BancoNome = transacao.Banco?.Nome ?? "Banco não encontrado",
                CategoriaNome = transacao.Categoria?.Nome ?? "Categoria não encontrada",
                Descricao = transacao.Descricao,
                ValorPlanejado = Math.Round(transacao.ValorPlanejado, 2),
                ValorPago = Math.Round(transacao.ValorPago ?? 0, 2),
                DataTransacao = transacao.DataTransacao,
                DataVencimento = transacao.DataVencimento
            };

            return View(vm);
        }

        // GET: /Despesa/Editar/{despesaId}
        public IActionResult Editar(int despesaId)
        {
            int usuarioId = 1; // Usuário autenticado
            var transacao = _transacaoUseCase.BuscarPorId(despesaId, usuarioId);
            if (transacao == null) return NotFound();

            var vm = new TransacaoViewModel
            {
                Id = transacao.Id,
                CategoriaId = transacao.CategoriaId,
                BancoId = transacao.BancoId,
                TipoTransacaoId = transacao.TipoTransacaoId,
                Descricao = transacao.Descricao,
                ValorPlanejado = Math.Round(transacao.ValorPlanejado, 2),
                ValorPago = Math.Round(transacao.ValorPago ?? 0, 2),
                DataTransacao = transacao.DataTransacao,
                DataVencimento = transacao.DataVencimento
            };

            PreencherViewBags(usuarioId);
            return View(vm);
        }

        // POST: /Despesa/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TransacaoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PreencherViewBags(1); // usuário
                return View(vm);
            }

            var despesa = new TransacaoModel
            {
                Id = vm.Id,
                UsuarioId = 1, // Alterar para o usuário autenticado
                TipoTransacaoId = 2, // Sempre Despesa
                CategoriaId = vm.CategoriaId,
                BancoId = vm.BancoId,
                Descricao = vm.Descricao,
                ValorPlanejado = Math.Round(vm.ValorPlanejado, 2),
                ValorPago = Math.Round(vm.ValorPago ?? 0, 2),
                DataTransacao = vm.DataTransacao
            };

            _transacaoUseCase.Atualizar(despesa);

            return RedirectToAction("Index");
        }

        // POST: /Despesa/Excluir/{despesaId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir([FromBody] int despesaId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var transacao = _transacaoUseCase.BuscarPorId(despesaId, usuarioId);
            if (transacao == null)
                return NotFound(new { mensagem = "Despesa não encontrada." });

            _transacaoUseCase.Deletar(despesaId, usuarioId);
            return Ok(new { mensagem = "Despesa excluída com sucesso." });
        }

        private void PreencherViewBags(int usuarioId)
        {
            ViewBag.Categorias = new SelectList(
                _categoriaUseCase.ListarTodos(usuarioId).ToList(),
                "Id",
                "Nome"
            );

            ViewBag.TipoTransacao = new SelectList(
                _tipoTransacaoUseCase.ListarTodos().ToList(),
                "Id",
                "Nome"
            );

            ViewBag.Bancos = new SelectList(
                _bancoUseCase.ListarTodos(usuarioId).ToList(),
                "Id",
                "Nome"
            );
        }
    }
}
