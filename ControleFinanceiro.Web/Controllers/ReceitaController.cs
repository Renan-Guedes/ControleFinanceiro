using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCase;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Web.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly ITransacaoUseCase _transacaoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;
        private readonly ITipoTransacaoUseCase _tipoTransacaoUseCase;
        private readonly IBancoUseCase _bancoUseCase;

        public ReceitaController(ITransacaoUseCase transacaoUseCase, ICategoriaUseCase categoriaUseCase, ITipoTransacaoUseCase tipoTransacaoUseCase, IBancoUseCase bancoUseCase)
        {
            _transacaoUseCase = transacaoUseCase;
            _categoriaUseCase = categoriaUseCase;
            _tipoTransacaoUseCase = tipoTransacaoUseCase;
            _bancoUseCase = bancoUseCase;
        }


        // GET: /Receita
        public IActionResult Index()
        {
            int usuarioId = 1; // Trocar pelo usuário autenticado

            var receitas = _transacaoUseCase.ListarTodasAsReceitas(usuarioId);

            var vm = receitas.Select(r => new TransacaoViewModel()
            {
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

        // POST: Receitas/Criar
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

                _transacaoUseCase.Criar(receita);

                return RedirectToAction(nameof(Index));
            }

            // Se cair aqui, houve erro de validação -> precisa repopular combos
            PreencherViewBags(usuarioId);
            return View(viewModel);
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
