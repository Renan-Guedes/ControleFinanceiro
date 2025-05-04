using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Repositories;

public interface IUsuarioRepository : IDisposable
{
    List<Usuario> Listar();
    Usuario ListarPorId(int id);
    void Criar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Deletar(int id);
}