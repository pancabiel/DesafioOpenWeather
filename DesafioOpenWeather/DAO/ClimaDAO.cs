using DesafioOpenWeather.Database;
using DesafioOpenWeather.Model;
using DesafioOpenWeather.Model.DTO;
using DesafioOpenWeather.Util;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DesafioOpenWeather.DAO
{
    public class ClimaDAO
    {
        private LocalContextDb _context;
        private ILogger<OpenWeatherService> _logger;

        public ClimaDAO(LocalContextDb context, ILogger<OpenWeatherService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Clima> Buscar(string? dataInicio, string? dataFinal, string? cidade)
        {
            try
            {
                IEnumerable<Clima> query = _context.Climas;

                if (dataInicio != null)
                    query = query.Where(c => c.Horario >= DateTimeOffset.ParseExact(dataInicio, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));

                if (dataFinal != null)
                    query = query.Where(c => c.Horario <= DateTimeOffset.ParseExact(dataFinal, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));

                if (cidade != null)
                    query = query.Where(c => c.Cidade.ToLower() == cidade.ToLower());

                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar as informações de clima");
                throw;
            }
        }

        public Clima Adicionar(ClimaDTO climaDTO)
        {
            Clima clima = MapearClimaDTOParaClima(climaDTO);
            _context.AdicionarClima(clima);
            return clima;
        }

        private Clima MapearClimaDTOParaClima(ClimaDTO climaDTO)
        {
            Clima clima = new()
            {
                Temperatura = climaDTO?.main?.temp,
                Cidade = climaDTO?.name,
                Id = climaDTO is null ? 0 : climaDTO.id.GetValueOrDefault(),
                Horario = DateTime.Now
            };


            return clima;
        }
    }
}
