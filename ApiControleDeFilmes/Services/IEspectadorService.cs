using ApiControleDeFilmes.InputModel;
using ApiControleDeFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Services
{
    public interface IEspectadorService : IDisposable
    {
        Task<List<EspectadorViewModel>> Obter(int pagina, int quantidade);
        Task<EspectadorViewModel> Obter(Guid id);
        Task<EspectadorViewModel> Inserir(EspectadorInputModel filme);
        Task Atualizar(Guid id, EspectadorInputModel filme);
        Task Remover(Guid id);

    }
}
