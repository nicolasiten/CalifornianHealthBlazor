using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Interfaces;
using CalifornianHealth.Common.Exceptions;
using CalifornianHealth.Common.Models;
using CalifornianHealthBlazor.Data.Entities;
using CalifornianHealthBlazor.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendar.Data
{
    public class AppointmentDataHandler : IAppointmentDataHandler
    {
        private readonly string _dbConnectionString;
        private readonly IList<TimeSlot> _timeSlots;

        public AppointmentDataHandler(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _dbConnectionString = configuration.GetConnectionString("DataConnection");

            _timeSlots = new List<TimeSlot>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var timeSlotRepository = scope.ServiceProvider.GetService<IAsyncRepository<TimeSlot>>();

                foreach (var timeSlot in timeSlotRepository.GetAllAsync().GetAwaiter().GetResult())
                {
                    _timeSlots.Add(timeSlot);
                }
            }
        }

        public async Task SaveAppointmentAsync(AppointmentModel appointmentModel)
        {
            int? timeSlotId = _timeSlots.SingleOrDefault(ts => ts.Time == appointmentModel.SelectedTime
                                                           && ts.ConsultantFk == appointmentModel.SelectedConsultantId
                                                           && ts.DayOfWeek == (int)appointmentModel.SelectedDate.DayOfWeek)?.Id;

            if (!timeSlotId.HasValue)
            {
                throw new CalifornianHealthException($"Timeslot {appointmentModel.SelectedTime} doesn't exist.");
            }

            string query = "INSERT INTO dbo.Appointment " +
                            "(ConsultantFk, PatientFk, TimeSlotFk, SelectedDate) " +
                            "VALUES (@ConsultantFk, @PatientFk, @TimeSlotFk, @SelectedDate);";

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ConsultantFk", SqlDbType.Int).Value = appointmentModel.SelectedConsultantId;
                    command.Parameters.Add("@PatientFk", SqlDbType.Int).Value = appointmentModel.SelectedPatientId;
                    command.Parameters.Add("@TimeSlotFk", SqlDbType.Int).Value = timeSlotId.Value;
                    command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = appointmentModel.SelectedDate;

                    try
                    {
                        await connection.OpenAsync().ConfigureAwait(false);
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2601) // duplicate error
                        {
                            throw new CalifornianHealthException($"Timeslot {appointmentModel.SelectedTime} has been booked already, please select another one.");
                        }

                        throw new CalifornianHealthException(ex.Message);
                    }
                }
            }
        }
    }
}
