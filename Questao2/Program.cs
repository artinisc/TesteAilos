using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class Program
{
    private const string _urlApi = "https://jsonmock.hackerrank.com/api/football_matches";

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var urlTime1 = $"{_urlApi}?year={year}&team1={team}";
        var urlTime2 = $"{_urlApi}?year={year}&team2={team}";

        var paginasJogandoEmCasa = ObterNumeroDePaginas(team, urlTime1, EnumTimeCasaVisitante.Time1);
        var paginasJogandoComoVisitante = ObterNumeroDePaginas(team, urlTime2, EnumTimeCasaVisitante.Time2);

        var totalGols = 0;

        for (int i = 1; i <= paginasJogandoEmCasa; i++)
        {
            totalGols += ObterGolsTime(team, $"{urlTime1}&page={i}", EnumTimeCasaVisitante.Time1);
        }

        for (int i = 1; i <= paginasJogandoEmCasa; i++)
        {
            totalGols += ObterGolsTime(team, $"{urlTime2}&page={i}", EnumTimeCasaVisitante.Time2);
        }

        return totalGols;
    }

    public static int ObterNumeroDePaginas(string team, string url, EnumTimeCasaVisitante timeCasaVisitante)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                JObject jsonResponse = JObject.Parse(responseBody);

                var paginas = (int)jsonResponse["total_pages"];

                return paginas;
            }
            catch (Exception ex)
            {
                var casaVisitante = timeCasaVisitante == EnumTimeCasaVisitante.Time1 ? "jogando em casa" : "jogando como visitante";
                Console.WriteLine($"Erro ao fazer requisição so time {team}, quando {timeCasaVisitante}: {ex.Message}");
                return 0;
            }
        }
    }

    public static int ObterGolsTime(string team, string url, EnumTimeCasaVisitante timeCasaVisitante)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                JObject jsonResponse = JObject.Parse(responseBody);

                var partidas = jsonResponse["data"];
                var totalGols = 0;

                foreach (var partida in partidas)
                {
                    if (timeCasaVisitante == EnumTimeCasaVisitante.Time1 && partida["team1"].ToString() == team)
                    {
                        totalGols += (int)partida["team1goals"];
                    }
                    else if (timeCasaVisitante == EnumTimeCasaVisitante.Time2 && partida["team2"].ToString() == team)
                    {
                        totalGols += (int)partida["team2goals"];
                    }
                }

                return totalGols;
            }
            catch (Exception ex)
            {
                var casaVisitante = timeCasaVisitante == EnumTimeCasaVisitante.Time1 ? "jogando em casa" : "jogando como visitante";
                Console.WriteLine($"Erro ao fazer requisição so time {team}, quando {timeCasaVisitante}: {ex.Message}");
                return 0;
            }
        }
    }

    public enum EnumTimeCasaVisitante
    {
        Time1 = 1,
        Time2 = 2
    }
}