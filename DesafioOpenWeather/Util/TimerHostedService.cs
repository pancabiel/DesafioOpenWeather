using System.Drawing;

namespace DesafioOpenWeather.Util
{
    public class TimerHostedService : IHostedService
    {
        private OpenWeatherService _openWeatherService;

        public TimerHostedService(OpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            new Task(async () =>
            {
                var timer = new PeriodicTimer(TimeSpan.FromMinutes(2));

                while (await timer.WaitForNextTickAsync())
                {
                    await _openWeatherService.BuscarTemperaturaAtualAsync();
                }
            }).Start();

            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
