using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public ActionResult Index()
        {
            var categorias = _categoriaUseCase.ListarTodas();

            var viewModel = categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Titulo = c.Titulo,
                Descricao = c.Descricao
            }).ToList();

            return View(viewModel);
        }

        // GET: /Categoria/Criar
        public ActionResult Criar()
        {
            return View();
        }

        // POST: /Categoria/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(CategoriaViewModel categoriaviewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaviewModel);
            }

            var novaCategoria = new Categoria
            {
                Titulo = categoriaviewModel.Titulo,
                Descricao = categoriaviewModel.Descricao
            };

            _categoriaUseCase.Criar(novaCategoria);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Categoria/Editar/5
        public ActionResult Editar(long id)
        {
            var categoria = _categoriaUseCase.PesquisarPorId(id);

            if (categoria == null)
                return NotFound();

            var categoriaEditada = new CategoriaViewModel
            {
                Id = categoria.Id,
                Titulo = categoria.Titulo,
                Descricao = categoria.Descricao
            };
   
            return View(categoriaEditada);
        }

        // POST: /Categoria/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CategoriaViewModel categoriaviewModel)
        {
            if (!ModelState.IsValid)
                return View(categoriaviewModel);

            var categoriaEditada = new Categoria 
            {
                Id = categoriaviewModel.Id,
                Titulo = categoriaviewModel.Titulo,
                Descricao = categoriaviewModel.Descricao
            };

            _categoriaUseCase.Atualizar(categoriaEditada);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Categoria/Deletar/5
        public ActionResult Deletar(long id)
        {
            var categoria = _categoriaUseCase.PesquisarPorId(id);

            if (categoria == null)
                return NotFound();

            var viewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Titulo = categoria.Titulo,
                Descricao = categoria.Descricao
            };

            return View(viewModel);
        }

        // POST: /Categoria/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDelete(long id)
        {
            _categoriaUseCase.Deletar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
