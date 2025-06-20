using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FornecedorApi.Data;
using FornecedorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FornecedorApi.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly ApplicationDbContext _context;

        public FornecedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Fornecedor> AddAsync(Fornecedor fornecedor)
        {
            fornecedor.Id = Guid.NewGuid();
            await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor> UpdateAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public Task<Fornecedor> GetByIdAsync(Guid id)
        {
            return _context.Fornecedores
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Fornecedor>>(_context.Fornecedores.AsNoTracking());
        }
    }
}