using System.ComponentModel.DataAnnotations;

namespace Prudential.Backend.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string TipoPessoa { get; set; } // Ex: MEI ou Outros

        [Required]
        [MaxLength(14)]
        public string Cnpj { get; set; }
    }
}
