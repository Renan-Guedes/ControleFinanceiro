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
        {
            _categoriaUseCase = categoriaUseCase;
        }

        // GET: /Categoria        
        public IActionResult Index()
        {
            var categorias = _categoriaUseCase.ListarTodos().ToList();

            var viewModel = categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Ativo = c.Ativo,
                DataExclusao = c.DataExclusao
            }).ToList();

            return View(viewModel);
        }

        // GET: /Categoria/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: /Categoria/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var novaCategoria = new CategoriaModel
            {
                Nome = categoriaViewModel.Nome,
                Ativo = categoriaViewModel.Ativo
            };

            _categoriaUseCase.Criar(novaCategoria);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Categoria/Editar/{id}
        public IActionResult Editar(int id)
        {
            var categoria = _categoriaUseCase.ListarTodos().FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            var viewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                DataExclusao = categoria.DataExclusao
            };

            return View(viewModel);
        }

        // POST: /Categoria/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaViewModel);
            }

            var categoria = _categoriaUseCase.ListarTodos().FirstOrDefault(c => c.Id == categoriaViewModel.Id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Nome = categoriaViewModel.Nome;
            categoria.Ativo = categoriaViewModel.Ativo;

            _categoriaUseCase.Atualizar(categoria);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Categoria/Deletar/{id}
        public IActionResult Deletar(int id)
        {
            var categoria = _categoriaUseCase.ListarTodos().FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            var viewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                DataExclusao = categoria.DataExclusao
            };

            return View(viewModel);
        }

        // POST: /Categoria/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDelete(int id)
        {
            var categoria = _categoriaUseCase.ListarTodos().FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            _categoriaUseCase.Deletar(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
