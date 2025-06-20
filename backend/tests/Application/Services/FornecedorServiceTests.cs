using Xunit;
using Moq;
using MyApp.Application.Services;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using MyApp.Domain.Enums;
using MyApp.Domain.Exceptions;
using MyApp.Application.DTOs.Request;
using System;
using System.Threading.Tasks;

namespace MyApp.Tests.Application.Services
{
    public class FornecedorServiceTests
    {
        private readonly Mock<IFornecedorRepository> _repoMock;
        private readonly FornecedorService _service;
        public FornecedorServiceTests()
        {
            _repoMock = new Mock<IFornecedorRepository>();
            _service = new FornecedorService(_repoMock.Object);
        }

        [Fact]
        public async Task DeveAdicionarFornecedorNumericoComSucesso()
        {
            var request = new AdicionarFornecedorRequest
            {
                Nome = "Fornecedor Teste",
                Cnpj = "11222333000181",
                TipoPessoa = TipoPessoa.PessoaJuridica
            };
            _repoMock.Setup(r => r.GetByCnpjAsync(request.Cnpj))
                .ReturnsAsync((Fornecedor)null);

            var response = await _service.AdicionarAsync(request);

            Assert.Equal(request.Nome, response.Nome);
            Assert.Equal(request.Cnpj, response.Cnpj);
        }

        [Fact]
        public async Task NaoDeveAdicionarMeiComCnpjAlfanumerico()
        {
            var request = new AdicionarFornecedorRequest
            {
                Nome = "Fornecedor MEI",
                Cnpj = "ABCD5678901234",
                TipoPessoa = TipoPessoa.MEI
            };

            await Assert.ThrowsAsync<FornecedorException>(async () =>
                await _service.AdicionarAsync(request));
        }

        [Fact]
        public async Task NaoDeveAdicionarCnpjComDvInvalido()
        {
            var request = new AdicionarFornecedorRequest
            {
                Nome = "Fornecedor DV inválido",
                Cnpj = "11222333000100",
                TipoPessoa = TipoPessoa.PessoaJuridica
            };

            await Assert.ThrowsAsync<FornecedorException>(async () =>
                await _service.AdicionarAsync(request));
        }
    }
}