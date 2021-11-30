using ApiControleDeFilmes.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public class EspectadorRepository : IEspectadorRepository
    {
        private static Dictionary<Guid, Espectador> espectadores = new Dictionary<Guid, Espectador>()
        {
            {Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Espectador{Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Nome = "Ludmila", Sobrenome = "Reis", Telefone = "21997923011"} },

        };
        public Task<List<Espectador>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(espectadores.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Espectador> Obter(Guid id)
        {
            if (!espectadores.ContainsKey(id))
                return null;

            return Task.FromResult(espectadores[id]);
        }

        public Task<List<Espectador>> Obter(string nome, string sobrenome, string telefone)
        {
            return Task.FromResult(espectadores.Values.Where(espectador => espectador.Nome.Equals(nome) && espectador.Sobrenome.Equals(sobrenome) && espectador.Telefone.Equals(telefone)).ToList());
        }

        public Task<List<Espectador>> ObterSemLambda(string nome, string sobrenome, string telefone)
        {
            var retorno = new List<Espectador>();

            foreach (var espectaor in espectadores.Values)
            {
                if (espectaor.Nome.Equals(nome))
                    retorno.Add(espectaor);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Espectador espectador)
        {
            espectadores.Add(espectador.Id, espectador);
            return Task.CompletedTask;
        }

        public Task Atualizar(Espectador espectador)
        {
            espectadores[espectador.Id] = espectador;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            espectadores.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
