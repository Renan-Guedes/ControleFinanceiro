using ControleFinanceiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.Interfaces;

public interface ICategoriaUseCase
{
    List<Categoria> ListarTodas();
    List<Categoria> ListarPorNome(string categoria);
    void Criar(Categoria categoria);
    void Atualizar(Categoria categoria);
    void Deletar(int id);
}
