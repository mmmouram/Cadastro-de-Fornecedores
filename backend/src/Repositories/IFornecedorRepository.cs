using System.Collections.Generic;
using System.Threading.Tasks;
using myApp.Models;

namespace myApp.Repositories
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> AdicionarFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> AtualizarFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> ObterFornecedorPorId(int id);
        Task<IEnumerable<Fornecedor>> ObterFornecedores(string filtro);
        Task CriarHistorico(Fornecedor fornecedor);
    }
}
