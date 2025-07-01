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

    public DbSet<BancoModel> Bancos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categoria
        modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");

        // Transacao
        modelBuilder.Entity<TransacaoModel>().ToTable("Transacao");

        // TipoTransacao
        modelBuilder.Entity<TipoTransacaoModel>().ToTable("TipoTransacao");
        modelBuilder.Entity<TipoTransacaoModel>().HasData(
            new TipoTransacaoModel 
            { 
                Id = 1, Nome = "Receita", 
                DataInclusao = new DateTime(2025, 01, 01),
                DataAtualizacao = null,
                DataExclusao = null
            },
            new TipoTransacaoModel 
            { 
                Id = 2, Nome = "Despesa",
                DataInclusao = new DateTime(2025, 01, 01),
                DataAtualizacao = null,
                DataExclusao = null
            }
        );

        // Banco
        modelBuilder.Entity<BancoModel>().ToTable("Banco");

        base.OnModelCreating(modelBuilder);
    }
}