using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // O DbSet representa todas as tabelas do banco de dados em sistema

    #region DbSets
    
    public DbSet<BancoModel> Bancos { get; set; }

    public DbSet<CategoriaModel> Categorias { get; set; }

    public DbSet<GastoFixoModel> GastosFixos { get; set; }

    public DbSet<CarteiraModel> Carteiras { get; set; }

    public DbSet<TipoTransacaoModel> TipoTransacoes { get; set; }

    public DbSet<TransacaoModel> Transacoes { get; set; }

    public DbSet<UsuarioModel> Usuarios { get; set; }
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configurações de mapeamento que implementam IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}