using ControleFinanceiro.Application.Interfaces;
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
    }
}
