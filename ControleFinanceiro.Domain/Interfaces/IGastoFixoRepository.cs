using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Domain.Interfaces;

public interface IGastoFixoRepository
{
    List<GastoFixoModel> ListarTodos(int usuarioId);
    
    GastoFixoModel? BuscarPorId(int gastoFixoId, int usuarioId);
    
    void Criar(GastoFixoModel gastoFixoModel);
    
    void Atualizar(GastoFixoModel gastoFixoModel);
    
    void Deletar(int gastoFixoId, int usuarioId);
}