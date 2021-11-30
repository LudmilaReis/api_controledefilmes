using ApiControleDeFilmes.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private static Dictionary<Guid, Filme> filmes = new Dictionary<Guid, Filme>()
        {
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "O Poderoso Chefão", Categoria = "Drama", Autor = "Mario Puzo", Produtora = "Alfran Productions"} },
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "A Lista de Schindler", Categoria = "Drama", Autor = "Thomas Keneally", Produtora = "Amblin Entertainment"} },
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "Um Sonho de Liberdade", Categoria = "Drama", Autor = "Stephen King", Produtora = "Columbia Pictures"} },
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "Vingadores: Ultimato", Categoria = "Ação", Autor = "Christopher Markus", Produtora = "Marvel Studios"} },
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "O Rei Leão", Categoria = "Animação", Autor = "Linda Woolverton", Produtora = "Walt Disney"} },
            {Guid.Parse(""), new Filme{Id = Guid.Parse(""), Nome = "O Alto da Compadecida", Categoria = "Comédia", Autor = "Ariano Suassuna", Produtora = "Globo Filmes"} }
        };

        public Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(filmes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Filme> Obter(Guid id)
        {
            if (!filmes.ContainsKey(id))
                return null;

            return Task.FromResult(filmes[id]);
        }

        public Task<List<Filme>> Obter(string nome, string categoria, string autor, string produtora)
        {
            return Task.FromResult(filmes.Values.Where(filme => filme.Nome.Equals(nome) && filme.Categoria.Equals(categoria) && filme.Autor.Equals(autor) && filme.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Filme>> ObterSemLambda(string nome, string categoria, string autor, string produtora)
        {
            var retorno = new List<Filme>();

            foreach (var filme in filmes.Values)
            {
                if (filme.Nome.Equals(nome))
                    retorno.Add(filme);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Filme filme)
        {
            filmes.Add(filme.Id, filme);
            return Task.CompletedTask;
        }

        public Task Atualizar(Filme filme)
        {
            filmes[filme.Id] = filme;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            filmes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
