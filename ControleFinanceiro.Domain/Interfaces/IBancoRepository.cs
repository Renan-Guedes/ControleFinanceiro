using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface IBancoRepository
{
    List<BancoModel> Listar();
    BancoModel? BuscarPorId(int id);
    void Criar(BancoModel banco);
    void Atualizar(BancoModel banco);
    void Deletar(int id);
}
