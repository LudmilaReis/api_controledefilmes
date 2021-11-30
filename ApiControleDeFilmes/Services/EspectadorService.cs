using ApiControleDeFilmes.Entites;
using ApiControleDeFilmes.Exceptions;
using ApiControleDeFilmes.InputModel;
using ApiControleDeFilmes.Repositories;
using ApiControleDeFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Services
{
    public class EspectadorService : IEspectadorService
    {
        private readonly IEspectadorRepository _espectadorRepository;
        public EspectadorService(IEspectadorRepository espectadorRepository)
        {
            _espectadorRepository = espectadorRepository;
        }
        public async Task<List<EspectadorViewModel>> Obter(int pagina, int quantidade)
        {
            var espectadores = await _espectadorRepository.Obter(pagina, quantidade);
            return espectadores.Select(espectador => new EspectadorViewModel
            {
                Id = espectador.Id,
                Nome = espectador.Nome,
                Sobrenome = espectador.Sobrenome,
                Telefone = espectador.Telefone                
            })
                .ToList();

        }
        public async Task<EspectadorViewModel> Obter(Guid id)
        {
            var espectador = await _espectadorRepository.Obter(id);
            if (espectador == null)
                return null;
            return new EspectadorViewModel
            {
                Id = espectador.Id,
                Nome = espectador.Nome,
                Sobrenome = espectador.Sobrenome,
                Telefone = espectador.Telefone
            };
        }
        public async Task<EspectadorViewModel> Inserir(EspectadorInputModel espectador)
        {
            var entidadeEspectador = await _espectadorRepository.Obter(espectador.Nome, espectador.Sobrenome, espectador.Telefone);
            if (entidadeEspectador.Count > 0)
                throw new EspectadorJaCadastradoException();
            var espectadorInsert = new Espectador
            {
                Id = Guid.NewGuid(),
                Nome = espectador.Nome,
                Sobrenome = espectador.Sobrenome,
                Telefone = espectador.Telefone
            };
            await _espectadorRepository.Inserir(espectadorInsert);
            return new EspectadorViewModel
            {
                Id = Guid.NewGuid(),
                Nome = espectador.Nome,
                Sobrenome = espectador.Sobrenome,
                Telefone = espectador.Telefone
            };
        }
        public async Task Atualizar(Guid id, EspectadorInputModel espectador)
        {
            var entidadeEspectador = await _espectadorRepository.Obter(id);
            if (entidadeEspectador == null)
                throw new EspectadorJaCadastradoException();
            entidadeEspectador.Nome = espectador.Nome;
            entidadeEspectador.Sobrenome = espectador.Sobrenome;
            entidadeEspectador.Telefone = espectador.Telefone;            

            await _espectadorRepository.Atualizar(entidadeEspectador);
        }
        public async Task Atualizar(Guid id)
        {
            var entidadeEspectador = await _espectadorRepository.Obter(id);
            if (entidadeEspectador == null)
                throw new EspectadorJaCadastradoException();

             await _espectadorRepository.Atualizar(entidadeEspectador);
        }
        public async Task Remover(Guid id)
        {
            var espectador = await _espectadorRepository.Obter(id);
            if (espectador == null)
                throw new EspectadorJaCadastradoException();
            await _espectadorRepository.Remover(id);
        }
        public void Dispose() => _espectadorRepository?.Dispose();
    }
}
