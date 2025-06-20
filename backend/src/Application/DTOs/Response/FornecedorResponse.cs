using System;
using MyApp.Domain.Enums;

namespace MyApp.Application.DTOs.Response
{
    public class FornecedorResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}