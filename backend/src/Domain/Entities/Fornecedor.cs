using System;
using MyApp.Domain.ValueObjects;
using MyApp.Domain.Enums;

namespace MyApp.Domain.Entities
{
    public class Fornecedor
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public TipoPessoa TipoPessoa { get; private set; }
        public DateTime DataCriacao { get; private set; }

        private Fornecedor() { }

        public Fornecedor(Guid id, string nome, Cnpj cnpj, TipoPessoa tipoPessoa)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Cnpj = cnpj ?? throw new ArgumentNullException(nameof(cnpj));
            TipoPessoa = tipoPessoa;
            DataCriacao = DateTime.UtcNow;
        }

        public void AtualizarCnpj(Cnpj novoCnpj)
        {
            Cnpj = novoCnpj ?? throw new ArgumentNullException(nameof(novoCnpj));
        }
    }
}