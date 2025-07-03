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
        builder.Property(g => g.Descricao)
            .HasMaxLength(200);

        // Configurações de relacionamento
        builder.HasOne(g => g.Categoria)
            .WithMany(c => c.GastosFixos)
            .HasForeignKey(g => g.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

       builder.HasOne(g => g.TipoTransacao)
            .WithMany(t => t.GastosFixos)
            .HasForeignKey(g => g.TipoTransacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Banco)
            .WithMany(b => b.GastosFixos)
            .HasForeignKey(g => g.BancoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.PlanejamentoMensal)
            .WithMany(pm => pm.GastosFixos)
            .HasForeignKey(g => g.PlanejamentoMensalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Usuario)
            .WithMany(u => u.GastosFixos)
            .HasForeignKey(g => g.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração de DataInclusao
        builder.Property(g => g.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
