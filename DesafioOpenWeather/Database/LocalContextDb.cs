using DesafioOpenWeather.Model;
using DesafioOpenWeather.Util;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DesafioOpenWeather.Database
{
    public class LocalContextDb
    {
        private const string PATH_JSON = "./database.json";
        
        private static LocalContextDb? _instance;
        public List<Clima> Climas = new List<Clima>();
        private static ILogger<LocalContextDb> _logger;

        public LocalContextDb(ILogger<LocalContextDb> logger)
        {
            _logger = logger;
            if (File.Exists(PATH_JSON))
            {
                var jsonText = File.ReadAllText(PATH_JSON);
                
                try
                {
                    var climas = JsonSerializer.Deserialize<List<Clima>>(jsonText);

                    if (climas != null) { Climas = climas; }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ocorreu um erro ao buscar as informações do arquivo JSON");
                    throw;
                }
            }
        }

        public static LocalContextDb Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalContextDb(_logger);
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        internal void AdicionarClima(Clima clima)
        {
            clima.Id = Climas.Any() ? Climas.Max(c => c.Id) + 1 : 1;
            Climas.Add(clima);
            Salvar();
        }

        private void Salvar()
        {
            var jsonText = JsonSerializer.Serialize(Climas);
            File.WriteAllText(PATH_JSON, jsonText);
        }

    }
}
