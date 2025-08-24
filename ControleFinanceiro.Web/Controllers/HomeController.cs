using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Web.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControleFinanceiro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransacaoUseCase _transacaoUseCase;
        private readonly IGastoFixoUseCase _gastoFixoUseCase;

        public HomeController(ITransacaoUseCase transacaoUseCase, IGastoFixoUseCase gastoFixoUseCase)
        {
            _transacaoUseCase = transacaoUseCase;
            _gastoFixoUseCase = gastoFixoUseCase;
        }

        // GET: /Home
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var totalReceitas = _transacaoUseCase.ObterTotalReceitas(usuarioId);
            var totalDespesas = _transacaoUseCase.ObterTotalDespesas(usuarioId);
            var saldoAtual = totalReceitas - totalDespesas;
            var totalGastosFixos = _gastoFixoUseCase.ObterTotalGastosFixos(usuarioId);
            var ultimasTransacoes = _transacaoUseCase
                                    .ListarTodos(usuarioId)
                                    .OrderByDescending(t => t.DataTransacao)
                                    .Take(5)
                                    .ToList();

            var ultimasTransacoesViewModel = ultimasTransacoes
            .Select(t => new TransacaoViewModel
            {
                Id = t.Id,
                CategoriaNome = t.Categoria.Nome,
                TipoTransacaoNome = t.TipoTransacao.Nome,
                ValorPago = t.ValorPago,
                DataTransacao = t.DataTransacao
            })
            .ToList();

            var vm = new HomeViewModel
            {
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoAtual = saldoAtual,
                TotalGastosFixos = totalGastosFixos,
                UltimasTransacoes = ultimasTransacoesViewModel
            };

            return View(vm);
        }
    }
}
