using System;
using System.Text.RegularExpressions;
using MyApp.Domain.Exceptions;

namespace MyApp.Domain.ValueObjects
{
    public class Cnpj
    {
        private const int DigitosCnpj = 14;
        public string Value { get; }

        private Cnpj(string value) { Value = value; }

        public static Cnpj Criar(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new FornecedorException("CNPJ não pode ser vazio.");

            var cnpj = valor.Trim().ToUpperInvariant();

            if (cnpj.Length != DigitosCnpj)
                throw new FornecedorException("CNPJ deve ter exatamente 14 caracteres.");

            if (!Regex.IsMatch(cnpj, "^[0-9A-Z]+$"))
                throw new FornecedorException("CNPJ contém caracteres inválidos.");

            if (!ValidarDigitoVerificador(cnpj))
                throw new FornecedorException("CNPJ não passou na conferência interna.");

            return new Cnpj(cnpj);
        }

        private static bool ValidarDigitoVerificador(string cnpj)
        {
            var nums = new int[DigitosCnpj];
            for (int i = 0; i < DigitosCnpj; i++)
            {
                char c = cnpj[i];
                if (char.IsDigit(c))
                    nums[i] = c - '0';
                else
                    nums[i] = c - 48;
            }

            int primeiroDv = CalcularDigito(nums, 12);
            if (nums[12] != primeiroDv) return false;

            int segundoDv = CalcularDigito(nums, 13);
            if (nums[13] != segundoDv) return false;

            return true;
        }

        private static int CalcularDigito(int[] nums, int posDv)
        {
            int soma = 0;
            int peso = 2;
            for (int i = posDv - 1; i >= 0; i--)
            {
                soma += nums[i] * peso;
                peso++;
                if (peso > 9) peso = 2;
            }
            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }

        public override string ToString() => Value;
    }
}