using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    #region Tabelas
    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Transacao> Transacoes { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    #endregion

    #region Fluent Mapping
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Identifica todos os mapeamentos que implementam o IEntityConfiguration

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        */

        modelBuilder.Entity<Categoria>().ToTable("Categoria");
        modelBuilder.Entity<Transacao>().ToTable("Transacao");
        modelBuilder.Entity<Usuario>().ToTable("Usuario");
    }

    #endregion
}
