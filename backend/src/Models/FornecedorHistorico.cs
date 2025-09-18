using System;
using System.ComponentModel.DataAnnotations;

namespace myApp.Models
{
    public class FornecedorHistorico
    {
        [Key]
        public int Id { get; set; }
        
        public int FornecedorId { get; set; }
        
        public string Nome { get; set; }
        
        public string Documento { get; set; }
        
        public string TipoFornecedor { get; set; }
        
        public string Status { get; set; }
        
        public int Versao { get; set; }
        
        public DateTime DataAlteracao { get; set; }
    }
}
