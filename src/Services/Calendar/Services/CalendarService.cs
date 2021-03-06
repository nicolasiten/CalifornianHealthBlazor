﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Interfaces;
using CalifornianHealth.Common.Models;
using CalifornianHealthBlazor.Data.Entities;
using CalifornianHealthBlazor.Interfaces;

namespace Calendar.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IAsyncRepository<Consultant> _consultantRepository;
        private readonly IAsyncRepository<Patient> _patientRepository;
        private readonly IAsyncRepository<TimeSlot> _timeSlotRepository;
        private readonly IAsyncRepository<Appointment> _appointmentRepository;

        public CalendarService(
            IAsyncRepository<Consultant> consultantRepository,
            IAsyncRepository<Patient> patientRepository,
            IAsyncRepository<TimeSlot> timeSlotRepository,
            IAsyncRepository<Appointment> appointmentRepository)
        {
            _consultantRepository = consultantRepository;
            _patientRepository = patientRepository;
            _timeSlotRepository = timeSlotRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentModel> BuildAppointmentModelAsync()
        {
            AppointmentModel appointmentModel = new AppointmentModel();

            appointmentModel.Consultants.AddRange((await _consultantRepository.GetAllAsync()).Select(c => new ConsultantModel
            {
                Id = c.Id,
                Firstname = c.Firstname,
                Lastname = c.Lastname,
                Specialty = c.Specialty
            }));

            appointmentModel.Patients.AddRange((await _patientRepository.GetAllAsync()).Select(p => new PatientModel
            {
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.City,
                Postcode = p.Postcode
            }));

            return appointmentModel;
        }

        public async Task<IEnumerable<string>> GetFreeAppointmentTimesAsync(DateTime date, int consultantId)
        {
            var takenTimeSlots = await GetTakenTimeSlotsAsync(date, consultantId);

            if (date < DateTime.UtcNow.Date)
            {
                return new string[0];
            }

            return (await _timeSlotRepository.GetAllAsync(ts => takenTimeSlots.All(tts => tts != ts.Id)
                                                                && ts.ConsultantFk == consultantId
                                                                && ts.DayOfWeek == (int)date.DayOfWeek))
                .Select(ts => ts.Time)
                .OrderBy(DateTime.Parse);
        }

        public async Task<IEnumerable<ConsultantModel>> GetConsultantsAsync()
        {
            return (await _consultantRepository.GetAllAsync()).Select(c => new ConsultantModel
            {
                Id = c.Id,
                Firstname = c.Firstname,
                Lastname = c.Lastname,
                Specialty = c.Specialty
            });
        }

        private async Task<IEnumerable<int>> GetTakenTimeSlotsAsync(DateTime date, int consultantId)
        {
            return (await _appointmentRepository.GetAllAsync(a => a.ConsultantFk == consultantId && a.SelectedDate == date))
                .Select(a => a.TimeSlotFk.Value);
        }
    }
}
