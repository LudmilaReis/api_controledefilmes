using ApiControleDeFilmes.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public interface IFilmeRepository : IDisposable
    {
        Task<List<Filme>> Obter(int pagina, int quantidade);
        Task<Filme> Obter(Guid id);
        Task<List<Filme>> Obter(string nome, string categoria, string autor, string produtra);
        Task Inserir(Filme filme);
        Task Atualizar(Filme filme);
        Task Remover(Guid id);
    }
}
