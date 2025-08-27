using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<CategoriaModel>
{
    public void Configure(EntityTypeBuilder<CategoriaModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("Categoria");

        // Configurações das colunas
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);

        // Configurações de relacionamento
        builder.HasOne(c => c.Usuario)
            .WithMany(u => u.Categorias)
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.TipoTransacao)
            .WithMany(t => t.Categorias)
            .HasForeignKey(t => t.TipoTransacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração de DataInclusao
        builder.Property(c => c.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
