using Xunit;
using MyApp.Backend.ValueObjects;
using MyApp.Backend.Exceptions;

namespace MyApp.Backend.Tests
{
    public class CnpjTests
    {
        [Theory]
        [InlineData("12345678000195")]
        [InlineData("1A3456780001B5")]
        public void Create_ValidCnpj_ReturnsCnpj(string input)
        {
            // Act
            var cnpj = Cnpj.Create(input);
            
            // Assert
            Assert.Equal(input.ToUpper(), cnpj.Value);
        }

        [Theory]
        [InlineData("12345678000196")]
        [InlineData("1A3456780001B4")]
        public void Create_InvalidDv_ThrowsBadRequest(string input)
        {
            // Act & Assert
            var ex = Assert.Throws<BadRequestException>(() => Cnpj.Create(input));
            Assert.Equal("Dígito verificador inválido", ex.Message);
        }

        [Theory]
        [InlineData("12#45678!000195")]
        [InlineData("123")]
        [InlineData("")]
        public void Create_InvalidFormat_ThrowsBadRequest(string input)
        {
            // Act & Assert
            var ex = Assert.Throws<BadRequestException>(() => Cnpj.Create(input));
            Assert.Equal("Formato de CNPJ inválido", ex.Message);
        }
    }
}
