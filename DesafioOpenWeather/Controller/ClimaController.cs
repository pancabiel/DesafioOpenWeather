using DesafioOpenWeather.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;

namespace DesafioOpenWeather.Controller
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClimaController : ControllerBase
    {
        private ClimaDAO _climaDAO;

        public ClimaController(ClimaDAO climaDAO)
        {
            _climaDAO = climaDAO;
        }

        [HttpGet]
        [SwaggerOperation("Busca de histórico de temperaturas", "Função que possibilita buscar as temperaturas gravadas a cada 2 minutos. Cidades com informações disponíveis: \"Porto Alegre\", \"Florianópolis\" e \"Curitiba\".")]
        public IActionResult GetClimas(
            [FromQuery]
            [SwaggerParameter("Formato esperado \"dd/MM/yyyy HH:mm\" (Exemplo: 19/03/2023 21:17)")]
            string? dataInicio,
            [FromQuery]
            [SwaggerParameter("Formato esperado \"dd/MM/yyyy HH:mm\" (Exemplo: 21/04/2023 23:51)")]
            string? dataFinal,
            [FromQuery]
            [SwaggerParameter("Cidades disponíveis: \"Porto Alegre\", \"Florianópolis\" e \"Curitiba\"")]
            string? cidade)
        {
            try
            {
                return Ok(_climaDAO.Buscar(dataInicio, dataFinal, cidade));
            }
            catch (Exception ex)
            {
                return Problem(title: "Ocorreu um erro ao buscar os valores de clima", detail: ex.Message);
            }
        }
    }
}
