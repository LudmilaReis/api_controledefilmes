using ApiControleDeFilmes.Entites;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public class FilmeSqlServerRepository : IFilmeRepository
    {
        private readonly SqlConnection sqlConnection;

        public FilmeSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Movie order by id offset { ((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Categoria = (string)sqlDataReader["Categoria"],
                    Autor = (string)sqlDataReader["Autor"],
                    Produtora = (string)sqlDataReader["Produtora"]
                });
            }

            await sqlConnection.CloseAsync();
            return filmes;
        }

        public async Task<Filme> Obter(Guid id)
        {
            Filme filme = null;

            var comando = $"select * from Movie where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filme = (new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Categoria = (string)sqlDataReader["Categoria"],
                    Autor = (string)sqlDataReader["Autor"],
                    Produtora = (string)sqlDataReader["Produtora"]
                });
            }

            await sqlConnection.CloseAsync();
            return filme;
        }

        public async Task<List<Filme>> Obter(string nome, string categoria, string autor, string produtora)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Movie where Nome = '{nome}', Categoria = '{categoria}', Autor = '{autor}' and Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Categoria = (string)sqlDataReader["Categoria"],
                    Autor = (string)sqlDataReader["Autor"],
                    Produtora = (string)sqlDataReader["Produtora"]
                });;
            }

            await sqlConnection.CloseAsync();
            return filmes;
        }

        public async Task Inserir(Filme filme)
        {
            var comando = $"insert Movie (Id, Nome, Categoria, Autor, Produtora) values ('{filme.Id}', '{filme.Nome}', '{filme.Categoria}', '{filme.Autor}', '{filme.Produtora}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Filme filme)
        {
            var comando = $"update Movie set Nome = '{filme.Nome}', Categoria = '{filme.Categoria}', Autor = '{filme.Autor}', Produtora = '{filme.Produtora}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Movie where id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
