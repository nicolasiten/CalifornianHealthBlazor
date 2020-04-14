using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using CalifornianHealthBlazor.Exceptions;
using CalifornianHealthBlazor.Interfaces;
using CalifornianHealthBlazor.Models;

namespace CalifornianHealthBlazor.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAsyncRepository<Consultant> _consultantRepository;
        private readonly IAsyncRepository<Patient> _patientRepository;
        private readonly IAsyncRepository<TimeSlot> _timeSlotRepository;
        private readonly IAsyncRepository<Appointment> _appointmentRepository;

        public AppointmentService(
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

        public async Task BuildAppointmentModelAsync(AppointmentModel appointmentModel)
        {
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
            var takenTimeSlots = await GetTakenTimeSlotsAsync(date, consultantId);

            return (await _timeSlotRepository.GetAllAsync(ts => takenTimeSlots.All(tts => tts != ts.Id)))
                .Select(ts => ts.Time);
        }

        public async Task SaveAppointmentAsync(AppointmentModel appointmentModel)
        {
            int? timeSlotId =
                (await _timeSlotRepository.GetAllAsync(ts => ts.Time == appointmentModel.SelectedTime))
                .SingleOrDefault()
                ?.Id;

            if (!timeSlotId.HasValue)
            {
                throw new CalifornianHealthException($"Timeslot {appointmentModel.SelectedTime} doesn't exist.");
            }

            try
            {
                await _appointmentRepository.AddAsync(new Appointment
                {
                    ConsultantFk = appointmentModel.SelectedConsultantId,
                    PatientFk = appointmentModel.SelectedPatientId,
                    TimeSlotFk = timeSlotId.Value,
                    SelectedDate = appointmentModel.SelectedDate
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<IEnumerable<int>> GetTakenTimeSlotsAsync(DateTime date, int consultantId)
        {
            return (await _appointmentRepository.GetAllAsync(a => a.ConsultantFk == consultantId && a.SelectedDate == date))
                .Select(a => a.TimeSlotFk.Value);
        }
    }
}
