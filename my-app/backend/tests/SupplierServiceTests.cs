using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MyApp.Backend.Services;
using MyApp.Backend.Repositories;
using MyApp.Backend.DTOs;
using MyApp.Backend.Models;
using MyApp.Backend.ValueObjects;
using MyApp.Backend.Models.Enums;
using MyApp.Backend.Exceptions;

namespace MyApp.Backend.Tests
{
    public class SupplierServiceTests
    {
        private readonly Mock<ISupplierRepository> _repositoryMock;
        private readonly SupplierService _service;

        public SupplierServiceTests()
        {
            _repositoryMock = new Mock<ISupplierRepository>();
            _service = new SupplierService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_MeiWithAlphanumeric_ThrowsBadRequest()
        {
            // Arrange
            var request = new SupplierRequest
            {
                Name = "Teste",
                Cnpj = "1A3456780001B5",
                PersonType = PersonType.MEI
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => _service.CreateAsync(request));
            Assert.Equal("MEI aceita somente CNPJ numérico", ex.Message);
        }

        [Fact]
        public async Task CreateAsync_ValidNumeric_CreatesSupplier()
        {
            // Arrange
            var request = new SupplierRequest
            {
                Name = "Teste",
                Cnpj = "12345678000195",
                PersonType = PersonType.NORMAL
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Supplier>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _service.CreateAsync(request);

            // Assert
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Cnpj, response.Cnpj);
            Assert.Equal(request.PersonType, response.PersonType);
        }

        [Fact]
        public async Task CreateAsync_ValidAlphanumeric_CreatesSupplier()
        {
            // Arrange
            var request = new SupplierRequest
            {
                Name = "Teste",
                Cnpj = "1A3456780001B5",
                PersonType = PersonType.NORMAL
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Supplier>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _service.CreateAsync(request);

            // Assert
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Cnpj, response.Cnpj);
            Assert.Equal(request.PersonType, response.PersonType);
        }

        [Fact]
        public async Task GetByIdAsync_NonExisting_ThrowsBadRequest()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Supplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _service.GetByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetByIdAsync_Existing_ReturnsResponse()
        {
            // Arrange
            var supplier = new Supplier("Nome", Cnpj.Create("12345678000195"), PersonType.NORMAL);
            _repositoryMock.Setup(r => r.GetByIdAsync(supplier.Id))
                .ReturnsAsync(supplier);

            // Act
            var response = await _service.GetByIdAsync(supplier.Id);

            // Assert
            Assert.Equal(supplier.Id, response.Id);
            Assert.Equal(supplier.Name, response.Name);
            Assert.Equal(supplier.Cnpj.Value, response.Cnpj);
        }

        [Fact]
        public async Task ListAsync_ReturnsAllSuppliers()
        {
            // Arrange
            var suppliers = new List<Supplier>
            {
                new Supplier("Nome1", Cnpj.Create("12345678000195"), PersonType.NORMAL),
                new Supplier("Nome2", Cnpj.Create("1A3456780001B5"), PersonType.NORMAL)
            };
            _repositoryMock.Setup(r => r.ListAsync()).ReturnsAsync(suppliers);

            // Act
            var result = await _service.ListAsync();

            // Assert
            Assert.Collection(result,
                item => Assert.Equal("12345678000195", item.Cnpj),
                item => Assert.Equal("1A3456780001B5", item.Cnpj));
        }
    }
}
