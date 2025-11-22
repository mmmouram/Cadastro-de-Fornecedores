using Xunit;
using FornecedorApi.Validations;
using FornecedorApi.Models.Enums;

namespace FornecedorApi.Tests.Validations
{
    public class CnpjValidatorTests
    {
        [Fact]
        public void Validar_NumericoValido_ReturnsTrue()
        {
            var (isValid, error) = CnpjValidator.Validar("11222333000181", TipoPessoa.Padrao);
            Assert.True(isValid);
            Assert.Null(error);
        }

        [Fact]
        public void Validar_TamanhoInvalido_ReturnsErroFormato()
        {
            var (isValid, error) = CnpjValidator.Validar("123", TipoPessoa.Padrao);
            Assert.False(isValid);
            Assert.Equal("O formato do CNPJ está incorreto.", error);
        }

        [Fact]
        public void Validar_CaracteresInvalidos_ReturnsErroFormato()
        {
            var (isValid, error) = CnpjValidator.Validar("11222333-000181", TipoPessoa.Padrao);
            Assert.False(isValid);
            Assert.Equal("O formato do CNPJ está incorreto.", error);
        }

        [Fact]
        public void Validar_MeiComAlfanumerico_ReturnsErroMeiSomenteNumerico()
        {
            var (isValid, error) = CnpjValidator.Validar("AB2223330001CZ", TipoPessoa.MEI);
            Assert.False(isValid);
            Assert.Equal("MEI aceita somente CNPJ numérico.", error);
        }

        [Fact]
        public void Validar_DvIncorreto_ReturnsErroConferencia()
        {
            var (isValid, error) = CnpjValidator.Validar("11222333000182", TipoPessoa.Padrao);
            Assert.False(isValid);
            Assert.Equal("O CNPJ não passou na conferência interna.", error);
        }
    }
}