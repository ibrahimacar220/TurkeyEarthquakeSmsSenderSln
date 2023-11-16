using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkeyEarthquakeSmsSenderSln
{
    using System;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using TurkeyEarthquakeSmsSenderSln.Models;

    class GetEarthQuakeData
    {

        private static readonly HttpClient _client = new HttpClient();

        private static string _previousApiResponse = string.Empty;

        public static async Task Get(string[] args)
        {
            while (true)
            {
                try
                {
                    string _apiUrl = $"https://deprem.afad.gov.tr/apiv2/event/filter?minlat=36&maxlat=41&minlon=32&maxlon=39&start={DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}&end={DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss")}&format=json&minmag=0&maxmag=7&magtype=ML";

                    HttpResponseMessage response = await _client.GetAsync(_apiUrl);

                    // Başarılı bir yanıt alındıysa devam et
                    if (response.IsSuccessStatusCode)
                    {
                        // API yanıtını JSON olarak oku
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        // Eğer mevcut ve önceki API yanıtları eşit değilse yeni veriyi kullanıcıya sun
                        if (apiResponse != _previousApiResponse)
                        {
                            List<Earthquake> earthquakes = JsonSerializer.Deserialize<List<Earthquake>>(apiResponse);

                            // İşlenmiş veriyi kullanıcıya sun
                            if (earthquakes != null)
                            {
                                foreach (var earthquake in earthquakes)
                                {
                                    Console.WriteLine($"Earthquake Information: {earthquake.location}, Magnitude: {earthquake.magnitude}, Date: {earthquake.date}");
                                    SendSms sendSms = new SendSms();
                                    string alertMessage = $"{earthquake.location} ilimizde {earthquake.magnitude} büyüklüğünde bir deprem olmuştur. tarihi: {earthquake.date}";
                                    sendSms.Send(alertMessage);
                                }
                            }
                        }

                        // Belirli bir süre bekleyerek tekrar istek gönder
                        Thread.Sleep(6000); // Örnekte 6 saniye bekletildi, süreyi ihtiyacınıza göre ayarlayabilirsiniz
                    }
                    else
                    {
                        Console.WriteLine($"API'den başarısız yanıt alındı. Durum kodu: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                }
            }
        }
    }

}
