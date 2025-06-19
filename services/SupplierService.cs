using MyApp.Backend.Models;
using MyApp.Backend.Repositories;
using MyApp.Backend.Integrations;

namespace MyApp.Backend.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;
        private readonly ISapIntegrationService _sap;

        public SupplierService(
            ISupplierRepository repo,
            ISapIntegrationService sapIntegration)
        {
            _repo = repo;
            _sap = sapIntegration;
        }

        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            // 1) Simula registro no SAP
            supplier.SapVendorCode = await _sap.RegisterSupplierAsync(
                supplier.Name, supplier.Cnpj);

            // 2) Persiste no banco
            return await _repo.AddAsync(supplier);
        }

        public Task<Supplier> GetByIdAsync(Guid id)
        {
            return _repo.GetByIdAsync(id);
        }
    }
}
