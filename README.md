# TurkeyEarthquakeSmsSenderSln
// This program fetches earthquake data from the AFAD (Disaster and Emergency Management Authority) API
// at regular intervals and processes the information. If there's a new earthquake event, it sends an SMS
// notification. The earthquake data includes details such as location, magnitude, and date.
// The program uses asynchronous HTTP requests to get the earthquake data and utilizes JSON serialization
// to convert the API response into a list of Earthquake objects.
// The SendSms class handles the logic for sending SMS notifications. It uses the Iletimerkezi API and requires
// an API key and hash for authentication. The SMS content is set to "hello" in this example.
// The program runs in a continuous loop, checking for new earthquake events at regular intervals.
