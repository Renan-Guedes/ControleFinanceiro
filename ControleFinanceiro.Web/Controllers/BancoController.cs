using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCase;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Web.Controllers
{
    public class BancoController : Controller
    {
        private readonly IBancoUseCase _bancoUseCase;

        public BancoController(IBancoUseCase bancoUseCase)
        {
            _bancoUseCase = bancoUseCase;
        }

        // GET: /Banco
        public IActionResult Index()
        {
            var bancos = _bancoUseCase.ListarTodos().ToList();

            var viewModel = bancos.Select(b => new BancoViewModel
            {
                Id = b.Id,
                Nome = b.Nome,
                Ativo = b.Ativo
            }).ToList();

            return View(viewModel);
        }

        // GET: /Banco/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: /Banco/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(BancoViewModel bancoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bancoViewModel);
            }
            
            var novoBanco = new BancoModel
            {
                Nome = bancoViewModel.Nome,
                Ativo = bancoViewModel.Ativo
            };
            
            _bancoUseCase.Criar(novoBanco);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Banco/Editar/{id}
        public IActionResult Editar(int id)
        {
            var banco = _bancoUseCase.BuscarPorId(id);
            
            if (banco == null)
            {
                return NotFound();
            }
            
            var viewModel = new BancoViewModel
            {
                Id = banco.Id,
                Nome = banco.Nome,
                Ativo = banco.Ativo
            };

            return View(viewModel);
        }

        // POST: /Banco/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(BancoViewModel bancoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bancoViewModel);
            }
            
            var bancoExistente = _bancoUseCase.BuscarPorId(bancoViewModel.Id);
            
            if (bancoExistente == null)
            {
                return NotFound();
            }
            
            bancoExistente.Nome = bancoViewModel.Nome;
            bancoExistente.Ativo = bancoViewModel.Ativo;
            
            _bancoUseCase.Atualizar(bancoExistente);
            
            return RedirectToAction(nameof(Index));
        }

        // GET: /Banco/Deletar/{id}
        public IActionResult Deletar(int id)
        {
            var banco = _bancoUseCase.BuscarPorId(id);
            
            if (banco == null)
            {
                return NotFound();
            }
            
            var viewModel = new BancoViewModel
            {
                Id = banco.Id,
                Nome = banco.Nome,
                Ativo = banco.Ativo
            };
            return View(viewModel);
        }

        // POST: /Categoria/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int id)
        {
            var banco = _bancoUseCase.BuscarPorId(id);

            if (banco is null)
                return NotFound();

            _bancoUseCase.Deletar(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
