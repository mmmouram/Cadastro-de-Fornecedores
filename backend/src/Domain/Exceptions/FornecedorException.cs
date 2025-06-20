using System;

namespace MyApp.Domain.Exceptions
{
    public class FornecedorException : Exception
    {
        public FornecedorException(string message) : base(message)
        {
        }
    }
}