using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.Service;
using ControleFinanceiro.Domain.Interfaces;
using ControleFinanceiro.Infra;
using ControleFinanceiro.Infra.Data;
using ControleFinanceiro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registra DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Recupera a Connection String do appsettings.json

    /* SQL SERVER
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    */

    // MySQL
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});

// Registra os serviços de aplicação //

// Categoria
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Transação
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Tipo de Transação
builder.Services.AddScoped<ITipoTransacaoService, TipoTransacaoService>();
builder.Services.AddScoped<ITipoTransacaoRepository, TipoTransacaoRepository>();

// Banco
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<IBancoRepository, BancoRepository>();

// Carteira
builder.Services.AddScoped<ICarteiraService, CarteiraService>();
builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();

// Gastos Fixos
builder.Services.AddScoped<IGastoFixoService, GastoFixoService>();
builder.Services.AddScoped<IGastoFixoRepository, GastoFixoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
