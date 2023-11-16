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
            string apiKey = ""; // Write the apiKey you received from our SMS service in this section."
            string hash = ""; // Write the hash you received from our SMS service in this section."
            string senderName = ""; // Write the Username you received from our SMS service in this section."
            List<string> phoneNumbers = new List<string> { "" }; // You can write the phone numbers to which you want to send SMS messages in this section.
            string smsContent = "this is a test message";

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
                        Console.WriteLine("SMS sent successfully.");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send SMS. HTTP error code: {response.StatusCode}");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }
    }
}
