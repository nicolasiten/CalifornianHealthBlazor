using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using CalifornianHealthBlazor.Interfaces;
using CalifornianHealthBlazor.Models;

namespace CalifornianHealthBlazor.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly IAsyncRepository<Consultant> _consultantRepository;

        public ConsultantService(IAsyncRepository<Consultant> consultantRepository)
        {
            _consultantRepository = consultantRepository;
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
    }
}
