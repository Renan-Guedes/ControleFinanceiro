using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCase;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class PlanejamentoMensalController : Controller
    {
        private readonly IPlanejamentoMensalUseCase _planejamentoMensalUseCase;
        private readonly IBancoUseCase _bancoUseCase;

        public PlanejamentoMensalController(IPlanejamentoMensalUseCase planejamentoMensalUseCase, IBancoUseCase bancoUseCase)
        {
            _planejamentoMensalUseCase = planejamentoMensalUseCase;
            _bancoUseCase = bancoUseCase;
        }

        // GET: /PlanejamentoMensal
        public IActionResult Index()
        {
            int usuarioId = 1; // Substituir depois com o usuário logado
            
            var planejamentosMensais = _planejamentoMensalUseCase
                .ListarTodos(usuarioId)
                .ToList();
            
            var vm = planejamentosMensais.Select(p => new PlanejamentoMensalViewModel
            {
                Id = p.Id,
                BancoId = p.BancoId,
                BancoNome = p.Banco?.Nome ?? "Banco Não Encontrado" ,
                Ano = p.Ano,
                Mes = p.Mes,
                SaldoInicial = p.SaldoInicial
            }).ToList();

            return View(vm);
        }

        // GET: /PlanejamentoMensal/Criar
        public IActionResult Criar()
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            PreencherViewBags(usuarioId);
            return View(new PlanejamentoMensalViewModel());
        }

        // POST: /PlanejamentoMensal/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(PlanejamentoMensalViewModel vm)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            try
            {
                if (!ModelState.IsValid)
                {
                    PreencherViewBags(usuarioId);
                    return View(vm);
                }

                var planejamentoMensal = new PlanejamentoMensalModel
                {
                    BancoId = vm.BancoId,
                    UsuarioId = usuarioId,
                    Ano = vm.Ano,
                    Mes = vm.Mes,
                    SaldoInicial = vm.SaldoInicial
                };

                _planejamentoMensalUseCase.Criar(planejamentoMensal);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags(usuarioId);
                return View(vm);
            }
        }

        // GET: /PlanejamentoMensal/Editar/{id}
        public IActionResult Editar(int planejamentoMensalId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            var planejamentoMensal = _planejamentoMensalUseCase
                .BuscarPorId(planejamentoMensalId, usuarioId);

            if (planejamentoMensal == null)
                return NotFound();
            
            var vm = new PlanejamentoMensalViewModel
            {
                Id = planejamentoMensal.Id,
                BancoId = planejamentoMensal.BancoId,
                Ano = planejamentoMensal.Ano,
                Mes = planejamentoMensal.Mes,
                SaldoInicial = planejamentoMensal.SaldoInicial
            };

            PreencherViewBags(usuarioId);
            return View(vm);
        }

        // POST: /PlanejamentoMensal/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(PlanejamentoMensalViewModel vm)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            try
            {
                if (!ModelState.IsValid)
                {
                    PreencherViewBags(usuarioId);
                    return View(vm);
                }

                var planejamentoMensal = _planejamentoMensalUseCase.BuscarPorId(vm.Id, usuarioId);

                if (planejamentoMensal == null)
                    return NotFound();

                planejamentoMensal.BancoId = vm.BancoId;
                planejamentoMensal.Ano = vm.Ano;
                planejamentoMensal.Mes = vm.Mes;
                planejamentoMensal.SaldoInicial = vm.SaldoInicial;

                _planejamentoMensalUseCase.Atualizar(planejamentoMensal);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /PlanejamentoMensal/Deletar/{id}
        public IActionResult Deletar(int planejamentoMensalId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            var planejamentoMensal = _planejamentoMensalUseCase
                .BuscarPorId(planejamentoMensalId, usuarioId);
            
            if (planejamentoMensal == null) 
                return NotFound();

            var vm = new PlanejamentoMensalViewModel
            {
                Id = planejamentoMensal.Id,
                BancoId = planejamentoMensal.BancoId,
                BancoNome = planejamentoMensal.Banco?.Nome ?? "Banco não encontrado",
                Ano = planejamentoMensal.Ano,
                Mes = planejamentoMensal.Mes,
                SaldoInicial = planejamentoMensal.SaldoInicial
            };

            return View(vm);
        }

        // POST: /PlanejamentoMensal/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int planejamentoMensalId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logad

            try
            {
                var planejamentoMensal = _planejamentoMensalUseCase
                    .BuscarPorId(planejamentoMensalId, usuarioId);

                if (planejamentoMensal == null)
                    NotFound();

                _planejamentoMensalUseCase.Deletar(planejamentoMensalId, usuarioId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao excluir: " + ex.Message);
                return View();
            }
        }

        private void PreencherViewBags(int usuarioId)
        {
            ViewBag.Bancos = _bancoUseCase
                .ListarTodos(usuarioId)
                .ToList();
        }
    }
}
