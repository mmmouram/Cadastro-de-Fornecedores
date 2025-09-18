using System.Collections.Generic;
using System.Threading.Tasks;
using myApp.Models;

namespace myApp.Services
{
    public interface IFornecedorService
    {
        Task<Fornecedor> RegistrarFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> ValidarFornecedor(int id);
        Task<Fornecedor> AtualizarFornecedor(int id, Fornecedor fornecedor);
        Task<Fornecedor> ConsultarFornecedorPorId(int id);
        Task<IEnumerable<Fornecedor>> ConsultarFornecedores(string filtro);
    }
}
