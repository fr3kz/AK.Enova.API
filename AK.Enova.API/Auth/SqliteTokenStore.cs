using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    using Microsoft.Data.Sqlite;

    namespace AK.Enova.API
    {
        public sealed class SqliteTokenStore : IEnovaTokenStore
        {
            private readonly string _connectionString;

            public SqliteTokenStore(string filePath)
            {
                _connectionString = $"Data Source={filePath}";
                Init();
            }

            private void Init()
            {
                using var con = new SqliteConnection(_connectionString);
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText =
                """
            CREATE TABLE IF NOT EXISTS access_tokens (
                token TEXT PRIMARY KEY,
                database TEXT NOT NULL,
                created TEXT NOT NULL
            );
            """;
                cmd.ExecuteNonQuery();
            }

            public EnovaAccessToken Create(string database)
            {
                var token = new EnovaAccessToken
                {
                    Database = database
                };

                using var con = new SqliteConnection(_connectionString);
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText =
                """
            INSERT INTO access_tokens(token, database, created)
            VALUES ($t, $d, $c);
            """;

                cmd.Parameters.AddWithValue("$t", token.Token);
                cmd.Parameters.AddWithValue("$d", token.Database);
                cmd.Parameters.AddWithValue("$c", token.Created.ToString("O"));

                cmd.ExecuteNonQuery();

                return token;
            }

            public EnovaAccessToken? Validate(string token)
            {
                using var con = new SqliteConnection(_connectionString);
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText =
                """
            SELECT token, database, created
            FROM access_tokens
            WHERE token = $t;
            """;

                cmd.Parameters.AddWithValue("$t", token);

                using var r = cmd.ExecuteReader();

                if (!r.Read())
                    return null;

                return new EnovaAccessToken
                {
                    Token = r.GetString(0),
                    Database = r.GetString(1),
                    Created = DateTime.Parse(r.GetString(2))
                };
            }

            public void Revoke(string token)
            {
                using var con = new SqliteConnection(_connectionString);
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandText =
                """
            DELETE FROM access_tokens WHERE token = $t;
            """;

                cmd.Parameters.AddWithValue("$t", token);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
