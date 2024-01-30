using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rainfall.Model;
using Rainfall.Service.DomainModels;
using Rainfall.Service.Interface;
using Rainfall.Service.MapperConfigs;

namespace Rainfall.Service
{
    public class RainfallService : IRainfallService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper = MapperConfig.InitializeRainfallReadingMapper;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTime
        };


        public RainfallService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }


        public async Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int? count = 10)
        {
            var url = $"id/stations/{stationId}/readings?_sorted&_limit={count}";
            var httpClient = _httpClientFactory.CreateClient(_configuration["Rainfall.Api:ClientName"]);

            string content = string.Empty;
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
                content = await response.Content.ReadAsStringAsync();

            var readings = JsonConvert.DeserializeObject<FloodMonitoringReadingsDomain>(content, serializerSettings) ?? new();

            return _mapper.Map<RainfallReadingResponse>(readings);
        }
    }
}
