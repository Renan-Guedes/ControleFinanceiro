using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioModel>
{
    public void Configure(EntityTypeBuilder<UsuarioModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("Usuario");
        
        builder.HasIndex(u => u.Email).IsUnique(); // Index para email único

        // Configurações das colunas
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.SenhaHash)
            .IsRequired();

        // Configuração de DataInclusao
        builder.Property(u => u.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
