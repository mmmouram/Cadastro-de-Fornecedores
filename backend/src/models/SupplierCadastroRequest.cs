using System.ComponentModel.DataAnnotations;

namespace Prudential.Backend.Models
{
    public class SupplierCadastroRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "TipoPessoa é obrigatório")]
        public string TipoPessoa { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter exatamente 14 caracteres")]
        public string Cnpj { get; set; }
    }
}
