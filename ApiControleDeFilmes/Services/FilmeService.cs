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
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }
        public async Task<List<FilmeViewModel>> Obter(int pagina, int quantidade)
        {
            var filmes = await _filmeRepository.Obter(pagina, quantidade);
            return filmes.Select(filme => new FilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Categoria = filme.Categoria,
                Autor = filme.Autor,
                Produtora = filme.Produtora                
            })
                .ToList();

        }
        public async Task<FilmeViewModel> Obter(Guid id)
        {
            var filme = await _filmeRepository.Obter(id);
            if (filme == null)
                return null;
            return new FilmeViewModel
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Categoria = filme.Categoria,
                Autor = filme.Autor,
                Produtora = filme.Produtora
            };
        }
        public async Task<FilmeViewModel> Inserir(FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepository.Obter(filme.Nome, filme.Categoria, filme.Autor, filme.Produtora);
            if (entidadeFilme.Count > 0)
                throw new FilmeJaCadastradoException();
            var filmeInsert = new Filme
            {
                Id = Guid.NewGuid(),
                Nome = filme.Nome,
                Categoria = filme.Categoria,
                Autor = filme.Autor,
                Produtora = filme.Produtora
            };
            await _filmeRepository.Inserir(filmeInsert);
            return new FilmeViewModel
            {
                Id = Guid.NewGuid(),
                Nome = filme.Nome,
                Categoria = filme.Categoria,
                Autor = filme.Autor,
                Produtora = filme.Produtora
            };
        }
        public async Task Atualizar(Guid id, FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepository.Obter(id);
            if (entidadeFilme == null)
                throw new FilmeJaCadastradoException();
            entidadeFilme.Nome = filme.Nome;
            entidadeFilme.Categoria = filme.Categoria;
            entidadeFilme.Autor = filme.Autor;
            entidadeFilme.Produtora = filme.Produtora;            

            await _filmeRepository.Atualizar(entidadeFilme);
        }
        public async Task Atualizar(Guid id)
        {
            var entidadeFilme = await _filmeRepository.Obter(id);
            if (entidadeFilme == null)
                throw new FilmeJaCadastradoException();

             await _filmeRepository.Atualizar(entidadeFilme);
        }
        public async Task Remover(Guid id)
        {
            var filme = await _filmeRepository.Obter(id);
            if (filme == null)
                throw new FilmeJaCadastradoException();
            await _filmeRepository.Remover(id);
        }
        public void Dispose()
        {
            _filmeRepository?.Dispose();
        }
    }
}
