using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ICategoriaRepository
{
    List<CategoriaModel> Listar();
}