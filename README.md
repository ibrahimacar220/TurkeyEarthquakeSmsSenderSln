# TurkeyEarthquakeSmsSenderSln

 
This program fetches earthquake data from the AFAD (Disaster and Emergency Management Authority) API at regular intervals and processes the information. 

It considers Kayseri and its surroundings as the main center, checking for earthquake events in a specific geographical region. If there is a new earthquake event, it sends an SMS notification.

The earthquake data includes details such as location, magnitude, and date. The program uses asynchronous HTTP requests to retrieve earthquake data and employs JSON serialization to convert the API response into a list of Earthquake objects. The SendSms class manages the logic for sending SMS notifications. It utilizes the Iletimerkezi API and requires an API key and hash for authentication. 

The SMS content is set to "hello" in this example. The program runs in a continuous loop, periodically checking for new earthquake events and providing information to the user.








afad doc: 
https://deprem.afad.gov.tr/event-service

sms service doc: https://www.toplusmsapi.com/en/sms/send/json/
![image](https://github.com/ibrahimacar220/TurkeyEarthquakeSmsSenderSln/assets/91982157/fb67386a-18d8-458c-a684-f798c88de717)

