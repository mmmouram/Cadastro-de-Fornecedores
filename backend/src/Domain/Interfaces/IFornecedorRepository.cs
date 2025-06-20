using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> GetByCnpjAsync(string cnpj);
        Task<Fornecedor> GetByIdAsync(Guid id);
        Task AddAsync(Fornecedor fornecedor);
        Task UpdateAsync(Fornecedor fornecedor);
        Task<IEnumerable<Fornecedor>> GetAllAsync();
    }
}