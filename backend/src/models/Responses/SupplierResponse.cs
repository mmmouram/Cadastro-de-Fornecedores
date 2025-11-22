using System;
using FornecedorApi.Models.Enums;

namespace FornecedorApi.Models.Responses
{
    public class SupplierResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}