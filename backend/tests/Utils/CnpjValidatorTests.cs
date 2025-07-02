using NUnit.Framework;
using Prudential.Backend.Utils;

namespace Prudential.Backend.Tests.Utils
{
    [TestFixture]
    public class CnpjValidatorTests
    {
        [Test]
        public void ValidarCnpj_NumericoValido_RetornaTrue()
        {
            // Arrange
            // CNPJ numérico válido previamente validado com algoritmo módulo 11
            string cnpjNumerico = "11444777000161"; // exemplo válido
            string tipoPessoa = "Outros";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjNumerico, tipoPessoa);

            // Assert
            Assert.IsTrue(resultado);
        }

        [Test]
        public void ValidarCnpj_NumericoIncorretoDv_RetornaFalse()
        {
            // Arrange
            string cnpjNumericoInvalido = "11444777000162"; // DV incorreto
            string tipoPessoa = "Outros";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjNumericoInvalido, tipoPessoa);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void ValidarCnpj_AlfanumericoValido_RetornaTrue()
        {
            // Arrange
            // Utilizando os 12 primeiros caracteres: "A1B2C3D4E5F6" e os dígitos calculados: 2 e 1
            string cnpjAlfanumerico = "A1B2C3D4E5F621";
            string tipoPessoa = "Outros";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjAlfanumerico, tipoPessoa);

            // Assert
            Assert.IsTrue(resultado);
        }

        [Test]
        public void ValidarCnpj_AlfanumericoComCaracteresInvalidos_RetornaFalse()
        {
            // Arrange
            string cnpjInvalido = "A1B2C3D4E5F6!@"; // contém caracteres especiais
            string tipoPessoa = "Outros";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjInvalido, tipoPessoa);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void ValidarCnpj_TamanhoIncorreto_RetornaFalse()
        {
            // Arrange
            string cnpjCurto = "1234567890123"; // 13 caracteres
            string tipoPessoa = "Outros";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjCurto, tipoPessoa);

            // Assert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void ValidarCnpj_MEIComAlfanumerico_DeveRetornarFalse()
        {
            // Arrange
            // Mesmo que o CNPJ alfanumérico seja formatado corretamente, se TipoPessoa é MEI, não é permitido
            string cnpjAlfanumerico = "A1B2C3D4E5F621";
            string tipoPessoa = "MEI";

            // Act
            bool resultado = CnpjValidator.ValidarCnpj(cnpjAlfanumerico, tipoPessoa);

            // Assert
            Assert.IsFalse(resultado);
        }
    }
}
