using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infra.Mappings;

public class TransacaoMapping
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.CriadoEm)
            .IsRequired();
        
        builder.Property(x => x.PagoOuRecebidoEm)
            .IsRequired();

        builder.Property(x => x.Tipo)
            .HasColumnType("SMALLINT");

        builder.Property(x => x.Valor)
            .IsRequired()
            .HasColumnType("MONEY");

        builder.Property(x => x.CategoriaId)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .IsRequired();
    }
}
