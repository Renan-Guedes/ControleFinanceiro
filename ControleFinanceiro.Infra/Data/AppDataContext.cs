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
}
