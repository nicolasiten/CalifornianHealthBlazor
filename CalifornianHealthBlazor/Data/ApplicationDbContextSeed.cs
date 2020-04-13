using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;

namespace CalifornianHealthBlazor.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (!dbContext.Consultants.Any())
            {
                Consultant[] consultants =
                {
                    new Consultant
                    {
                        Firstname = "Peter",
                        Lastname = "Griffin",
                        Specialty = "Beer"
                    },
                    new Consultant
                    {
                        Firstname = "Hansueli",
                        Lastname = "Jakob",
                        Specialty = "Knee"
                    },
                    new Consultant
                    {
                        Firstname = "Andreas",
                        Lastname = "Koch",
                        Specialty = "Virus"
                    }
                };

                dbContext.Consultants.AddRange(consultants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Patients.Any())
            {
                Patient[] patients = 
                {
                    new Patient
                    {
                        Address1 = "Address1",
                        Address2 = "Address2",
                        City = "City",
                        Firstname = "FPatient1",
                        Lastname = "LPatient1",
                        Postcode = "2323"
                    },
                    new Patient
                    {
                        Address1 = "Address1",
                        Address2 = "Address2",
                        City = "City",
                        Firstname = "FPatient2",
                        Lastname = "LPatient2",
                        Postcode = "1234"
                    }
                };

                dbContext.Patients.AddRange(patients);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
