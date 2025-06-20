using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FornecedorApi.Models;

namespace FornecedorApi.Repositories
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> AddAsync(Fornecedor fornecedor);
        Task<Fornecedor> UpdateAsync(Fornecedor fornecedor);
        Task<Fornecedor> GetByIdAsync(Guid id);
        Task<IEnumerable<Fornecedor>> GetAllAsync();
    }
}