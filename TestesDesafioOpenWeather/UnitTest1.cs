using DesafioOpenWeather.DAO;
using DesafioOpenWeather.Database;
using DesafioOpenWeather.Model;
using Moq;

namespace TestesDesafioOpenWeather
{
    public class UnitTest1
    {
        [Fact]
        public void Teste_Buscar()
        {
            // Arrange
            var clima1 = new Clima { Id = 1, Cidade = "Curitiba", Temperatura = 25.0, Horario = new DateTimeOffset(2022, 3, 6, 12, 0, 0, TimeSpan.Zero) };
            var clima2 = new Clima { Id = 2, Cidade = "Porto Alegre", Temperatura = 30.0, Horario = new DateTimeOffset(2022, 3, 8, 12, 0, 0, TimeSpan.Zero) };
            var clima3 = new Clima { Id = 3, Cidade = "Curitiba", Temperatura = 20.0, Horario = new DateTimeOffset(2022, 3, 9, 12, 0, 0, TimeSpan.Zero) };
            var clima4 = new Clima { Id = 4, Cidade = "Florianópolis", Temperatura = 23.0, Horario = new DateTimeOffset(2022, 3, 8, 12, 0, 0, TimeSpan.Zero) };
            var clima5 = new Clima { Id = 5, Cidade = "Curitiba", Temperatura = 32.0, Horario = new DateTimeOffset(2022, 3, 7, 12, 0, 0, TimeSpan.Zero) };


            var climaList = new List<Clima> { clima1, clima2, clima3, clima4, clima5 };
            var dataInicio = "07/03/2022 00:00";
            var dataFinal = "08/03/2022 23:59";
            var cidade = "curitiba";

            LocalContextDb.Instance.Climas = climaList;

            var climaService = new ClimaDAO(LocalContextDb.Instance);

            // Act
            var result = climaService.Buscar(dataInicio, dataFinal, cidade);

            // Assert
            Assert.Single(result);
            Assert.Contains(clima5, result);
            Assert.DoesNotContain(clima1, result);
            Assert.DoesNotContain(clima2, result);
            Assert.DoesNotContain(clima3, result);
            Assert.DoesNotContain(clima4, result);
        }
    }
}