using ApiControleDeFilmes.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public interface IEspectadorRepository : IDisposable
    {
        Task<List<Espectador>> Obter(int pagina, int quantidade);
        Task<Espectador> Obter(Guid id);
        Task<List<Espectador>> Obter(string nome, string sobrenome, string telefone);
        Task Inserir(Espectador espectador);
        Task Atualizar(Espectador espectador);
        Task Remover(Guid id);
    }
}
