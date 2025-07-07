using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCase;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ControleFinanceiro.Web.Controllers
{
    public class BancoController : Controller
    {
        private readonly IBancoUseCase _bancoUseCase;

        public BancoController(IBancoUseCase bancoUseCase)
            => _bancoUseCase = bancoUseCase;


        // GET: /Banco
        public IActionResult Index()
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var bancos = _bancoUseCase
                .ListarTodos(usuarioId)
                .ToList();

            var vm = bancos.Select(b => new BancoViewModel
            {
                Id = b.Id,
                UsuarioId = usuarioId,
                Nome = b.Nome,
                Ativo = b.Ativo
            }).ToList();

            return View(vm);
        }

        // GET: /Banco/Criar
        public IActionResult Criar()
        {
            var vm = new BancoViewModel();
            return View(vm);
        }

        // POST: /Banco/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(BancoViewModel vm)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var novoBanco = new BancoModel
                {
                    UsuarioId = usuarioId,
                    Nome = vm.Nome,
                    Ativo = vm.Ativo
                };

                _bancoUseCase.Criar(novoBanco);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /Banco/Editar/{id}
        public IActionResult Editar(int bancoId)
        {
            int usuarioid = 1; // Substitua pelo ID do usuário autenticado

            var banco = _bancoUseCase
                .BuscarPorId(bancoId, usuarioid);

            if (banco == null)
                return NotFound();

            var vm = new BancoViewModel
            {
                Id = banco.Id,
                Nome = banco.Nome,
                Ativo = banco.Ativo
            };

            return View(vm);
        }

        // POST: /Banco/Editar/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(BancoViewModel vm)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                if (!ModelState.IsValid)
                    return View(vm);

                var bancoExistente = _bancoUseCase
                    .BuscarPorId(vm.Id, usuarioId);

                if (bancoExistente == null)
                    return NotFound();

                bancoExistente.Nome = vm.Nome;
                bancoExistente.Ativo = vm.Ativo;
                _bancoUseCase.Atualizar(bancoExistente);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                return View(vm);
            }
        }

        // GET: /Banco/Deletar/{id}
        public IActionResult Deletar(int bancoId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            var banco = _bancoUseCase
                .BuscarPorId(bancoId, usuarioId);

            if (banco == null)
                return NotFound();

            var vm = new BancoViewModel
            {
                Id = banco.Id,
                UsuarioId = banco.UsuarioId,
                Nome = banco.Nome,
                Ativo = banco.Ativo
            };

            return View(vm);
        }

        // POST: /Categoria/Deletar/{id}
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarDeletar(int bancoId)
        {
            int usuarioId = 1; // Substitua pelo ID do usuário autenticado

            try
            {
                var banco = _bancoUseCase
                    .BuscarPorId(bancoId, usuarioId);

                if (banco == null)
                    return NotFound();

                _bancoUseCase.Deletar(bancoId, usuarioId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao excluir: " + ex.Message);
                return View();
            }
        }
    }
}
