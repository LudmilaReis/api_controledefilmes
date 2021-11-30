using System;

namespace ApiControleDeFilmes.Exceptions
{
    public class FilmeNaoCadastradoException : Exception
    {
        public FilmeNaoCadastradoException()
          : base("Este filme não está cadastrado")
        { }
    }
        
}
