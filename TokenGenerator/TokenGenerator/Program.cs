using Microsoft.Data.Sqlite;
using System;
using System.IO;

var dbFile = Path.Combine(
    AppContext.BaseDirectory,
    "enova_tokens.db"
);

EnsureDatabase(dbFile);

using var conn = new SqliteConnection($"Data Source={dbFile}");
conn.Open();

while (true)
{
    Console.Clear();

    Console.WriteLine("=================================");
    Console.WriteLine(" ENOVA ACCESS TOKEN TOOL");
    Console.WriteLine("=================================");
    Console.WriteLine("1 - Lista tokenów");
    Console.WriteLine("2 - Wygeneruj nowy");
    Console.WriteLine("3 - Usuń token");
    Console.WriteLine("0 - Wyjście");
    Console.WriteLine();

    Console.Write("Wybierz opcję: ");
    var key = Console.ReadLine();

    Console.WriteLine();

    switch (key)
    {
        case "1":
            ListTokens(conn);
            break;

        case "2":
            CreateToken(conn);
            break;

        case "3":
            DeleteToken(conn);
            break;

        case "0":
            return;
    }

    Console.WriteLine();
    Console.WriteLine("Enter aby wrócić do menu...");
    Console.ReadLine();
}

static void EnsureDatabase(string dbFile)
{
    if (File.Exists(dbFile))
        return;

    using var conn = new SqliteConnection($"Data Source={dbFile}");
    conn.Open();

    var cmd = conn.CreateCommand();
    cmd.CommandText =
    """
    CREATE TABLE access_tokens (
        token TEXT PRIMARY KEY,
        database TEXT NOT NULL,
        created TEXT NOT NULL
    );
    """;

    cmd.ExecuteNonQuery();
}

static void ListTokens(SqliteConnection conn)
{
    var cmd = conn.CreateCommand();
    cmd.CommandText =
        "SELECT token, database, created FROM access_tokens ORDER BY created DESC";

    using var r = cmd.ExecuteReader();

    Console.WriteLine("TOKEN | DATABASE | CREATED");
    Console.WriteLine("--------------------------------------------------------");

    while (r.Read())
    {
        Console.WriteLine(
            $"{r.GetString(0)} | {r.GetString(1)} | {r.GetString(2)}"
        );
    }
}

static void CreateToken(SqliteConnection conn)
{
    Console.Write("Nazwa bazy (np. API): ");
    var db = Console.ReadLine() ?? "API";

    var token = Guid.NewGuid().ToString("N");

    var cmd = conn.CreateCommand();
    cmd.CommandText =
        "INSERT INTO access_tokens(token, database, created) VALUES (@t,@d,@c)";

    cmd.Parameters.AddWithValue("@t", token);
    cmd.Parameters.AddWithValue("@d", db);
    cmd.Parameters.AddWithValue("@c", DateTime.UtcNow.ToString("O"));

    cmd.ExecuteNonQuery();

    Console.WriteLine();
    Console.WriteLine("NOWY ACCESS TOKEN:");
    Console.WriteLine(token);
}

static void DeleteToken(SqliteConnection conn)
{
    Console.Write("Token do usunięcia: ");
    var token = Console.ReadLine();

    var cmd = conn.CreateCommand();
    cmd.CommandText = "DELETE FROM access_tokens WHERE token=@t";
    cmd.Parameters.AddWithValue("@t", token);

    var rows = cmd.ExecuteNonQuery();

    Console.WriteLine(
        rows > 0
            ? "Token usunięty."
            : "Nie znaleziono tokenu."
    );
}

