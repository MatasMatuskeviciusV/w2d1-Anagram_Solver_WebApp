using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AnagramSolver.Cli
{
    internal class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            int minUserLen = int.Parse(config["Settings:MinUserWordLength"]);
            var apiBaseUrl = config["Api:BaseUrl"];

            if (string.IsNullOrWhiteSpace(apiBaseUrl))
            {
                Console.WriteLine("Konfigūracijoje nenurodytas Base Url.");
                return;
            }

            var user = new UserProcessor(minUserLen);

            Console.WriteLine("Įveskite žodžius: ");
            string? input = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Nieko neįvesta");
                return;
            }

            if (!user.IsValid(input))
            {
                Console.WriteLine($"Įvestas per trumpas žodis. Minimalus ilgis: {minUserLen}");
                return;
            }

            _client.BaseAddress = new Uri(apiBaseUrl);

            var response = await _client.GetAsync($"/api/anagrams/{Uri.EscapeDataString(input)}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var results = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

            if(results.Count == 0)
            {
                Console.WriteLine("Anagramų nerasta.");
            }
            else
            {
                Console.WriteLine("Anagramos: ");
                foreach(var r in results)
                {
                    Console.WriteLine(r);
                }
            }
        }
    }
}