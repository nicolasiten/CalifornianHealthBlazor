using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CalifornianHealth.Common.Models;
using CalifornianHealthBlazor.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CalifornianHealthBlazor.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public ConsultantService(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("CalendarBaseUrl");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ConsultantModel>> GetConsultantsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}GetConsultants");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ConsultantModel>>(responseString);
        }
    }
}
