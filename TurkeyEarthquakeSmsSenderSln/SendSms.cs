using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkeyEarthquakeSmsSenderSln
{
    public class SendSms
    {
        public async Task<int> Send(string message)
        {
            string apiKey = ""; // bu kısma sms servisinizden aldığınız apiKeyi yazın
            string hash = ""; // bu kısma sms servisinizden aldığınız apiKeyi yazın
            string senderName = ""; // bu kısma sms servisinizdeki kullanıcı adınızı yazın
            List<string> phoneNumbers = new List<string> { "" }; // bu kısma hangi telefon numaralarına sms  göndermek istediğiniz yazabilirsiniz.
            string smsContent = "Bu bir deneme mesajıdır.";

            string apiUrl = "https://api.iletimerkezi.com/v1/send-sms/json";

            var requestData = new
            {
                request = new
                {
                    authentication = new
                    {
                        key = apiKey,
                        hash = hash
                    },
                    order = new
                    {
                        sender = "APITEST",
                        sendDateTime = new object[] { },
                        iys = "0",
                        iysList = "BIREYSEL",
                        message = new
                        {
                            text = message,
                            receipents = new
                            {
                                number = phoneNumbers
                            }
                        }
                    }
                }
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var jsonRequest = JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("SMS başarıyla gönderildi.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine($"SMS gönderme başarısız. HTTP hata kodu: {response.StatusCode}");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return 0;
            }
        }
    }
}
