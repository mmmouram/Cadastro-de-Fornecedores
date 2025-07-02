using Prudential.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prudential.Backend.Services
{
    public interface ISupplierService
    {
        Task<Supplier> CriarFornecedorAsync(SupplierCadastroRequest request);
        Task<Supplier> AtualizarFornecedorAsync(int id, SupplierCadastroRequest request);
        Task<Supplier> ObterFornecedorPorIdAsync(int id);
        Task<IEnumerable<Supplier>> ListarFornecedoresAsync();
    }
}
