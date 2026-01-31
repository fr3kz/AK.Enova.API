using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AK.Enova.API.Auth
{
    public static class EnovaTokenStore
    {
        private static readonly string Dir =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "AK.Enova.API");

        private static readonly string FilePath =
            Path.Combine(Dir, "tokens.json");

        private static Dictionary<string, string> _tokens;

        static EnovaTokenStore()
        {
            Directory.CreateDirectory(Dir);

            if (File.Exists(FilePath))
                _tokens = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    File.ReadAllText(FilePath)) ?? new();
            else
                _tokens = new();
        }

        public static string CreateToken(string database)
        {
            var token = Guid.NewGuid().ToString("N");
            _tokens[database] = token;
            Save();
            return token;
        }

        public static bool Validate(string token)
            => _tokens.Values.Contains(token);

        private static void Save()
        {
            File.WriteAllText(
                FilePath,
                JsonSerializer.Serialize(_tokens, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }
    }
}
