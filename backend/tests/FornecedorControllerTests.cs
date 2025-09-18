using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myApp.Controllers;
using myApp.Models;
using myApp.Services;

namespace myApp.Tests.Controllers
{
    [TestFixture]
    public class FornecedorControllerTests
    {
        private Mock<IFornecedorService> _mockService;
        private FornecedorController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IFornecedorService>();
            _controller = new FornecedorController(_mockService.Object);
        }

        [Test]
        public async Task RegistrarFornecedor_ReturnsCreatedAtActionResult_WithFornecedor()
        {
            // Arrange
            var fornecedor = new Fornecedor { Id = 1, Nome = "Fornecedor Teste", Documento = "123", TipoFornecedor = "PessoaJuridica" };
            _mockService.Setup(s => s.RegistrarFornecedor(fornecedor)).ReturnsAsync(fornecedor);

            // Act
            var result = await _controller.RegistrarFornecedor(fornecedor) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ConsultarFornecedorPorId", result.ActionName);
            Assert.AreEqual(fornecedor, result.Value);
        }

        [Test]
        public async Task ValidarFornecedor_ReturnsOk_WhenFornecedorFound()
        {
            // Arrange
            int id = 1;
            var fornecedor = new Fornecedor { Id = id, Status = "Validado" };
            _mockService.Setup(s => s.ValidarFornecedor(id)).ReturnsAsync(fornecedor);

            // Act
            var result = await _controller.ValidarFornecedor(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(fornecedor, okResult.Value);
        }

        [Test]
        public async Task ValidarFornecedor_ReturnsNotFound_WhenFornecedorNotFound()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(s => s.ValidarFornecedor(id)).ReturnsAsync((Fornecedor)null);

            // Act
            var result = await _controller.ValidarFornecedor(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task AtualizarFornecedor_ReturnsOk_WhenFornecedorIsUpdated()
        {
            // Arrange
            int id = 1;
            var fornecedorInput = new Fornecedor { Nome = "Novo Nome", Documento = "456", TipoFornecedor = "PessoaFisica" };
            var fornecedorUpdated = new Fornecedor { Id = id, Nome = "Novo Nome", Documento = "456", TipoFornecedor = "PessoaFisica" };
            _mockService.Setup(s => s.AtualizarFornecedor(id, fornecedorInput)).ReturnsAsync(fornecedorUpdated);

            // Act
            var result = await _controller.AtualizarFornecedor(id, fornecedorInput);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(fornecedorUpdated, okResult.Value);
        }

        [Test]
        public async Task AtualizarFornecedor_ReturnsNotFound_WhenFornecedorDoesNotExist()
        {
            // Arrange
            int id = 1;
            var fornecedorInput = new Fornecedor { Nome = "Novo Nome", Documento = "456", TipoFornecedor = "PessoaFisica" };
            _mockService.Setup(s => s.AtualizarFornecedor(id, fornecedorInput)).ReturnsAsync((Fornecedor)null);

            // Act
            var result = await _controller.AtualizarFornecedor(id, fornecedorInput);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task ConsultarFornecedorPorId_ReturnsOk_WhenFornecedorFound()
        {
            // Arrange
            int id = 1;
            var fornecedor = new Fornecedor { Id = id, Nome = "Teste" };
            _mockService.Setup(s => s.ConsultarFornecedorPorId(id)).ReturnsAsync(fornecedor);

            // Act
            var result = await _controller.ConsultarFornecedorPorId(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(fornecedor, okResult.Value);
        }

        [Test]
        public async Task ConsultarFornecedorPorId_ReturnsNotFound_WhenFornecedorNotFound()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(s => s.ConsultarFornecedorPorId(id)).ReturnsAsync((Fornecedor)null);

            // Act
            var result = await _controller.ConsultarFornecedorPorId(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task ConsultarFornecedores_ReturnsOk_WithListOfFornecedores()
        {
            // Arrange
            string filtro = "teste";
            var lista = new List<Fornecedor>
            {
                new Fornecedor { Id = 1, Nome = "Fornecedor 1" },
                new Fornecedor { Id = 2, Nome = "Fornecedor 2" }
            };
            _mockService.Setup(s => s.ConsultarFornecedores(filtro)).ReturnsAsync(lista);

            // Act
            var result = await _controller.ConsultarFornecedores(filtro);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(lista, okResult.Value);
        }
    }
}
