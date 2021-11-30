using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.ViewModel
{
    public class FilmeViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Autor { get; set; }
        public string Produtora { get; set; }
    }
}
