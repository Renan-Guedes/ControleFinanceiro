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

        // POST: RealizarPagamento
        // DTO genérico para pagamento/recebimento
        public class TransacaoRequest
        {
            public int Id { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataTransacao { get; set; }
        }

        // POST: Pagamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RealizarPagamento([FromBody] TransacaoRequest request)
        {
            return SalvarTransacao(request, "Pago");
        }

        // POST: Recebimento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RealizarRecebimento([FromBody] TransacaoRequest request)
        {
            return SalvarTransacao(request, "Recebido");
        }

        private IActionResult SalvarTransacao(TransacaoRequest request, string label)
        {
            var usuarioId = 1; // depois pegar o usuário logado
            var gasto = _gastoFixoService.BuscarPorId(request.Id, usuarioId);

            if (gasto == null)
                return NotFound(new { mensagem = "Gasto fixo não encontrado." });

            try
            {
                var transacao = new TransacaoModel
                {
                    CategoriaId = gasto.CategoriaId,
                    TipoTransacaoId = gasto.TipoTransacaoId,
                    BancoId = gasto.BancoId,
                    UsuarioId = usuarioId,
                    GastoFixoId = gasto.Id,
                    Descricao = gasto.Descricao ?? "",
                    ValorPlanejado = request.Valor,
                    ValorPago = request.Valor,
                    DataTransacao = request.DataTransacao,
                };

                _transacaoService.Criar(transacao);

                // Atualizar cards
                var total = _gastoFixoService.ObterTotalGastosFixos(usuarioId).ToString("C2");
                var totalAberto = _gastoFixoService.ObterContasEmAberto(usuarioId).ToString("C2");
                var count = _gastoFixoService.ListarTodos(usuarioId).Count;
                var countAbertos = _gastoFixoService.ListarTodos(usuarioId)
                    .Count(g => !_transacaoService.ListarTodos(usuarioId).Any(t => t.GastoFixoId == g.Id));

                return Ok(new
                {
                    mensagem = $"{label} registrado com sucesso!",
                    gastoFixoId = gasto.Id,
                    statusLabel = label,
                    total,
                    totalAberto,
                    count,
                    countAbertos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao registrar transação: " + ex.Message });
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
