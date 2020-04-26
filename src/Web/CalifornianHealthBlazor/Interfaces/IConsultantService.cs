using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealth.Common.Models;

namespace CalifornianHealthBlazor.Interfaces
{
    public interface IConsultantService
    {
        Task<IEnumerable<ConsultantModel>> GetConsultantsAsync();
    }
}
