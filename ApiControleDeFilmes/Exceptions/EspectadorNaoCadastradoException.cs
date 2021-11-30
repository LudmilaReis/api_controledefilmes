using System;

namespace ApiControleDeFilmes.Exceptions
{
    public class EspectadorNaoCadastradoException : Exception
    {
        public EspectadorNaoCadastradoException()
          : base("Este espectador não está cadastrado")
        { }
    }
        
}
