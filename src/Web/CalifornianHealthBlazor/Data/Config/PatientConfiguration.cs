using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalifornianHealthBlazor.Data.Config
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Firstname)
                .IsRequired();

            builder.Property(p => p.Lastname)
                .IsRequired();

            builder.Property(p => p.Address1);

            builder.Property(p => p.Address2);

            builder.Property(p => p.City);

            builder.Property(p => p.Postcode);

            builder.ToTable("Patient");
        }
    }
}
