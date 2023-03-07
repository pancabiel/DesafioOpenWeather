namespace DesafioOpenWeather.Model
{
    public class Clima
    {
        public int Id { get; set; }
        public string? Cidade { get; set; }
        public double? Temperatura { get; set;}
        public DateTimeOffset? Horario { get; set; }
    }
}
