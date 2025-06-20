using MyApp.Domain.Enums;

namespace MyApp.Application.DTOs.Request
{
    public class AdicionarFornecedorRequest
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}