using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalifornianHealthBlazor.Data.Config
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.SelectedDate)
                .IsRequired()
                .HasColumnType("date");

            builder.HasIndex(a => new {a.ConsultantFk, a.PatientFk, a.TimeSlotFk, a.SelectedDate}).IsUnique();

            builder.HasOne(a => a.Consultant)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ConsultantFk)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientFk)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.TimeSlot)
                .WithMany(ts => ts.Appointments)
                .HasForeignKey(a => a.TimeSlotFk)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Appointment");
        }
    }
}
