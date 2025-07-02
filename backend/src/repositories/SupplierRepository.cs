using Microsoft.EntityFrameworkCore;
using Prudential.Backend.Data;
using Prudential.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prudential.Backend.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> AdicionarAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> AtualizarAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> ObterPorIdAsync(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Supplier>> ListarAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
