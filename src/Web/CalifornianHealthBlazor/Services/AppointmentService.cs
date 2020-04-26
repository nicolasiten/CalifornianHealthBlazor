using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CalifornianHealth.Common.Exceptions;
using CalifornianHealth.Common.Models;
using CalifornianHealthBlazor.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CalifornianHealthBlazor.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public AppointmentService(
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("CalendarBaseUrl");
            _httpClient = httpClient;
        }

        public async Task<AppointmentModel> BuildAppointmentModelAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}GetAppointmentModel");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AppointmentModel>(responseString);
        }

        public IEnumerable<string> ValidateAppointment(AppointmentModel appointmentModel)
        {
            List<string> errors = new List<string>();

            // validation
            if (appointmentModel.SelectedConsultantId < 1)
            {
                errors.Add("Please select Consultant");
            }

            if (appointmentModel.SelectedPatientId < 1)
            {
                errors.Add("Please select Patient");
            }

            if (appointmentModel.SelectedDate < DateTime.UtcNow.Date)
            {
                errors.Add("It's not possible to book an appointment in the past. Please select today or a future date");
            }

            if (appointmentModel.SelectedTime == string.Empty)
            {
                errors.Add("Please select a time");
            }

            return errors;
        }

        public async Task<IEnumerable<string>> GetFreeAppointmentTimesAsync(DateTime date, int consultantId)
        {
            var response = await _httpClient.GetAsync(
                $"{_baseUrl}GetFreeAppointmentTimes?date={date.ToString("s", CultureInfo.InvariantCulture)}&consultantId={consultantId}");
            var responseString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseString))
            {
                return new string[0];
            }

            return JsonConvert.DeserializeObject<List<string>>(responseString);
        }

        public async Task SaveAppointmentAsync(AppointmentModel appointmentModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(appointmentModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new CalifornianHealthException(await response.Content.ReadAsStringAsync());
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
