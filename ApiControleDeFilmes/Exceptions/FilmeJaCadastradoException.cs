using System;

namespace ApiControleDeFilmes.Exceptions
{
    public class FilmeJaCadastradoException : Exception
    {
        public FilmeJaCadastradoException()
           : base("Este filme já está cadastrado")
        { }
    }
}
