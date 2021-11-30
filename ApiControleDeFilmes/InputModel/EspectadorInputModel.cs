using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.InputModel
{
    public class EspectadorInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do especator deve conter entre 3 e 100 cacacteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da sobrenome deve conter entre 1 e 100 cacacteres")]
        public string Sobrenome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da sobrenome deve conter entre 1 e 100 cacacteres")]
        public string Telefone { get; set; }

    }
}
