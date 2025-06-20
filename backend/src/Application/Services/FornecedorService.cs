using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyApp.Application.DTOs.Request;
using MyApp.Application.DTOs.Response;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using MyApp.Domain.Enums;
using MyApp.Domain.Exceptions;

namespace MyApp.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _repository;
        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<FornecedorResponse> AdicionarAsync(AdicionarFornecedorRequest request)
        {
            if (request.TipoPessoa == TipoPessoa.MEI && Regex.IsMatch(request.Cnpj, "[A-Z]"))
                throw new FornecedorException("MEI aceita somente CNPJ numérico");

            var cnpj = Cnpj.Criar(request.Cnpj);

            var existente = await _repository.GetByCnpjAsync(cnpj.Value);
            if (existente != null)
                throw new FornecedorException("Fornecedor com este CNPJ já existe.");

            var fornecedor = new Fornecedor(Guid.NewGuid(), request.Nome, cnpj, request.TipoPessoa);
            await _repository.AddAsync(fornecedor);

            return new FornecedorResponse
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Cnpj = fornecedor.Cnpj.Value,
                TipoPessoa = fornecedor.TipoPessoa
            };
        }

        public async Task<FornecedorResponse> AtualizarAsync(Guid id, AtualizarFornecedorRequest request)
        {
            var fornecedor = await _repository.GetByIdAsync(id)
                ?? throw new FornecedorException("Fornecedor não encontrado.");

            if (request.TipoPessoa == TipoPessoa.MEI && Regex.IsMatch(request.Cnpj, "[A-Z]"))
                throw new FornecedorException("MEI aceita somente CNPJ numérico");

            var cnpj = Cnpj.Criar(request.Cnpj);
            fornecedor.AtualizarCnpj(cnpj);
            await _repository.UpdateAsync(fornecedor);

            return new FornecedorResponse
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Cnpj = fornecedor.Cnpj.Value,
                TipoPessoa = fornecedor.TipoPessoa
            };
        }

        public async Task<IEnumerable<FornecedorResponse>> ListarAsync()
        {
            var fornecedores = await _repository.GetAllAsync();
            return fornecedores.Select(f => new FornecedorResponse
            {
                Id = f.Id,
                Nome = f.Nome,
                Cnpj = f.Cnpj.Value,
                TipoPessoa = f.TipoPessoa
            });
        }

        public async Task<FornecedorResponse> ObterPorIdAsync(Guid id)
        {
            var f = await _repository.GetByIdAsync(id)
                ?? throw new FornecedorException("Fornecedor não encontrado.");
            return new FornecedorResponse
            {
                Id = f.Id,
                Nome = f.Nome,
                Cnpj = f.Cnpj.Value,
                TipoPessoa = f.TipoPessoa
            };
        }
    }
}