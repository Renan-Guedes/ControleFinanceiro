using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Data.Configurations
{
    public class CarteiraConfiguration : IEntityTypeConfiguration<CarteiraModel>
    {
        public void Configure(EntityTypeBuilder<CarteiraModel> builder)
        {
            // Configurações da tabela
            builder.ToTable("Carteira");

            // Configurações das colunas
            builder.HasKey(p => p.Id);

            // Configurações de relacionamento
            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Carteiras)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de DataInclusao
            builder.Property(p => p.DataInclusao).HasDefaultValueSql("GETDATE()");
        }
    }
}
