using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ControleFinanceiro.Infra.Data.Configurations;

public class TipoTransacaoConfiguration : IEntityTypeConfiguration<TipoTransacaoModel>
{
    public void Configure(EntityTypeBuilder<TipoTransacaoModel> builder)
    {
        // Configurações da tabela
        builder.ToTable("TipoTransacao");

        // Configurações das colunas
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasData(
            new TipoTransacaoModel
            {
                Id = 1,
                Nome = "Receita",
                DataInclusao = new DateTime(2025, 01, 01, 0, 0, 0),
                DataAtualizacao = null,
                DataExclusao = null
            },
            new TipoTransacaoModel
            {
                Id = 2,
                Nome = "Despesa",
                DataInclusao = new DateTime(2025, 01, 01, 0, 0, 0),
                DataAtualizacao = null,
                DataExclusao = null
            }
        );

        // Configuração de DataInclusao
        builder.Property(t => t.DataInclusao).HasDefaultValueSql("GETDATE()");
    }
}
