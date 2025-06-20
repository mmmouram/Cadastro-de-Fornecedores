using MyApp.Domain.Enums;

namespace MyApp.Application.DTOs.Request
{
    public class AtualizarFornecedorRequest
    {
        public string Cnpj { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}