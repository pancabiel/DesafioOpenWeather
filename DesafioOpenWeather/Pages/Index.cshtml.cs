using DesafioOpenWeather.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Text;
using System.Globalization;

public class IndexModel : PageModel
{
    [BindProperty(Name = "CidadeQuery")]
    public string CidadeQuery { get; set; }

    [BindProperty(Name = "DataInicioQuery")]
    public DateTime? DataInicioQuery { get; set; }

    [BindProperty(Name = "DataFinalQuery")]
    public DateTime? DataFinalQuery { get; set; }

    [BindProperty(Name = "Climas")]
    public List<Clima> Climas { get; set; }

    private readonly HttpClient httpClient;

    public IndexModel(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        Climas = new List<Clima>();
    }

    public async Task OnGetAsync()
    {
        Climas = await GetClimasAsync(null, null, null);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            Climas = await GetClimasAsync(CidadeQuery, DataInicioQuery, DataFinalQuery);
        }
        catch (Exception)
        {
            throw;
        }

        return Page();
    }

    private async Task<List<Clima>> GetClimasAsync(string cidadeQuery, DateTime? dataInicioQuery, DateTime? dataFinalQuery)
    {
        var urlBuilder = new StringBuilder("https://localhost:7262/api/Clima");

        if (!string.IsNullOrEmpty(cidadeQuery))
        {
            urlBuilder.Append("?cidade=").Append(cidadeQuery);
        }

        if (dataInicioQuery.HasValue)
        {
            var dataInicioString = dataInicioQuery.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            urlBuilder.Append(urlBuilder.ToString().Contains("?") ? "&" : "?").Append("dataInicio=").Append(dataInicioString);
        }

        if (dataFinalQuery.HasValue)
        {
            var dataFinalString = dataFinalQuery.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            urlBuilder.Append(urlBuilder.ToString().Contains("?") ? "&" : "?").Append("dataFinal=").Append(dataFinalString);
        }

        var response = await httpClient.GetAsync(urlBuilder.ToString());
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<List<Clima>>();
        return result ?? new List<Clima>();
    }
}
