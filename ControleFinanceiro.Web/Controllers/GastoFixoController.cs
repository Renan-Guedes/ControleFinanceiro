using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.Service;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.Web.Controllers
{
    public class GastoFixoController : Controller
    {
        private readonly IGastoFixoService _gastoFixoService;
        private readonly IBancoService _bancoService;
        private readonly ICategoriaService _categoriaService;
        private readonly ITransacaoService _transacaoService;

        public GastoFixoController(IGastoFixoService gastoFixoService, IBancoService bancoService, ICategoriaService categoriaService, ITransacaoService transacaoService)
        { 
            _gastoFixoService = gastoFixoService; 
            _bancoService = bancoService;
            _categoriaService = categoriaService;
            _transacaoService = transacaoService;
        }
        

        // GET: /GastoFixo
        public IActionResult Index()
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            var gastosFixos = _gastoFixoService
                .ListarTodos(usuarioId);

            var vm = gastosFixos
                .Select(g => new GastoFixoViewModel
                {
                    Id = g.Id,
                    CategoriaId = g.CategoriaId,
                    CategoriaNome = g.Categoria?.Nome ?? "Categoria não encontrada",
                    TipoTransacaoId = g.TipoTransacaoId,
                    BancoId = g.BancoId,
                    BancoNome = g.Banco?.Nome ?? "Banco não encontrado",
                    UsuarioId = g.UsuarioId,
                    Descricao = g.Descricao ?? string.Empty,
                    Valor = g.Valor,
                    IsPago = _transacaoService.ListarTodos(usuarioId).Any(t => t.GastoFixoId == g.Id && g.DataExclusao == null)
                })
                .ToList();

            PreencherViewBags(usuarioId);
            ViewBag.ContagemGastosFixosAbertos = vm.Count(g => !g.IsPago);

            return View(vm);
        }

        // POST: CriarTransacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(GastoFixoViewModel vm)
        {
            int usuarioId = 1;
            
            if (!ModelState.IsValid)
            {
                PreencherViewBags(usuarioId);
                return View("Index", vm);
            }

            try
            {
                var gastoFixo = new GastoFixoModel
                {
                    CategoriaId = vm.CategoriaId,
                    TipoTransacaoId = vm.TipoTransacaoId,
                    BancoId = vm.BancoId,
                    UsuarioId = usuarioId,
                    Descricao = vm.Descricao,
                    Valor = vm.Valor
                };

                _gastoFixoService.Criar(gastoFixo);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao salvar gasto fixo: " + ex.Message;
                PreencherViewBags(usuarioId);
                return View("Index", vm);
            }
        }

        // POST: Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir([FromBody] int gastoFixoId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var gastoFixo = _gastoFixoService.BuscarPorId(gastoFixoId, usuarioId);
            if (gastoFixo == null)
                return NotFound(new { mensagem = "Conta não encontrada." });

            _gastoFixoService.Deletar(gastoFixoId, usuarioId);
            return Ok(new { mensagem = "Conta excluída com sucesso." });
        }

        private void PreencherViewBags(int usuarioId)
        {
            var bancos = _bancoService.ListarTodos(usuarioId).ToList();
            var categorias = _categoriaService.ListarTodos(usuarioId).ToList();

            ViewBag.Bancos = new SelectList(bancos, "Id", "Nome");
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            ViewBag.TotalGastosFixos = _gastoFixoService.ObterTotalGastosFixos(usuarioId).ToString("C2");
            ViewBag.ContagemGastosFixos = _gastoFixoService.ListarTodos(usuarioId).Count;
            
            ViewBag.TotalGastosFixosEmAberto = _gastoFixoService.ObterContasEmAberto(usuarioId).ToString("C2");
        }
    }
}
