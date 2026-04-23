using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Diagnostics;

namespace ConsolTest
{
    internal class Program
    {
        private const string ApiUrl = "http://localhost:5138/api/products/";
        private static readonly HttpClient _httpClient = new HttpClient();
        static async Task Main(string[] args)
        {

            string endpoint = "test-sync";
            int requestCount = 200; // Aynı anda atılacak istek sayısı

            Console.WriteLine($"{requestCount} adet istek {endpoint} ucuna gönderiliyor...");

            var stopwatch = Stopwatch.StartNew();
            var tasks = new List<Task<HttpResponseMessage>>();

            // 200 adet isteği eşzamanlı olarak fırlatıyoruz
            for (int i = 0; i < requestCount; i++)
            {
                tasks.Add(_httpClient.GetAsync(ApiUrl + endpoint));
            }

            // Tüm isteklerin cevap dönmesini bekle
            await Task.WhenAll(tasks);
            stopwatch.Stop();

            Console.WriteLine($"Tüm işlemler tamamlandı!");
            Console.WriteLine($"Toplam geçen süre: {stopwatch.Elapsed.TotalSeconds:F2} saniye");
            Console.ReadLine();

        }
    }
}
