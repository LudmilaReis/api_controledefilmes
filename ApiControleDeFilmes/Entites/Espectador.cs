using System;
using System.Collections.Generic;

namespace ApiControleDeFilmes.Entites
{
    public class Espectador
    {    
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }

        //public IEnumerable<EspectadorCategoria> EspectadorCategorias { get; set; }

        //public IEnumerable<EspectadorFilme> EspectadoresFilmes { get; set; }
    }

}

