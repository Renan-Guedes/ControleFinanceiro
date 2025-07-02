using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class BancoConfiguration : IEntityTypeConfiguration<BancoModel>
{
    public void Configure(EntityTypeBuilder<BancoModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("Banco");

        // Configurações das colunas
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Nome)
            .IsRequired()
            .HasMaxLength(100);

        // Configurações de relacionamento
        builder.HasOne(b => b.Usuario)
            .WithMany(u => u.Bancos)
            .HasForeignKey(b => b.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração de DataInclusao
        builder.Property(b => b.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
