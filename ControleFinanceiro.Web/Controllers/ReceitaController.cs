using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly ITransacaoService _transacaoService;

        public ReceitaController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // POST: /Receita/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(TransacaoViewModel vm)
        {
            int usuarioId = 1; // Substituir pelo usuário autenticado

            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }


            var novaReceita = new TransacaoModel
            {
                CategoriaId = vm.CategoriaId,
                TipoTransacaoId = 1, // Sempre Receita
                BancoId = vm.BancoId,
                UsuarioId = usuarioId,
                Descricao = vm.Descricao,
                ValorPlanejado = vm.ValorPlanejado,
                ValorPago = vm.ValorPago,
                DataTransacao = vm.DataTransacao
            };

            _transacaoService.Criar(novaReceita);

            return RedirectToAction("Index", "Transacao");
        }
    }
}
