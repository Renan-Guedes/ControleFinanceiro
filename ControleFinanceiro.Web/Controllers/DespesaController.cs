using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class DespesaController : Controller
    {
        private readonly ITransacaoService _transacaoService;

        public DespesaController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // POST: /Despesa/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(TransacaoViewModel vm)
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }


            var novaDespesa = new TransacaoModel
            {
                CategoriaId = vm.CategoriaId,
                TipoTransacaoId = 2, // Sempre Despesa
                BancoId = vm.BancoId,
                UsuarioId = usuarioId,
                Descricao = vm.Descricao,
                Fatura = vm.Fatura,
                DataVencimento = vm.DataVencimento,
                ValorPlanejado = vm.ValorPlanejado,
                ValorPago = vm.ValorPago,
                DataTransacao = vm.DataTransacao
            };

            _transacaoService.Criar(novaDespesa);

            return RedirectToAction("Index", "Transacao");
        }
    }
}
