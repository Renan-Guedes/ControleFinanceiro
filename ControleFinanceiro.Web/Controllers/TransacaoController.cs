using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleFinanceiro.Web.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ITransacaoUseCase _transacaoUseCase;
        private readonly ICategoriaUseCase _categoriaUseCase;

        public TransacaoController(ITransacaoUseCase transacaoUseCase, ICategoriaUseCase categoriaUseCase)
        {
            _transacaoUseCase = transacaoUseCase;
            _categoriaUseCase = categoriaUseCase;
        }

        // GET: /transacao
        public ActionResult Index()
        {
            var transacoes = _transacaoUseCase.ListarTodas();

            var viewModel = transacoes.Select(t => new TransacaoViewModel
            {
                Id = t.Id,
                Titulo = t.Titulo,
                PagoOuRecebidoEm = t.PagoOuRecebidoEm,
                Tipo = t.Tipo,
                Valor = t.Valor,
                CategoriaId = t.CategoriaId,
                CategoriaNome = t.Categoria.Titulo,
                DataCriacao = t.DataCriacao
            }).ToList();

            return View(viewModel);
        }

        // GET: /Categoria/Criar
        public ActionResult Criar()
        {
            var viewModel = new TransacaoViewModel
            {
                Categorias = _categoriaUseCase
                    .ListarTodas()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Titulo
                    }).ToList()
            };

            return View(viewModel);
        }

        // POST: /transacao/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(TransacaoViewModel transacaoViewModel)
        {
            var novaTransacao = new Transacao
            {
                Id = transacaoViewModel.Id,
                Titulo = transacaoViewModel.Titulo,
                PagoOuRecebidoEm = transacaoViewModel.PagoOuRecebidoEm,
                Tipo = transacaoViewModel.Tipo,
                Valor = transacaoViewModel.Valor,
                CategoriaId = transacaoViewModel.CategoriaId,
                DataCriacao = transacaoViewModel.DataCriacao
            };

            _transacaoUseCase.Criar(novaTransacao);
            return RedirectToAction(nameof(Index));
        }

        // GET: /transacao/Editar/5
        public ActionResult Editar(long id)
        {
            var transacao = _transacaoUseCase.PesquisarPorId(id);

            if (transacao == null)
                return NotFound();

            var viewModel = new TransacaoViewModel
            {
                Id = transacao.Id,
                Titulo = transacao.Titulo,
                PagoOuRecebidoEm = transacao.PagoOuRecebidoEm,
                Tipo = transacao.Tipo,
                Valor = transacao.Valor,
                CategoriaId = transacao.CategoriaId,
                DataCriacao = transacao.DataCriacao,
                Categorias = _categoriaUseCase
                    .ListarTodas()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Titulo
                    }).ToList()
            };

            return View(viewModel);
        }

        // POST: /transacao/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(TransacaoViewModel transacaoViewModel)
        {
            if (!ModelState.IsValid)
                return View(transacaoViewModel);

            var transacaoEditada = new Transacao
            {
                Id = transacaoViewModel.Id,
                Titulo = transacaoViewModel.Titulo,
                PagoOuRecebidoEm = transacaoViewModel.PagoOuRecebidoEm,
                Tipo = transacaoViewModel.Tipo,
                Valor = transacaoViewModel.Valor,
                CategoriaId = transacaoViewModel.CategoriaId,
            };

            _transacaoUseCase.Atualizar(transacaoEditada);
            return RedirectToAction(nameof(Index));
        }

        // GET: /transacao/Deletar/5
        public ActionResult Deletar(long id)
        {
            var transacao = _transacaoUseCase.PesquisarPorId(id);

            if (transacao == null)
                return NotFound();

            var viewModel = new TransacaoViewModel
            {
                Id = transacao.Id,
                Titulo = transacao.Titulo,
                PagoOuRecebidoEm = transacao.PagoOuRecebidoEm,
                Tipo = transacao.Tipo,
                Valor = transacao.Valor,
                CategoriaId = transacao.CategoriaId,
                DataCriacao = transacao.DataCriacao
            };

            return View(viewModel);
        }

        // POST: /transacao/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDelete(long id)
        {
            _transacaoUseCase.Deletar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
