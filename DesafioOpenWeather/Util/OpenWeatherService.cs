using DesafioOpenWeather.DAO;
using DesafioOpenWeather.Database;
using DesafioOpenWeather.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DesafioOpenWeather.Util
{
    public class OpenWeatherService
    {
        private readonly HttpClient _httpClient;
        public ILogger<OpenWeatherService> _logger { get; set; }
        public ClimaDAO _climaDAO { get; }

        public OpenWeatherService(HttpClient httpClient, ILogger<OpenWeatherService> logger, ClimaDAO climaDAO)
        {
            _httpClient = httpClient;
            _logger = logger;
            _climaDAO = climaDAO;
        }

        public async Task<bool> BuscarTemperaturaAtualAsync()
        {
            try
            {
                var responsePortoAlegre = await _httpClient.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=-30.034647&lon=-51.217659&APPID=e6a5fb3ef89592909d4058b92e9d1b83&units=metric");
                var responseFlorianopolis = await _httpClient.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=-27.594870&lon=-48.548222&APPID=e6a5fb3ef89592909d4058b92e9d1b83&units=metric");
                var responseCuritiba = await _httpClient.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=-25.480877&lon=-49.304424&APPID=e6a5fb3ef89592909d4058b92e9d1b83&units=metric");

                var deserializeOptions = new JsonSerializerOptions { IncludeFields = true };
                var climas = new List<ClimaDTO>
                {
                    JsonSerializer.Deserialize<ClimaDTO>(await responsePortoAlegre.Content.ReadAsStringAsync(), deserializeOptions),
                    JsonSerializer.Deserialize<ClimaDTO>(await responseFlorianopolis.Content.ReadAsStringAsync(), deserializeOptions),
                    JsonSerializer.Deserialize<ClimaDTO>(await responseCuritiba.Content.ReadAsStringAsync(), deserializeOptions)
                };

                foreach (var clima in climas)
                {
                    if (clima != null)
                    {
                        _climaDAO.Adicionar(clima);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar as informações da API");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar a temperatura atual");
                return false;
            }

            return true;
        }
    }
}
