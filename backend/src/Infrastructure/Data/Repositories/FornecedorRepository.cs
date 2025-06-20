using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Data.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly MyAppDbContext _context;
        public FornecedorRepository(MyAppDbContext context) { _context = context; }

        public async Task AddAsync(Fornecedor fornecedor)
        {
            await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Fornecedor> GetByCnpjAsync(string cnpj)
        {
            return await _context.Fornecedores
                  .AsNoTracking()
                  .FirstOrDefaultAsync(f => f.Cnpj.Value == cnpj);
        }

        public async Task<Fornecedor> GetByIdAsync(Guid id)
        {
            return await _context.Fornecedores
               .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return await _context.Fornecedores.AsNoTracking().ToListAsync();
        }
    }
}