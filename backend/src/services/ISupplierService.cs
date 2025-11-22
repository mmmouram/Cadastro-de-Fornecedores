using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FornecedorApi.Models.Requests;
using FornecedorApi.Models.Responses;

namespace FornecedorApi.Services
{
    public interface ISupplierService
    {
        Task<SupplierResponse> CreateAsync(SupplierRequest request);
        Task<SupplierResponse> UpdateAsync(Guid id, SupplierRequest request);
        Task<SupplierResponse> GetByIdAsync(Guid id);
        Task<IEnumerable<SupplierResponse>> GetAllAsync();
    }
}