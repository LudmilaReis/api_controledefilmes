using System;

namespace ApiControleDeFilmes.Exceptions
{
    public class EspectadorJaCadastradoException : Exception
    {
        public EspectadorJaCadastradoException()
           : base("Este espectador já está cadastrado")
        { }
    }
}
