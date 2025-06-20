using Xunit;
using MyApp.Domain.ValueObjects;
using MyApp.Domain.Exceptions;

namespace MyApp.Tests.Domain.ValueObjects
{
    public class CnpjTests
    {
        [Theory]
        [InlineData("11222333000181")]
        public void DeveCriarCnpjNumericoValido(string valor)
        {
            var cnpj = Cnpj.Criar(valor);
            Assert.Equal(valor, cnpj.Value);
        }

        [Fact]
        public void NaoDeveCriarCnpjComTamanhoInvalido()
        {
            Assert.Throws<FornecedorException>(() => Cnpj.Criar("123"));
        }

        [Fact]
        public void NaoDeveCriarCnpjComCaracteresInvalidos()
        {
            Assert.Throws<FornecedorException>(() => Cnpj.Criar("12345678901!@#"));
        }

        [Fact]
        public void NaoDeveCriarCnpjComDvInvalido()
        {
            Assert.Throws<FornecedorException>(() => Cnpj.Criar("11222333000100"));
        }
    }
}