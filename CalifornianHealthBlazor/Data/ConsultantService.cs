using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Models;

namespace CalifornianHealthBlazor.Data
{
    public class ConsultantService
    {
        public async Task<IEnumerable<ConsultantModel>> GetAllConsultants()
        {
            return new[]
            {
                new ConsultantModel
                {
                    Id = 1,
                    Firstname = "Peter",
                    Lastname = "Griffin",
                    Specialty = "Beer"
                },
                new ConsultantModel
                {
                    Id = 2,
                    Firstname = "Hansueli",
                    Lastname = "Jakob",
                    Specialty = "Knee"
                },
                new ConsultantModel
                {
                    Id = 3,
                    Firstname = "Andreas",
                    Lastname = "Koch",
                    Specialty = "Virus"
                }
            };
        }
    }
}
