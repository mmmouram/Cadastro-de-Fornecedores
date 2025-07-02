using System;
using System.Linq;

namespace Prudential.Backend.Utils
{
    public static class CnpjValidator
    {
        public static bool ValidarCnpj(string cnpj, string tipoPessoa)
        {
            // Verifica se o CNPJ tem exatamente 14 caracteres
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
                return false;

            // Se o tipo for MEI, não pode conter letras
            if (tipoPessoa.ToUpper() == "MEI" && cnpj.Any(ch => char.IsLetter(ch)))
                return false;

            // Se for composto somente por dígitos, aplica validação numérica
            if (cnpj.All(char.IsDigit))
            {
                return ValidarCnpjNumerico(cnpj);
            }
            else
            {
                // Se contiver letras, deve ser alfanumérico válido
                if (!cnpj.All(ch => char.IsLetterOrDigit(ch)))
                    return false;
                return ValidarCnpjAlfanumerico(cnpj);
            }
        }

        private static bool ValidarCnpjNumerico(string cnpj)
        {
            int[] multiplicadores1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpjBase = cnpj.Substring(0, 12);
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpjBase[i].ToString()) * multiplicadores1[i];
            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            cnpjBase += digito1.ToString();
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpjBase[i].ToString()) * multiplicadores2[i];
            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            string dv = cnpj.Substring(12, 2);
            return dv == $"{digito1}{digito2}";
        }

        private static bool ValidarCnpjAlfanumerico(string cnpj)
        {
            // Para letras A-Z, utilizar valor ASCII-48 (A -> 17, B -> 18, ...)
            int[] pesos = new int[] { 2, 3, 4, 5, 6, 7, 8, 9 };
            int soma = 0;
            // Calcula o primeiro dígito verificador usando os 12 primeiros caracteres
            for (int i = 0; i < 12; i++)
            {
                int valor = ObterValorAlfanumerico(cnpj[i]);
                int peso = pesos[i % pesos.Length];
                soma += valor * peso;
            }
            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                int valor = (i < 12) ? ObterValorAlfanumerico(cnpj[i]) : digito1;
                int peso = pesos[i % pesos.Length];
                soma += valor * peso;
            }
            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            string dv = cnpj.Substring(12, 2);
            return dv == $"{digito1}{digito2}";
        }

        private static int ObterValorAlfanumerico(char ch)
        {
            if (char.IsDigit(ch))
                return ch - '0';
            if (char.IsLetter(ch))
                return ((int)char.ToUpper(ch)) - 48;
            return 0;
        }
    }
}
