using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using myApp.Models;
using myApp.Repositories;
using myApp.Services;

namespace myApp.Tests.Services
{
    [TestFixture]
    public class FornecedorServiceTests
    {
        private Mock<IFornecedorRepository> _mockRepository;
        private FornecedorService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IFornecedorRepository>();
            _service = new FornecedorService(_mockRepository.Object);
        }

        [Test]
        public async Task RegistrarFornecedor_Should_SetProperFields_And_CallRepositoryMethods()
        {
            // Arrange
            var fornecedor = new Fornecedor { Nome = "Teste", Documento = "123", TipoFornecedor = "PessoaJuridica" };
            var fornecedorRetorno = new Fornecedor { Id = 1, Nome = fornecedor.Nome, Documento = fornecedor.Documento, TipoFornecedor = fornecedor.TipoFornecedor, Versao = 1, Status = "PendenteValidação", DataCriacao = DateTime.UtcNow };

            _mockRepository.Setup(repo => repo.AdicionarFornecedor(It.IsAny<Fornecedor>())).ReturnsAsync((Fornecedor f) => {
                f.Id = fornecedorRetorno.Id;
                return f;
            });
            
            _mockRepository.Setup(repo => repo.CriarHistorico(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _service.RegistrarFornecedor(fornecedor);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("PendenteValidação", resultado.Status);
            Assert.AreEqual(1, resultado.Versao);
            _mockRepository.Verify(repo => repo.AdicionarFornecedor(It.IsAny<Fornecedor>()), Times.Once);
            _mockRepository.Verify(repo => repo.CriarHistorico(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Test]
        public async Task ValidarFornecedor_Should_UpdateStatusAndVersion_WhenFornecedorIsPendente()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedor = new Fornecedor { Id = fornecedorId, Status = "PendenteValidação", Versao = 1, DataCriacao = DateTime.UtcNow };
            _mockRepository.Setup(repo => repo.ObterFornecedorPorId(fornecedorId)).ReturnsAsync(fornecedor);
            _mockRepository.Setup(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>())).ReturnsAsync((Fornecedor f) => f);
            _mockRepository.Setup(repo => repo.CriarHistorico(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _service.ValidarFornecedor(fornecedorId);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Validado", resultado.Status);
            Assert.AreEqual(2, resultado.Versao);
            _mockRepository.Verify(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>()), Times.Once);
            _mockRepository.Verify(repo => repo.CriarHistorico(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Test]
        public async Task ValidarFornecedor_Should_ReturnNull_WhenFornecedorIsNotPendente()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedor = new Fornecedor { Id = fornecedorId, Status = "Validado", Versao = 1, DataCriacao = DateTime.UtcNow };
            _mockRepository.Setup(repo => repo.ObterFornecedorPorId(fornecedorId)).ReturnsAsync(fornecedor);

            // Act
            var resultado = await _service.ValidarFornecedor(fornecedorId);

            // Assert
            Assert.IsNull(resultado);
            _mockRepository.Verify(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>()), Times.Never);
        }

        [Test]
        public async Task AtualizarFornecedor_Should_UpdateFieldsAndVersion_WhenFornecedorExists()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedorExistente = new Fornecedor { Id = fornecedorId, Nome = "Nome Antigo", Documento = "123", TipoFornecedor = "PessoaJuridica", Versao = 1 };
            var fornecedorUpdate = new Fornecedor { Nome = "Nome Novo", Documento = "456", TipoFornecedor = "PessoaFisica" };

            _mockRepository.Setup(repo => repo.ObterFornecedorPorId(fornecedorId)).ReturnsAsync(fornecedorExistente);
            _mockRepository.Setup(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>())).ReturnsAsync((Fornecedor f) => f);
            _mockRepository.Setup(repo => repo.CriarHistorico(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _service.AtualizarFornecedor(fornecedorId, fornecedorUpdate);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Nome Novo", resultado.Nome);
            Assert.AreEqual("456", resultado.Documento);
            Assert.AreEqual("PessoaFisica", resultado.TipoFornecedor);
            Assert.AreEqual(2, resultado.Versao);
            _mockRepository.Verify(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>()), Times.Once);
            _mockRepository.Verify(repo => repo.CriarHistorico(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Test]
        public async Task AtualizarFornecedor_Should_ReturnNull_WhenFornecedorDoesNotExist()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedorUpdate = new Fornecedor { Nome = "Nome Novo", Documento = "456", TipoFornecedor = "PessoaFisica" };
            
            _mockRepository.Setup(repo => repo.ObterFornecedorPorId(fornecedorId)).ReturnsAsync((Fornecedor)null);

            // Act
            var resultado = await _service.AtualizarFornecedor(fornecedorId, fornecedorUpdate);

            // Assert
            Assert.IsNull(resultado);
            _mockRepository.Verify(repo => repo.AtualizarFornecedor(It.IsAny<Fornecedor>()), Times.Never);
        }

        [Test]
        public async Task ConsultarFornecedorPorId_Should_ReturnFornecedor_WhenFound()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedor = new Fornecedor { Id = fornecedorId, Nome = "Teste" };
            _mockRepository.Setup(repo => repo.ObterFornecedorPorId(fornecedorId)).ReturnsAsync(fornecedor);

            // Act
            var resultado = await _service.ConsultarFornecedorPorId(fornecedorId);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(fornecedorId, resultado.Id);
        }

        [Test]
        public async Task ConsultarFornecedores_Should_ReturnListOfFornecedores()
        {
            // Arrange
            string filtro = "Teste";
            var fornecedores = new[] { new Fornecedor { Id = 1, Nome = "Fornecedor 1" }, new Fornecedor { Id = 2, Nome = "Fornecedor 2" } };
            _mockRepository.Setup(repo => repo.ObterFornecedores(filtro)).ReturnsAsync(fornecedores);

            // Act
            var resultado = await _service.ConsultarFornecedores(filtro);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, ((System.Collections.ICollection)resultado).Count);
        }
    }
}
