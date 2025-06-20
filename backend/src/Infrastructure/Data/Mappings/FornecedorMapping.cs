using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;

namespace MyApp.Infrastructure.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Nome");
            builder.Property(f => f.Cnpj)
                .IsRequired()
                .HasColumnName("Cnpj")
                .HasColumnType("VARCHAR(14)")
                .HasConversion(
                    c => c.Value,
                    v => Cnpj.Criar(v)
                );
            builder.Property(f => f.TipoPessoa)
                .IsRequired()
                .HasColumnName("TipoPessoa");
            builder.Property(f => f.DataCriacao)
                .IsRequired()
                .HasColumnName("DataCriacao");
        }
    }
}