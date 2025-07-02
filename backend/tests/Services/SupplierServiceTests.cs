using Moq;
using NUnit.Framework;
using Prudential.Backend.Models;
using Prudential.Backend.Repositories;
using Prudential.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prudential.Backend.Tests.Services
{
    [TestFixture]
    public class SupplierServiceTests
    {
        private SupplierService _service;
        private Mock<ISupplierRepository> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ISupplierRepository>();
            _service = new SupplierService(_repositoryMock.Object);
        }

        [Test]
        public async Task CriarFornecedor_ComCnpjNumericoValido_DeveCriarFornecedor()
        {
            // Arrange
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor Numerico",
                TipoPessoa = "Outros",
                // Utilizando um CNPJ numérico válido (14 dígitos) - exemplo.
                // Este número foi previamente validado com o algoritmo de módulo 11.
                Cnpj = "11444777000161"
            };

            // Configura o mock para retornar o fornecedor com Id atribuído
            _repositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Supplier>())).ReturnsAsync((Supplier s) => 
            {
                s.Id = 1;
                return s;
            });

            // Act
            var fornecedorCriado = await _service.CriarFornecedorAsync(request);

            // Assert
            Assert.IsNotNull(fornecedorCriado);
            Assert.AreEqual(request.Nome, fornecedorCriado.Nome);
            Assert.AreEqual(request.TipoPessoa, fornecedorCriado.TipoPessoa);
            Assert.AreEqual(request.Cnpj, fornecedorCriado.Cnpj);
            _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Supplier>()), Times.Once);
        }

        [Test]
        public async Task CriarFornecedor_ComCnpjAlfanumericoValido_DeveCriarFornecedor()
        {
            // Arrange
            // Gerando um CNPJ alfanumérico válido
            // Utilizando os 12 primeiros caracteres: "A1B2C3D4E5F6" e os dígitos calculados conforme algoritmo:
            // Calculado: Primeiro dígito = 2, Segundo dígito = 1, formando: "A1B2C3D4E5F621"
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor Alfanumérico",
                TipoPessoa = "Outros",
                Cnpj = "A1B2C3D4E5F621"
            };

            _repositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Supplier>())).ReturnsAsync((Supplier s) => 
            {
                s.Id = 2;
                return s;
            });

            // Act
            var fornecedorCriado = await _service.CriarFornecedorAsync(request);

            // Assert
            Assert.IsNotNull(fornecedorCriado);
            Assert.AreEqual(request.Nome, fornecedorCriado.Nome);
            Assert.AreEqual(request.TipoPessoa, fornecedorCriado.TipoPessoa);
            Assert.AreEqual(request.Cnpj, fornecedorCriado.Cnpj);
            _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Supplier>()), Times.Once);
        }

        [Test]
        public void CriarFornecedor_MEIComCnpjAlfanumerico_DeveLancarExcecao()
        {
            // Arrange
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor MEI com alfanumérico",
                TipoPessoa = "MEI",
                Cnpj = "A1B2C3D4E5F621"
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CriarFornecedorAsync(request));
            Assert.AreEqual("MEI aceita somente CNPJ numérico", ex.Message);
        }

        [Test]
        public void CriarFornecedor_ComFormatoInvalido_DeveLancarExcecao()
        {
            // Arrange
            // CNPJ com caracteres especiais e tamanho incorreto
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor com formato inválido",
                TipoPessoa = "Outros",
                Cnpj = "1234!6789012" // Menos que 14 ou com símbolo
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CriarFornecedorAsync(request));
            Assert.AreEqual("CNPJ não passou na conferência interna ou formato incorreto.", ex.Message);
        }

        [Test]
        public void CriarFornecedor_ComDvIncorreto_DeveLancarExcecao()
        {
            // Arrange
            // Utilizando um CNPJ numérico com DV incorreto (alterado último dígito)
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor com DV incorreto",
                TipoPessoa = "Outros",
                Cnpj = "11444777000162" // Supostamente inválido
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _service.CriarFornecedorAsync(request));
            Assert.AreEqual("CNPJ não passou na conferência interna ou formato incorreto.", ex.Message);
        }

        [Test]
        public async Task AtualizarFornecedor_MudancaParaAlfanumerico_DeveAtualizarFornecedor()
        {
            // Arrange
            // Fornecedor existente cadastrado com CNPJ numérico
            var fornecedorExistente = new Supplier
            {
                Id = 10,
                Nome = "Fornecedor Existente",
                TipoPessoa = "Outros",
                Cnpj = "11444777000161"
            };

            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor Atualizado",
                TipoPessoa = "Outros",
                // Atualizando para CNPJ alfanumérico válido: "A1B2C3D4E5F621"
                Cnpj = "A1B2C3D4E5F621"
            };

            _repositoryMock.Setup(r => r.ObterPorIdAsync(fornecedorExistente.Id)).ReturnsAsync(fornecedorExistente);
            _repositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<Supplier>())).ReturnsAsync((Supplier s) => s);

            // Act
            var fornecedorAtualizado = await _service.AtualizarFornecedorAsync(fornecedorExistente.Id, request);

            // Assert
            Assert.IsNotNull(fornecedorAtualizado);
            Assert.AreEqual(request.Nome, fornecedorAtualizado.Nome);
            Assert.AreEqual(request.TipoPessoa, fornecedorAtualizado.TipoPessoa);
            Assert.AreEqual(request.Cnpj, fornecedorAtualizado.Cnpj);
            _repositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Supplier>()), Times.Once);
        }

        [Test]
        public void AtualizarFornecedor_FornecedorInexistente_DeveLancarExcecao()
        {
            // Arrange
            var request = new SupplierCadastroRequest
            {
                Nome = "Fornecedor Inexistente",
                TipoPessoa = "Outros",
                Cnpj = "11444777000161"
            };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Supplier)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _service.AtualizarFornecedorAsync(99, request));
            Assert.AreEqual("Fornecedor não encontrado.", ex.Message);
        }

        [Test]
        public async Task ListarFornecedores_DeveRetornarListaDeFornecedores()
        {
            // Arrange
            var fornecedores = new List<Supplier>
            {
                new Supplier { Id = 1, Nome = "Fornecedor 1", TipoPessoa = "Outros", Cnpj = "11444777000161" },
                new Supplier { Id = 2, Nome = "Fornecedor 2", TipoPessoa = "Outros", Cnpj = "A1B2C3D4E5F621" }
            };
            _repositoryMock.Setup(r => r.ListarAsync()).ReturnsAsync(fornecedores);

            // Act
            var lista = await _service.ListarFornecedoresAsync();

            // Assert
            Assert.IsNotNull(lista);
            Assert.AreEqual(2, lista.Count());
        }

        [Test]
        public async Task ObterFornecedorPorId_FornecedorExistente_DeveRetornarFornecedor()
        {
            // Arrange
            var fornecedor = new Supplier { Id = 5, Nome = "Fornecedor Existente", TipoPessoa = "Outros", Cnpj = "11444777000161" };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(fornecedor.Id)).ReturnsAsync(fornecedor);

            // Act
            var result = await _service.ObterFornecedorPorIdAsync(fornecedor.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fornecedor.Id, result.Id);
        }
    }
}
