using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ICategoriaRepository
{
    List<CategoriaModel> Listar();

    CategoriaModel? BuscarPorId(int id);

    void Criar(CategoriaModel categoria);
    
    void Atualizar(CategoriaModel categoria);
    
    void Deletar(int id);
}