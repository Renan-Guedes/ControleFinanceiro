using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Web.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControleFinanceiro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IGastoFixoService _gastoFixoService;

        public HomeController(ITransacaoService transacaoService, IGastoFixoService gastoFixoService)
        {
            _transacaoService = transacaoService;
            _gastoFixoService = gastoFixoService;
        }

        // GET: /Home
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var totalReceitas = _transacaoService.ObterTotalReceitas(usuarioId);
            var totalDespesas = _transacaoService.ObterTotalDespesas(usuarioId);
            var saldoAtual = totalReceitas - totalDespesas;
            var totalGastosFixos = _gastoFixoService.ObterTotalGastosFixos(usuarioId);
            var ultimasTransacoes = _transacaoService
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
