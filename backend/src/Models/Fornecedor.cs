using System;
using System.ComponentModel.DataAnnotations;

namespace myApp.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        // Documento: pode ser CNPJ ou CPF
        [Required]
        public string Documento { get; set; }

        // Tipo do fornecedor: "PessoaFisica" ou "PessoaJuridica"
        [Required]
        public string TipoFornecedor { get; set; }

        // Status: PendenteValidação, Validado, etc.
        [Required]
        public string Status { get; set; }

        // Número de versão para controle de histórico
        public int Versao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
