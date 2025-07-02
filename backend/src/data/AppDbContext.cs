using Microsoft.EntityFrameworkCore;
using Prudential.Backend.Models;

namespace Prudential.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().ToTable("Fornecedores");
            modelBuilder.Entity<Supplier>().Property(s => s.Cnpj).HasColumnType("VARCHAR(14)");
        }
    }
}
