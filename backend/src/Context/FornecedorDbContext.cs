using Microsoft.EntityFrameworkCore;
using myApp.Models;

namespace myApp.Context
{
    public class FornecedorDbContext : DbContext
    {
        public FornecedorDbContext(DbContextOptions<FornecedorDbContext> options) : base(options)
        {
        }
        
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FornecedorHistorico> FornecedorHistoricos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais, se necessário
        }
    }
}
