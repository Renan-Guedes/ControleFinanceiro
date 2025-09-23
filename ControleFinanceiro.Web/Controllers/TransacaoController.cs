using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.Web.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IBancoService _bancoService;
        private readonly ICategoriaService _categoriaService;

        public TransacaoController(ITransacaoService transacaoService, IBancoService bancoService, ICategoriaService categoriaService)
        {
            _transacaoService = transacaoService;
            _bancoService = bancoService;
            _categoriaService = categoriaService;
        }

        // GET: /Transacao
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var transacoes = _transacaoService
                .ListarTodos(usuarioId)
                .ToList();

            var vm = transacoes
                .Select(t => new TransacaoViewModel
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
                    DataTransacao = t.DataTransacao,
                    IsPago = _transacaoService.ListarTodos(usuarioId).Any(v => v.Id == t.Id && v.ValorPago.HasValue)
                }).ToList();

            PreencherViewBags(usuarioId);

            return View(vm);
        }

        // POST: /Transacao/Excluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir([FromBody] int transacaoId)
        {
            var usuarioId = 1; // Trocar pelo usuário autenticado

            var transacao = _transacaoService.BuscarPorId(transacaoId, usuarioId);
            if (transacao == null)
                return NotFound();

            _transacaoService.Deletar(transacaoId, usuarioId);
            return Ok();
        }

        private void PreencherViewBags(int usuarioId)
        {
            var bancos = _bancoService.ListarAtivos(usuarioId);
            var categoriasDespesas = _categoriaService.ListarDespesasAtivas(usuarioId);
            var categoriasReceitas = _categoriaService.ListarReceitasAtivas(usuarioId);

            ViewBag.Bancos = new SelectList(bancos, "Id", "Nome");
            ViewBag.CategoriasDespesas = new SelectList(categoriasDespesas, "Id", "Nome");
            ViewBag.CategoriasReceitas = new SelectList(categoriasReceitas, "Id", "Nome");

            ViewBag.TotalReceitas = _transacaoService.ObterTotalReceitas(usuarioId).ToString("C2");
            ViewBag.TotalDespesas = _transacaoService.ObterTotalDespesas(usuarioId).ToString("C2");
        }
    }
}
