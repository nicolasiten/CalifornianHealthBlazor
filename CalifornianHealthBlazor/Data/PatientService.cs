using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CalifornianHealthBlazor.Data
{
    public class PatientService
    {
        public async Task<IEnumerable<PatientModel>> GetAllPatients()
        {
            return new[]
            {
                new PatientModel
                {
                    Id = 1,
                    Address1 = "Address1",
                    Address2 = "Address2",
                    City = "City",
                    Firstname = "FPatient1",
                    Lastname = "LPatient1",
                    Postcode = "2323"
                },
                new PatientModel
                {
                    Id = 2,
                    Address1 = "Address1",
                    Address2 = "Address2",
                    City = "City",
                    Firstname = "FPatient2",
                    Lastname = "LPatient2",
                    Postcode = "1234"
                }
            };
        }
    }
}
