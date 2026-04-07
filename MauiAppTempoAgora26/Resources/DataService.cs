using MauiAppTempoAgora26.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora26.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&lang=pt_br&appid={chave}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.StatusCode == HttpStatusCode.NotFound)
                        throw new Exception("Cidade não encontrada.");

                    if (!resp.IsSuccessStatusCode) throw new Exception("Erro ao buscar dados da previsão.");

                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = DateTimeOffset
                    .FromUnixTimeSeconds((long)rascunho["sys"]["sunrise"]).LocalDateTime;
                    DateTime sunset = DateTimeOffset
                        .FromUnixTimeSeconds((long)rascunho["sys"]["sunset"]).LocalDateTime;

                    t = new Tempo
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString("HH:mm"),
                        sunset = sunset.ToString("HH:mm"),
                    }; // Fecha obj do Tempo.
                } // fecha laço using

            }
            catch (HttpRequestException)
            {
                throw new Exception("Sem conexão com a internet.");

            }

            return t;
        }
    }
}