using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<TransacaoModel>
{
    public void Configure(EntityTypeBuilder<TransacaoModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("Transacao");

        // Configurações das colunas
        builder.HasKey(t => t.Id);

        // Configurações de relacionamento
        builder.HasOne(t => t.Usuario)
            .WithMany(u => u.Transacoes)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Categoria)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Banco)
            .WithMany(b => b.Transacoes)
            .HasForeignKey(t => t.BancoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.TipoTransacao)
            .WithMany(t => t.Transacoes)
            .HasForeignKey(t => t.TipoTransacaoId)
            .OnDelete(DeleteBehavior.Restrict);


        // Configuração de DataInclusao
        builder.Property(t => t.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}