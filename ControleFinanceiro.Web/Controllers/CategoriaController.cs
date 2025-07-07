using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaUseCase _categoriaUseCase;

        public CategoriaController(ICategoriaUseCase categoriaUseCase)
            => _categoriaUseCase = categoriaUseCase;
        

        // GET: /Categoria        
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var categorias = _categoriaUseCase
                .ListarTodos(usuarioId)
                .ToList();

            var vm = categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Ativo = c.Ativo,
            }).ToList();

            return View(vm);
        }

        // GET: /Categoria/Criar
        public IActionResult Criar()
            => View();

        // POST: /Categoria/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(CategoriaViewModel vm)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                if (!ModelState.IsValid)
                    RedirectToAction("Index");


                var novaCategoria = new CategoriaModel
                {
                    Nome = vm.Nome,
                    UsuarioId = usuarioId,
                    Ativo = vm.Ativo
                };

                _categoriaUseCase.Criar(novaCategoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /Categoria/Editar/{id}
        public IActionResult Editar(int categoriaId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var categoria = _categoriaUseCase
                .BuscarPorId(categoriaId, usuarioId);

            if (categoria == null)
                return NotFound();
            
            var vm = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
            };

            return View(vm);
        }

        // POST: /Categoria/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(CategoriaViewModel vm)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var categoria = _categoriaUseCase
                    .BuscarPorId(vm.Id, usuarioId);

                if (categoria == null)
                    return NotFound();

                categoria.Nome = vm.Nome;
                categoria.Ativo = vm.Ativo;

                _categoriaUseCase.Atualizar(categoria);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /Categoria/Deletar/{id}
        public IActionResult Deletar(int categoriaId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var categoria = _categoriaUseCase
                .BuscarPorId(categoriaId, usuarioId);

            if (categoria is null)
                return NotFound();

            var vm = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo
            };

            return View(vm);
        }

        // POST: /Categoria/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int categoriaId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                var categoria = _categoriaUseCase
                    .BuscarPorId(categoriaId, usuarioId);

                if (categoria is null)
                    return NotFound();

                _categoriaUseCase.Deletar(categoriaId, usuarioId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View();
            }
        }
    }
}
