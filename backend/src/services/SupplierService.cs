using Prudential.Backend.Models;
using Prudential.Backend.Repositories;
using Prudential.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prudential.Backend.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<Supplier> CriarFornecedorAsync(SupplierCadastroRequest request)
        {
            // Validação do CNPJ
            if (!CnpjValidator.ValidarCnpj(request.Cnpj, request.TipoPessoa))
            {
                throw new ArgumentException("CNPJ não passou na conferência interna ou formato incorreto.");
            }

            // Regra para MEI: não aceita CNPJ alfanumérico
            if (request.TipoPessoa.ToUpper() == "MEI" && !IsCnpjNumerico(request.Cnpj))
            {
                throw new ArgumentException("MEI aceita somente CNPJ numérico");
            }

            var supplier = new Supplier
            {
                Nome = request.Nome,
                TipoPessoa = request.TipoPessoa,
                Cnpj = request.Cnpj
            };

            return await _repository.AdicionarAsync(supplier);
        }

        public async Task<Supplier> AtualizarFornecedorAsync(int id, SupplierCadastroRequest request)
        {
            var supplierExistente = await _repository.ObterPorIdAsync(id);
            if (supplierExistente == null)
            {
                throw new ArgumentException("Fornecedor não encontrado.");
            }

            if (!CnpjValidator.ValidarCnpj(request.Cnpj, request.TipoPessoa))
            {
                throw new ArgumentException("CNPJ não passou na conferência interna ou formato incorreto.");
            }

            if (request.TipoPessoa.ToUpper() == "MEI" && !IsCnpjNumerico(request.Cnpj))
            {
                throw new ArgumentException("MEI aceita somente CNPJ numérico");
            }

            supplierExistente.Nome = request.Nome;
            supplierExistente.TipoPessoa = request.TipoPessoa;
            supplierExistente.Cnpj = request.Cnpj;

            return await _repository.AtualizarAsync(supplierExistente);
        }

        public async Task<Supplier> ObterFornecedorPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Supplier>> ListarFornecedoresAsync()
        {
            return await _repository.ListarAsync();
        }

        private bool IsCnpjNumerico(string cnpj)
        {
            foreach (var ch in cnpj)
            {
                if (!char.IsDigit(ch))
                    return false;
            }
            return true;
        }
    }
}
