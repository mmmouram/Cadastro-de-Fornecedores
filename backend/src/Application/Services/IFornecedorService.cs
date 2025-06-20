using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Application.DTOs.Request;
using MyApp.Application.DTOs.Response;

namespace MyApp.Application.Services
{
    public interface IFornecedorService
    {
        Task<FornecedorResponse> AdicionarAsync(AdicionarFornecedorRequest request);
        Task<FornecedorResponse> AtualizarAsync(Guid id, AtualizarFornecedorRequest request);
        Task<IEnumerable<FornecedorResponse>> ListarAsync();
        Task<FornecedorResponse> ObterPorIdAsync(Guid id);
    }
}