using Prudential.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prudential.Backend.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier> AdicionarAsync(Supplier supplier);
        Task<Supplier> AtualizarAsync(Supplier supplier);
        Task<Supplier> ObterPorIdAsync(int id);
        Task<IEnumerable<Supplier>> ListarAsync();
    }
}
