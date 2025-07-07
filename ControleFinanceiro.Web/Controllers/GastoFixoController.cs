using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCase;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class GastoFixoController : Controller
    {
        private readonly IGastoFixoUseCase _gastoFixoUseCase;
        private readonly IBancoUseCase _bancoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;

        public GastoFixoController(IGastoFixoUseCase gastoFixoUseCase, IBancoUseCase bancoUseCase, ICategoriaUseCase categoriaUseCase)
        { 
            _gastoFixoUseCase = gastoFixoUseCase; 
            _bancoUseCase = bancoUseCase;
            _categoriaUseCase = categoriaUseCase;
        }
        

        // GET: /GastoFixo
        public IActionResult Index()
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            var gastosFixos = _gastoFixoUseCase
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
                    Valor = g.Valor
                })
                .ToList();

            return View(vm);
        }
        
        // GET: /GastoFixo/Criar
        public IActionResult Criar()
        {
            var usuarioId = 1; // Substituir depois com o usuário logado
            PreencherViewBags(usuarioId);

            return View(new GastoFixoViewModel());
        }

        // POST: /GastoFixo/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(GastoFixoViewModel vm)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            try
            {
                ValidarCampos(vm);

                if (!ModelState.IsValid)
                {
                    PreencherViewBags(usuarioId);
                    return View(vm);
                }

                var novoGastoFixo = new GastoFixoModel
                {
                    CategoriaId = vm.CategoriaId,
                    TipoTransacaoId = vm.TipoTransacaoId,
                    BancoId = vm.BancoId,
                    UsuarioId = usuarioId,
                    Descricao = vm.Descricao,
                    Valor = vm.Valor
                };

                _gastoFixoUseCase.Criar(novoGastoFixo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags(usuarioId);
                return View(vm);
            }
        }

        // GET: /GastoFixo/Editar/{id}
        public IActionResult Editar(int gastoFixoId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            var gastoFixo = _gastoFixoUseCase
                .BuscarPorId(gastoFixoId, usuarioId);

            if (gastoFixo == null)
            {
                return NotFound();
            }

            var vm = new GastoFixoViewModel
            {
                Id = gastoFixo.Id,
                CategoriaId = gastoFixo.CategoriaId,
                CategoriaNome = gastoFixo.Categoria?.Nome ?? "Categoria não encontrada",
                TipoTransacaoId = gastoFixo.TipoTransacaoId,
                BancoId = gastoFixo.BancoId,
                BancoNome = gastoFixo.Banco?.Nome ?? "Banco não encontrado",
                UsuarioId = gastoFixo.UsuarioId,
                Descricao = gastoFixo.Descricao ?? string.Empty,
                Valor = gastoFixo.Valor
            };

            PreencherViewBags(usuarioId);
            return View(vm);
        }

        // POST: /GastoFixo/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(GastoFixoViewModel vm)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            try
            {
                ValidarCampos(vm);

                if (!ModelState.IsValid)
                {
                    PreencherViewBags(usuarioId);
                    return View(vm);
                }

                var gastoFixo = new GastoFixoModel
                {
                    Id = vm.Id,
                    CategoriaId = vm.CategoriaId,
                    TipoTransacaoId = vm.TipoTransacaoId,
                    BancoId = vm.BancoId,
                    UsuarioId = usuarioId,
                    Descricao = vm.Descricao,
                    Valor = vm.Valor
                };

                _gastoFixoUseCase.Atualizar(gastoFixo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                PreencherViewBags(usuarioId);
                return View(vm);
            }
        }

        // GET: /GastoFixo/Deletar/{id}
        public IActionResult Deletar(int gastoFixoId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado
            
            var gastoFixo = _gastoFixoUseCase
                    .BuscarPorId(gastoFixoId, usuarioId);
            
            if (gastoFixo == null)
                return NotFound();
            
            
            var vm = new GastoFixoViewModel
            {
                Id = gastoFixo.Id,
                CategoriaId = gastoFixo.CategoriaId,
                CategoriaNome = gastoFixo.Categoria?.Nome ?? "Categoria não encontrada",
                TipoTransacaoId = gastoFixo.TipoTransacaoId,
                BancoId = gastoFixo.BancoId,
                BancoNome = gastoFixo.Banco?.Nome ?? "Banco não encontrado",
                UsuarioId = gastoFixo.UsuarioId,
                Descricao = gastoFixo.Descricao ?? string.Empty,
                Valor = gastoFixo.Valor
            };
            
            PreencherViewBags(usuarioId);
            return View(vm);
        }

        // POST: /GastoFixo/Deletar
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int gastoFixoId)
        {
            int usuarioId = 1; // Substituir depois com o usuário logado

            try
            {
                var gastoFixo = _gastoFixoUseCase
                    .BuscarPorId(gastoFixoId, usuarioId);

                if (gastoFixo == null)
                    return NotFound();

                _gastoFixoUseCase.Deletar(gastoFixoId, usuarioId);

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

            ViewBag.Categorias = _categoriaUseCase
                .ListarTodos(usuarioId)
                .ToList();
        }

        private void ValidarCampos(GastoFixoViewModel vm)
        {
            
        }
    }
}
