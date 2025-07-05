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
            var userId = 1; // Substituir depois com o usuário logado
            
            var planejamentosMensais = _planejamentoMensalUseCase.ListarTodos(userId).ToList();
            
            var viewModel = planejamentosMensais.Select(p => new PlanejamentoMensalViewModel
            {
                Id = p.Id,
                BancoId = p.BancoId,
                BancoNome = p.Banco?.Nome ?? "Banco Não Encontrado" ,
                Ano = p.Ano,
                Mes = p.Mes,
                SaldoInicial = p.SaldoInicial
            }).ToList();

            return View(viewModel);
        }

        // GET: /PlanejamentoMensal/Criar
        public IActionResult Criar()
        {
            PreencherViewBags();
            return View();
        }

        // POST: /PlanejamentoMensal/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(PlanejamentoMensalViewModel planejamentoMensalViewModel)
        {
            try
            {
                ValidarPlanejamento(planejamentoMensalViewModel);

                if (!ModelState.IsValid)
                {
                    PreencherViewBags();
                    return View(planejamentoMensalViewModel);
                }

                var userId = 1; // Substituir depois com o usuário logado

                var planejamentoMensal = new PlanejamentoMensalModel
                {
                    BancoId = planejamentoMensalViewModel.BancoId,
                    UsuarioId = userId,
                    Ano = planejamentoMensalViewModel.Ano,
                    Mes = planejamentoMensalViewModel.Mes,
                    SaldoInicial = planejamentoMensalViewModel.SaldoInicial,
                };

                _planejamentoMensalUseCase.Criar(planejamentoMensal);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags();
                return View(planejamentoMensalViewModel);
            }
        }

        // GET: /PlanejamentoMensal/Editar/{id}
        public IActionResult Editar(int id)
        {
            int userId = 1; // Substituir depois com o usuário logado

            var planejamentoMensal = _planejamentoMensalUseCase.ListarPorId(id, userId);

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

            PreencherViewBags();
            return View(vm);
        }

        // POST: /PlanejamentoMensal/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(PlanejamentoMensalViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return View(vm);
                
                var userId = 1; // Substituir depois com o usuário logado
                
                var planejamentoMensal = new PlanejamentoMensalModel
                {
                    Id = vm.Id,
                    BancoId = vm.BancoId,
                    UsuarioId = userId,
                    Ano = vm.Ano,
                    Mes = vm.Mes,
                    SaldoInicial = vm.SaldoInicial,
                };

                _planejamentoMensalUseCase.Atualizar(planejamentoMensal);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao atualizar Planejamento: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /PlanejamentoMensal/Deletar/{id}
        public IActionResult Deletar(int id)
        {
            var userId = 1; // Substituir depois com o usuário logado

            var planejamentoMensal = _planejamentoMensalUseCase.ListarPorId(id, userId);
            
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
        public IActionResult ConfirmarDeletar(int id)
        {
            var userId = 1; // Substituir depois com o usuário logado

            var planejamentoMensal = _planejamentoMensalUseCase.ListarPorId(id, userId);

            if (planejamentoMensal is null) 
                return NotFound();

            _planejamentoMensalUseCase.Deletar(id, userId);

            return RedirectToAction(nameof(Index));
        }

        private void PreencherViewBags()
        {
            ViewBag.Bancos = _bancoUseCase.ListarTodos().ToList();
        }

        private void ValidarPlanejamento(PlanejamentoMensalViewModel vm)
        {
            if (vm.BancoId == 0)
                ModelState.AddModelError(nameof(vm.BancoId), "Selecione um banco.");

            if (vm.Ano == 0)
                ModelState.AddModelError(nameof(vm.Ano), "Ano é obrigatório.");

            if (vm.Mes < 1 || vm.Mes > 12)
                ModelState.AddModelError(nameof(vm.Mes), "Mês inválido.");
        }
    }
}
