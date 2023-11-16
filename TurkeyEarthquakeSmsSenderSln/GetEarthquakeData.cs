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
                    string _apiUrl = $"https://earthquake.afad.gov.tr/api/v1/event/filter?minlat=36&maxlat=41&minlon=32&maxlon=39&start={DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}&end={DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss")}&format=json&minmag=0&maxmag=7&magtype=ML";

                    HttpResponseMessage response = await _client.GetAsync(_apiUrl);

                    // Continue if a successful response is received
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the API response as JSON
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        // If the current and previous API responses are not equal, present the new data to the user
                        if (apiResponse != _previousApiResponse)
                        {
                            _previousApiResponse = apiResponse;
                            List<Earthquake> earthquakes = JsonSerializer.Deserialize<List<Earthquake>>(apiResponse);

                            // Present processed data to the user
                            if (earthquakes != null)
                            {
                                foreach (var earthquake in earthquakes)
                                {
                                    Console.WriteLine($"Earthquake Information: {earthquake.location}, Magnitude: {earthquake.magnitude}, Date: {earthquake.date}");
                                    SendSms sendSms = new SendSms();
                                    string alertMessage = $"{earthquake.location} experienced an earthquake with a magnitude of {earthquake.magnitude} on {earthquake.date}.";
                                    sendSms.Send(alertMessage);
                                }
                            }
                        }

                        // Wait for a specific period before sending another request
                        Thread.Sleep(6000); // Delayed for 6 seconds in the example, adjust the time according to your needs
                    }
                    else
                    {
                        Console.WriteLine($"Failed response from the API. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }

}
