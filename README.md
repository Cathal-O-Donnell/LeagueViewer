
# LeagueViewer

LeagueViewer is a ASP.NET CORE project that displays current league information using data from RapidApi.

To run this site you will need an API key from https://rapidapi.com/api-sports/api/api-football (see steps below)

![league](https://i.imgur.com/9VnZu0U.png)

![enter image description here](https://i.imgur.com/jkyqdoz.png)

![enter image description here](https://i.imgur.com/gwsaCX4.png)

## Note

If you get the free version of the api there is a limit on the number of request that can be sent in a period of time, so if you get the basic version you may have to delay in sending requests or you may see errors.

## Steps to run

Create an account on https://rapidapi.com/api-sports/api/api-football.

Once an account has been created, enter your ApiKey value into the appsettings.json file under the "ApiCredentials" section

```json
 "ApiCredentials": {
    "RapidFootballUrl": "https://api-football-v1.p.rapidapi.com/",
    "ApiHost": "api-football-v1.p.rapidapi.com",
    "ApiKey": " - ENTER KEY HERE -"
  },
```
