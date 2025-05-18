using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Application.UseCases;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infra.Data;
using ControleFinanceiro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Recupera Connection String
var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

// Registra DbContext
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

// Registra serviços
builder.Services.AddScoped<ICategoriaUseCase, CategoriaUseCase>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
