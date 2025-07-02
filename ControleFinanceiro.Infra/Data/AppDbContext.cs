using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // O DbSet representa todas as tabelas do banco de dados em sistema

    public DbSet<BancoModel> Bancos { get; set; }

    public DbSet<CategoriaModel> Categorias { get; set; }

    public DbSet<GastoFixoModel> GastosFixos { get; set; }

    public DbSet<PlanejamentoMensalModel> PlanejamentosMensais { get; set; }

    public DbSet<TipoTransacaoModel> TipoTransacoes { get; set; }

    public DbSet<TransacaoModel> Transacoes { get; set; }

    public DbSet<UsuarioModel> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Banco
        modelBuilder.Entity<BancoModel>().ToTable("Banco");

        // Categoria
        modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");

        // Gastos Fixos
        modelBuilder.Entity<GastoFixoModel>().ToTable("GastoFixo");

        // Planejamento Mensal
        modelBuilder.Entity<PlanejamentoMensalModel>().ToTable("PlanejamentoMensal");

        // TipoTransacao
        modelBuilder.Entity<TipoTransacaoModel>().ToTable("TipoTransacao");
        modelBuilder.Entity<TipoTransacaoModel>().HasData(
            new TipoTransacaoModel
            {
                Id = 1,
                Nome = "Receita",
                DataInclusao = new DateTime(2025, 01, 01),
                DataAtualizacao = null,
                DataExclusao = null
            },
            new TipoTransacaoModel
            {
                Id = 2,
                Nome = "Despesa",
                DataInclusao = new DateTime(2025, 01, 01),
                DataAtualizacao = null,
                DataExclusao = null
            }
        );

        // Transacao
        modelBuilder.Entity<TransacaoModel>().ToTable("Transacao");

        // Usuario
        modelBuilder.Entity<UsuarioModel>().ToTable("Usuario");

        base.OnModelCreating(modelBuilder);
    }
}