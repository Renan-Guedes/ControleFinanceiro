using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class GastoFixoConfiguration : IEntityTypeConfiguration<GastoFixoModel>
{
    public void Configure(EntityTypeBuilder<GastoFixoModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("GastoFixo");

        // Configurações das colunas
        builder.HasKey(g => g.Id);

        // Configurações de relacionamento
        builder.HasOne(g => g.Usuario)
            .WithMany(u => u.GastosFixos)
            .HasForeignKey(g => g.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Categoria)
            .WithMany(c => c.GastosFixos)
            .HasForeignKey(g => g.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);


        // Configuração de DataInclusao
        builder.Property(g => g.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
