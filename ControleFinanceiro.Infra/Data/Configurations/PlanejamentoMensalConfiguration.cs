using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations
{
    public class PlanejamentoMensalConfiguration : IEntityTypeConfiguration<PlanejamentoMensalModel>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoMensalModel> builder)
        {
            // Configurações da tabela
            builder.ToTable("PlanejamentoMensal");

            // Configurações das colunas
            builder.HasKey(p => p.Id);

            // Configurações de relacionamento
            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.PlanejamentosMensais)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de DataInclusao
            builder.Property(p => p.DataInclusao).HasDefaultValueSql("GETDATE()");
        }
    }
}
