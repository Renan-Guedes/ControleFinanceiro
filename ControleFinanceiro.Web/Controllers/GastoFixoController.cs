using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class GastoFixoController : Controller
    {
        private readonly IGastoFixoUseCase _gastoFixoUseCase;

        public GastoFixoController(IGastoFixoUseCase gastoFixoUseCase)
        {
            _gastoFixoUseCase = gastoFixoUseCase;
        }

        // GET: /GastoFixo
        public IActionResult Index()
        {
            return View();
        }

        // GET: /GastoFixo/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: /GastoFixo/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(GastoFixoViewModel gastoFixoViewModel)
        {
            try
            {
                ValidarCampos(gastoFixoViewModel);

                if (!ModelState.IsValid)
                {
                    PreencherViewBags();
                    return View(gastoFixoViewModel);
                }

                var userId = 1; // Substituir depois com o usuário logado

                var gastoFixo = new GastoFixoModel
                {
                    CategoriaId = gastoFixoViewModel.CategoriaId,
                    TipoTransacaoId = gastoFixoViewModel.TipoTransacaoId,
                    BancoId = gastoFixoViewModel.BancoId,
                    UsuarioId = userId,
                    Descricao = gastoFixoViewModel.Descricao,
                    Valor = gastoFixoViewModel.Valor
                };

                _gastoFixoUseCase.Criar(gastoFixo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags();
                return View(gastoFixoViewModel);
            }
        }

        // GET: /GastoFixo/Editar/{id}
        public IActionResult Editar(int id)
        {
            var userId = 1; // Substituir depois com o usuário logado

            var gastoFixo = _gastoFixoUseCase.ListarPorId(id, userId);

            if (gastoFixo == null)
            {
                return NotFound();
            }

            var viewModel = new GastoFixoViewModel
            {
                Id = gastoFixo.Id,
                CategoriaId = gastoFixo.CategoriaId,
                CategoriaNome = gastoFixo.Categoria?.Nome ?? "Categoria não encontrada",
                TipoTransacaoId = gastoFixo.TipoTransacaoId,
                BancoId = gastoFixo.BancoId,
                BancoNome = gastoFixo.Banco?.Nome ?? "Banco não encontrado",
                UsuarioId = gastoFixo.UsuarioId,
                Descricao = gastoFixo.Descricao,
                Valor = gastoFixo.Valor
            };

            PreencherViewBags();
            return View(viewModel);
        }

        // POST: /GastoFixo/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(GastoFixoViewModel gastoFixoViewModel)
        {
            try
            {
                ValidarCampos(gastoFixoViewModel);

                if (!ModelState.IsValid)
                {
                    PreencherViewBags();
                    return View(gastoFixoViewModel);
                }

                var userId = 1; // Substituir depois com o usuário logado

                var gastoFixo = new GastoFixoModel
                {
                    Id = gastoFixoViewModel.Id,
                    CategoriaId = gastoFixoViewModel.CategoriaId,
                    TipoTransacaoId = gastoFixoViewModel.TipoTransacaoId,
                    BancoId = gastoFixoViewModel.BancoId,
                    UsuarioId = userId,
                    Descricao = gastoFixoViewModel.Descricao,
                    Valor = gastoFixoViewModel.Valor
                };

                _gastoFixoUseCase.Atualizar(gastoFixo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags();
                return View(gastoFixoViewModel);
            }
        }

        // GET: /GastoFixo/Deletar/{id}
        public IActionResult Deletar(int id)
        {
            var userId = 1; // Substituir depois com o usuário logado
            
            var gastoFixo = _gastoFixoUseCase.ListarPorId(id, userId);
            
            if (gastoFixo == null)
            {
                return NotFound();
            }
            
            var viewModel = new GastoFixoViewModel
            {
                Id = gastoFixo.Id,
                CategoriaId = gastoFixo.CategoriaId,
                CategoriaNome = gastoFixo.Categoria?.Nome ?? "Categoria não encontrada",
                TipoTransacaoId = gastoFixo.TipoTransacaoId,
                BancoId = gastoFixo.BancoId,
                BancoNome = gastoFixo.Banco?.Nome ?? "Banco não encontrado",
                UsuarioId = gastoFixo.UsuarioId,
                Descricao = gastoFixo.Descricao,
                Valor = gastoFixo.Valor
            };
            
            PreencherViewBags();
            return View(viewModel);
        }

        // POST: /GastoFixo/Deletar
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int id)
        {
            try
            {
                var userId = 1; // Substituir depois com o usuário logado
                
                _gastoFixoUseCase.Deletar(id, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao deletar: " + ex.Message);
                return View();
            }
        }

        private void PreencherViewBags()
        {
            // Aqui você pode preencher os ViewBags necessários para a view
            // Exemplo: ViewBag.Categorias = _categoriaService.ListarTodas();
            // Exemplo: ViewBag.Bancos = _bancoService.ListarTodos();
        }

        private void ValidarCampos(GastoFixoViewModel gastoFixoViewModel)
        {
            
        }
    }
}
