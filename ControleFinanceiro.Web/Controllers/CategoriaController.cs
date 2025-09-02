using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
            => _categoriaService = categoriaService;
        

        // GET: /Categoria        
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var categorias = _categoriaService.ListarTodos(usuarioId);

            var vm = categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                TipoTransacaoId = c.TipoTransacaoId,
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
                    Ativo = true, // Sempre cria ativo
                    TipoTransacaoId = vm.TipoTransacaoId
                };

                _categoriaService.Criar(novaCategoria);
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

            var categoria = _categoriaService
                .BuscarPorId(categoriaId, usuarioId);

            if (categoria == null)
                return NotFound();

            var vm = new CategoriaViewModel
            {
                Id = categoria.Id,
                TipoTransacaoId = categoria.TipoTransacaoId,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo
            };
            
            ViewBag.Ativos = new SelectList(new[]
            {
                new { Value = true, Text = "Ativo" },
                new { Value = false, Text = "Inativo" }
            }, "Value", "Text", categoria.Ativo);

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

                var categoria = _categoriaService
                    .BuscarPorId(vm.Id, usuarioId);

                if (categoria == null)
                    return NotFound();

                categoria.Nome = vm.Nome;
                categoria.Ativo = vm.Ativo;
                categoria.TipoTransacaoId = vm.TipoTransacaoId;

                _categoriaService.Atualizar(categoria);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir([FromBody] int categoriaId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var categoria = _categoriaService.BuscarPorId(categoriaId, usuarioId);

            if (categoria == null) 
                return NotFound(new { mensagem = "Categoria não encontrada." });

            _categoriaService.Deletar(categoriaId, usuarioId);
            return Ok(new { mensagem = "Categoria excluída com sucesso!" });
        }
    }
}
