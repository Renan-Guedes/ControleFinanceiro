using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface ICategoriaRepository
{
    List<CategoriaModel> Listar();
    
    void Criar(CategoriaModel categoria);
    
    void Atualizar(CategoriaModel categoria);
    
    void Deletar(int id);
}