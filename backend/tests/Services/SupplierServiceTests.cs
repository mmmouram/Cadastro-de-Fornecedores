using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using FornecedorApi.Services;
using FornecedorApi.Repositories;
using FornecedorApi.Models;
using FornecedorApi.Models.Enums;
using FornecedorApi.Models.Requests;
using FornecedorApi.Models.Responses;

namespace FornecedorApi.Tests.Services
{
    public class SupplierServiceTests
    {
        private readonly Mock<IFornecedorRepository> _repoMock;
        private readonly SupplierService _service;

        public SupplierServiceTests()
        {
            _repoMock = new Mock<IFornecedorRepository>();
            _service = new SupplierService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_NumericoValido_ReturnsResponse()
        {
            // Arrange
            var request = new SupplierRequest
            {
                Nome = "Fornecedor A",
                Cnpj = "11222333000181",
                TipoPessoa = TipoPessoa.Padrao
            };

            var fornecedor = new Fornecedor
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Cnpj = request.Cnpj,
                TipoPessoa = request.TipoPessoa
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Fornecedor>()))
                     .ReturnsAsync(fornecedor);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fornecedor.Id, result.Id);
            Assert.Equal(fornecedor.Cnpj, result.Cnpj);
        }

        [Fact]
        public async Task CreateAsync_MeiComAlfanumerico_ThrowsBusinessException()
        {
            var request = new SupplierRequest
            {
                Nome = "Fornecedor MEI",
                Cnpj = "AB2223330001CZ",
                TipoPessoa = TipoPessoa.MEI
            };

            await Assert.ThrowsAsync<BusinessException>(() => _service.CreateAsync(request));
        }

        [Fact]
        public async Task CreateAsync_DvIncorreto_ThrowsBusinessException()
        {
            var request = new SupplierRequest
            {
                Nome = "Fornecedor B",
                Cnpj = "11222333000182",
                TipoPessoa = TipoPessoa.Padrao
            };

            await Assert.ThrowsAsync<BusinessException>(() => _service.CreateAsync(request));
        }
    }
}