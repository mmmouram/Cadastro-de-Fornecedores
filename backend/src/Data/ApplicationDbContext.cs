using Microsoft.EntityFrameworkCore;
using FornecedorApi.Models;

namespace FornecedorApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.ToTable("Fornecedores");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnType("VARCHAR(14)")
                    .HasMaxLength(14);

                entity.Property(e => e.TipoPessoa)
                    .IsRequired();
            });
        }
    }
}