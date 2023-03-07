using System.Text.Json.Serialization;

namespace DesafioOpenWeather.Model.DTO
{
    public class ClimaDTO
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public Main? main { get; set; }
    }

    public class Main
    {
        public double? temp;

    }
}
