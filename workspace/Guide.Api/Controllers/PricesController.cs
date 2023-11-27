using Guide.Domain.Entities;
using Guide.Infra.Context;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Guide.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GuideContext _context;
        private object httpClient;
        public PricesController(GuideContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        private decimal? CalculaVariacao(decimal atual, decimal? anterior)
        {
            if (anterior.HasValue && anterior.Value != 0)
            {
                return ((atual - anterior.Value) / anterior.Value) * 100;
            }

            return null;
        }
        private DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dataInicial = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return dataInicial.AddSeconds(unixTimeStamp);
        }

        [HttpGet("getPrices/{symbol}")]
        public async Task<IActionResult> getPrices(string symbol)
        {
            try
            {
                string apiUrl = $"https://query2.finance.yahoo.com/v8/finance/chart/{symbol}?interval=1d&range=1y";
                using (HttpClient client = _httpClientFactory.CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JsonSerializerOptions options = new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var data = JsonSerializer.Deserialize<Root>(responseBody, options);
                        var itens = data.Chart.Result.First().Timestamp.Count;
                        for (var i = 0; i < itens; i++)
                        {
                            _context.PriceRecords.Add(new PriceRecord
                            {
                                Symbol = symbol,
                                Date = UnixTimeStampToDateTime(data.Chart.Result.First().Timestamp[i]),
                                OpenPrice = data.Chart.Result.First().Indicators.Quote.First().Open[i]
                            });
                        }
                        _context.SaveChanges();
                        return Ok(responseBody);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, $"Erro na requisição: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("calculateVariance/{symbol}")]
        public async Task<IActionResult> calculateVariance(string symbol)
        {
            var dataAtual = DateTime.Now;
            var trintadiasAtras = dataAtual.AddDays(-30);

            var httpClient = _httpClientFactory.CreateClient();

            var precos = _context.PriceRecords.Where(x => x.Symbol == symbol && x.Date > trintadiasAtras).OrderBy(x => x.Date).ToList();

            var result = new List<ResponsePricesResult>();

            var primeiroValor = precos.First();
            for (int i = 0; i < precos.Count(); i++)
            {
                var precoAtual = precos[i];
                var precoAnterior = i > 0 ? precos[i - 1] : null;

                var variacaoAnterior = CalculaVariacao(precoAtual.OpenPrice, precoAnterior?.OpenPrice);
                var variacaoPrimeiro = CalculaVariacao(precoAtual.OpenPrice, primeiroValor.OpenPrice);

                var apiResponse = new ResponsePricesResult
                {
                    NumDia = i + 1,
                    Data = precoAtual.Date,
                    ValorAbertura = precoAtual.OpenPrice,
                    ComparacaoAoDiaAnterior = variacaoAnterior,
                    ComparacaoAoPrimeiroDia = variacaoPrimeiro
                };

                result.Add(apiResponse);
            }

            return Ok(result);
        }
    }
}

