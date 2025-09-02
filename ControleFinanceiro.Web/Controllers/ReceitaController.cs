using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.Service;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Transactions;

namespace ControleFinanceiro.Web.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly ITransacaoService _transacaoService;
        private readonly ICategoriaService _categoriaService;
        private readonly ITipoTransacaoService _tipoTransacaoService;
        private readonly IBancoService _bancoService;

        public ReceitaController(ITransacaoService transacaoService, ICategoriaService categoriaService, ITipoTransacaoService tipoTransacaoService, IBancoService bancoService)
        {
            _transacaoService = transacaoService;
            _categoriaService = categoriaService;
            _tipoTransacaoService = tipoTransacaoService;
            _bancoService = bancoService;
        }


        // GET: /Receita
        public IActionResult Index()
        {
            int usuarioId = 1; // Trocar pelo usuário autenticado

            var receitas = _transacaoService.ListarTodasAsReceitas(usuarioId);

            var vm = receitas.Select(r => new TransacaoViewModel()
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

        //GET: /Receita/Criar
        public IActionResult Criar()
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            PreencherViewBags(usuarioId);
            return View(new TransacaoViewModel());
        }

        // POST: /Receita/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(TransacaoViewModel viewModel)
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            if (ModelState.IsValid)
            {
                var receita = new TransacaoModel
                {
                    CategoriaId = viewModel.CategoriaId,
                    TipoTransacaoId = 1, // Já cadastra como Receita
                    BancoId = viewModel.BancoId,
                    UsuarioId = usuarioId,
                    Descricao = viewModel.Descricao,
                    Fatura = false,
                    ValorPlanejado = viewModel.ValorPlanejado,
                    ValorPago = viewModel.ValorPago,
                    DataTransacao = viewModel.DataTransacao
                };

                _transacaoService.Criar(receita);

                return RedirectToAction(nameof(Index));
            }

            PreencherViewBags(usuarioId);
            return View(viewModel);
        }

        // GET: /Receita/Detalhes/{id}
        public IActionResult Detalhes(int receitaId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var transacao = _transacaoService.BuscarPorId(receitaId, usuarioId);
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

        // GET: /Receita/Editar/5
        public IActionResult Editar(int receitaId)
        {
            int usuarioId = 1; // Usuário autenticado
            var transacao = _transacaoService.BuscarPorId(receitaId, usuarioId);
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

        // POST: /Receita/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(TransacaoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PreencherViewBags(1); // usuário
                return View(vm);
            }

            var receita = new TransacaoModel
            {
                Id = vm.Id,
                UsuarioId = 1, // Alterar para o usuário autenticado
                TipoTransacaoId = 1, // Sempre Receita
                CategoriaId = vm.CategoriaId,
                BancoId = vm.BancoId,
                Descricao = vm.Descricao,
                ValorPlanejado = Math.Round(vm.ValorPlanejado, 2),
                ValorPago = Math.Round(vm.ValorPago ?? 0, 2),
                DataTransacao = vm.DataTransacao
            };

            _transacaoService.Atualizar(receita);

            return RedirectToAction("Index");
        }

        // POST: /Receita/Excluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir([FromBody] int receitaId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var transacao = _transacaoService.BuscarPorId(receitaId, usuarioId);
            if (transacao == null)
                return NotFound(new { mensagem = "Receita não encontrada." });

            _transacaoService.Deletar(receitaId, usuarioId);
            return Ok(new { mensagem = "Receita excluída com sucesso." });
        }

        private void PreencherViewBags(int usuarioId)
        {
            ViewBag.Categorias = new SelectList(
                _categoriaService.ListarTodos(usuarioId).ToList(),
                "Id",
                "Nome"
            );

            ViewBag.TipoTransacao = new SelectList(
                _tipoTransacaoService.ListarTodos().ToList(),
                "Id",
                "Nome"
            );

            ViewBag.Bancos = new SelectList(
                _bancoService.ListarTodos(usuarioId).ToList(),
                "Id",
                "Nome"
            );
        }
    }
}
