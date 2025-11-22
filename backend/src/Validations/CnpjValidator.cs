using System;
using System.Linq;
using FornecedorApi.Models.Enums;

namespace FornecedorApi.Validations
{
    public static class CnpjValidator
    {
        public static (bool isValid, string errorMessage) Validar(string cnpj, TipoPessoa tipoPessoa)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return (false, "CNPJ não pode ser vazio.");

            if (cnpj.Length != 14)
                return (false, "O formato do CNPJ está incorreto.");

            if (!cnpj.All(char.IsLetterOrDigit))
                return (false, "O formato do CNPJ está incorreto.");

            bool hasLetter = cnpj.Any(char.IsLetter);

            if (tipoPessoa == TipoPessoa.MEI && hasLetter)
                return (false, "MEI aceita somente CNPJ numérico.");

            if (!ValidarDigitoVerificador(cnpj))
                return (false, "O CNPJ não passou na conferência interna.");

            return (true, null);
        }

        private static int MapearValor(char c)
        {
            return (int)c - 48;
        }

        private static bool ValidarDigitoVerificador(string cnpj)
        {
            var nums = cnpj.Select(MapearValor).ToArray();

            // Primeiro dígito verificador
            int soma = 0, peso = 2;
            for (int i = 11; i >= 0; i--)
            {
                soma += nums[i] * peso;
                peso++;
                if (peso > 9) peso = 2;
            }
            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;
            if (nums[12] != dv1) return false;

            // Segundo dígito verificador
            soma = 0; peso = 2;
            for (int i = 12; i >= 0; i--)
            {
                soma += nums[i] * peso;
                peso++;
                if (peso > 9) peso = 2;
            }
            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;
            if (nums[13] != dv2) return false;

            return true;
        }
    }
}