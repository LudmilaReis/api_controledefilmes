using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.InputModel
{
    public class FilmeInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do filme deve conter entre 3 e 100 cacacteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da Categoria deve conter entre 1 e 100 cacacteres")]
        public string Categoria { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do autor deve conter entre 1 e 100 cacacteres")]
        public string Autor { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 1 e 100 cacacteres")]
        public string Produtora { get; set; }

    }
}
