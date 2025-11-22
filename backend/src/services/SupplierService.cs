using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FornecedorApi.Models;
using FornecedorApi.Models.Enums;
using FornecedorApi.Models.Requests;
using FornecedorApi.Models.Responses;
using FornecedorApi.Repositories;
using FornecedorApi.Validations;

namespace FornecedorApi.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IFornecedorRepository _repository;

        public SupplierService(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<SupplierResponse> CreateAsync(SupplierRequest request)
        {
            var (isValid, error) = CnpjValidator.Validar(request.Cnpj, request.TipoPessoa);
            if (!isValid)
                throw new BusinessException(error);

            var fornecedor = new Fornecedor
            {
                Nome = request.Nome,
                Cnpj = request.Cnpj,
                TipoPessoa = request.TipoPessoa
            };

            var result = await _repository.AddAsync(fornecedor);

            return new SupplierResponse
            {
                Id = result.Id,
                Nome = result.Nome,
                Cnpj = result.Cnpj,
                TipoPessoa = result.TipoPessoa
            };
        }

        public async Task<SupplierResponse> UpdateAsync(Guid id, SupplierRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new BusinessException("Fornecedor não encontrado.");

            var (isValid, error) = CnpjValidator.Validar(request.Cnpj, request.TipoPessoa);
            if (!isValid)
                throw new BusinessException(error);

            existing.Nome = request.Nome;
            existing.Cnpj = request.Cnpj;
            existing.TipoPessoa = request.TipoPessoa;

            var updated = await _repository.UpdateAsync(existing);

            return new SupplierResponse
            {
                Id = updated.Id,
                Nome = updated.Nome,
                Cnpj = updated.Cnpj,
                TipoPessoa = updated.TipoPessoa
            };
        }

        public async Task<IEnumerable<SupplierResponse>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(f => new SupplierResponse
            {
                Id = f.Id,
                Nome = f.Nome,
                Cnpj = f.Cnpj,
                TipoPessoa = f.TipoPessoa
            });
        }

        public async Task<SupplierResponse> GetByIdAsync(Guid id)
        {
            var f = await _repository.GetByIdAsync(id);
            if (f == null)
                return null;

            return new SupplierResponse
            {
                Id = f.Id,
                Nome = f.Nome,
                Cnpj = f.Cnpj,
                TipoPessoa = f.TipoPessoa
            };
        }
    }

    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}