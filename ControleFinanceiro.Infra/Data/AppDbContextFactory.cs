using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ControleFinanceiro.Infra.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Cria config lendo o appsettings.json da camada Web
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ControleFinanceiro.Web"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        /* SQL SERVER
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
        */

        // MySQL
        optionsBuilder.UseMySql(
            configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")) // detecta a versão automaticamente
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}
