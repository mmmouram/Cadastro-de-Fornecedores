using System.ComponentModel.DataAnnotations;
using FornecedorApi.Models.Enums;

namespace FornecedorApi.Models.Requests
{
    public class SupplierRequest
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [MinLength(14)]
        [MaxLength(14)]
        public string Cnpj { get; set; }

        [Required]
        public TipoPessoa TipoPessoa { get; set; }
    }
}