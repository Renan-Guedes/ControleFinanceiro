using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // O DbSet representa todas as tabelas do banco de dados em sistema
    public DbSet<CategoriaModel> Categorias { get; set; }
        
    public DbSet<TransacaoModel> Transacoes { get; set; }

    public DbSet<TipoTransacaoModel> TipoTransacoes { get; set; }
}