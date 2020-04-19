using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalifornianHealthBlazor.Data.Config
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Time)
                .IsRequired();

            builder.Property(ts => ts.DayOfWeek)
                .IsRequired();

            builder.HasIndex(ts => ts.ConsultantFk);

            builder.HasOne(a => a.Consultant)
                .WithMany(a => a.TimeSlots)
                .HasForeignKey(ts => ts.ConsultantFk)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("TimeSlot");
        }
    }
}
