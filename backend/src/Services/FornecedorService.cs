using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myApp.Models;
using myApp.Repositories;

namespace myApp.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        
        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        
        public async Task<Fornecedor> RegistrarFornecedor(Fornecedor fornecedor)
        {
            fornecedor.DataCriacao = DateTime.UtcNow;
            fornecedor.Status = "PendenteValidação";
            // Versão inicial
            fornecedor.Versao = 1;
            
            var fornecedorCriado = await _fornecedorRepository.AdicionarFornecedor(fornecedor);
            await _fornecedorRepository.CriarHistorico(fornecedorCriado);
            return fornecedorCriado;
        }
        
        public async Task<Fornecedor> ValidarFornecedor(int id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorPorId(id);
            if (fornecedor == null || fornecedor.Status != "PendenteValidação")
                return null;
            
            fornecedor.Status = "Validado";
            fornecedor.Versao += 1;
            var fornecedorAtualizado = await _fornecedorRepository.AtualizarFornecedor(fornecedor);
            await _fornecedorRepository.CriarHistorico(fornecedorAtualizado);
            return fornecedorAtualizado;
        }
        
        public async Task<Fornecedor> AtualizarFornecedor(int id, Fornecedor fornecedor)
        {
            var fornecedorExistente = await _fornecedorRepository.ObterFornecedorPorId(id);
            if (fornecedorExistente == null)
                return null;
            
            // Atualiza apenas os campos relevantes
            fornecedorExistente.Nome = fornecedor.Nome;
            fornecedorExistente.Documento = fornecedor.Documento;
            fornecedorExistente.TipoFornecedor = fornecedor.TipoFornecedor;
            fornecedorExistente.Versao += 1;
            
            var fornecedorAtualizado = await _fornecedorRepository.AtualizarFornecedor(fornecedorExistente);
            await _fornecedorRepository.CriarHistorico(fornecedorAtualizado);
            return fornecedorAtualizado;
        }
        
        public async Task<Fornecedor> ConsultarFornecedorPorId(int id)
        {
            return await _fornecedorRepository.ObterFornecedorPorId(id);
        }
        
        public async Task<IEnumerable<Fornecedor>> ConsultarFornecedores(string filtro)
        {
            return await _fornecedorRepository.ObterFornecedores(filtro);
        }
    }
}
