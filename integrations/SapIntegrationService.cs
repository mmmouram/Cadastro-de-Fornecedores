namespace MyApp.Backend.Integrations
{
    public interface ISapIntegrationService
    {
        /// <summary>
        /// Simula registro de fornecedor no SAP e retorna um VendorCode.
        /// </summary>
        Task<string> RegisterSupplierAsync(string name, string cnpj);
    }

    public class SapIntegrationService : ISapIntegrationService
    {
        public Task<string> RegisterSupplierAsync(string name, string cnpj)
        {
    
            var vendorCode = "VNDR-" + new Random().Next(10000, 99999);
            return Task.FromResult(vendorCode);
        }
    }
}
