using ApiControleDeFilmes.InputModel;
using ApiControleDeFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Services
{
    public interface IFilmeService : IDisposable
    {
        Task<List<FilmeViewModel>> Obter(int pagina, int quantidade);
        Task<FilmeViewModel> Obter(Guid id);
        Task<FilmeViewModel> Inserir(FilmeInputModel filme);
        Task Atualizar(Guid id, FilmeInputModel filme);
        Task Remover(Guid id);

    }
}
