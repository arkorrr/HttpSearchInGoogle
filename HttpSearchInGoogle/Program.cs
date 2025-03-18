using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HttpSearchInGoogle
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Введите текст для поиска: ");
            string query = Console.ReadLine();

            string apiKey = " "; // Ключ API
            string searchEngineId = " "; // Идентификатор поисковой системы

            string apiUrl = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={Uri.EscapeDataString(query)}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);

                    JObject json = JObject.Parse(response);
                    var items = json["items"];

                    Console.WriteLine("\nРезультаты поиска:");
                    int i = 1;
                    foreach (var item in items)
                    {
                        Console.WriteLine($"Результат №{i++}");
                        string title = item["title"]?.ToString();
                        string link = item["link"]?.ToString();
                        string snippet = item["snippet"]?.ToString();

                        Console.WriteLine($"Заголовок: {title}");
                        Console.WriteLine($"Ссылка: {link}\n");
                        Console.WriteLine($"Описание: {snippet}\n");
                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
