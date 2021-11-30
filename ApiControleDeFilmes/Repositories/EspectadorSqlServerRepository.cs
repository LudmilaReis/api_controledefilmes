using ApiControleDeFilmes.Entites;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Repositories
{
    public class EspectadorSqlServerRepository : IEspectadorRepository
    {
        private readonly SqlConnection sqlConnection;

        public EspectadorSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Espectador>> Obter(int pagina, int quantidade)
        {
            var espectadores = new List<Espectador>();

            var comando = $"select * from Movie order by id offset { ((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                espectadores.Add(new Espectador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Sobrenome = (string)sqlDataReader["Sobrenome"],
                    Telefone = (string)sqlDataReader["Telefone"]                   
                });
            }

            await sqlConnection.CloseAsync();
            return espectadores;
        }

        public async Task<Espectador> Obter(Guid id)
        {
            Espectador espectador = null;

            var comando = $"select * from Movie where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                espectador = new Espectador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Sobrenome = (string)sqlDataReader["Sobrenome"],
                    Telefone = (string)sqlDataReader["Telefone"]
                };
            }

            await sqlConnection.CloseAsync();
            return espectador;
        }

        public async Task<List<Espectador>> Obter(string nome, string sobrenome, string telefone)
        {
            var espectadores = new List<Espectador>();

            var comando = $"select * from Movie where Nome = '{nome}', Sobrenome = '{sobrenome}' and Telefone = '{telefone}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                espectadores.Add(new Espectador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Sobrenome = (string)sqlDataReader["Sobrenome"],
                    Telefone = (string)sqlDataReader["Telefone"]
                });;
            }

            await sqlConnection.CloseAsync();
            return espectadores;
        }

        public async Task Inserir(Espectador espectador)
        {
            var comando = $"insert Movie (Id, Nome, Sobrenome, Telefone) values ('{espectador.Id}', '{espectador.Nome}', '{espectador.Sobrenome}', '{espectador.Telefone}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Espectador espectador)
        {
            var comando = $"insert Movie (Id, Nome, Sobrenome, Telefone) values ('{espectador.Id}', '{espectador.Nome}', '{espectador.Sobrenome}', '{espectador.Telefone}')";

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
