using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
