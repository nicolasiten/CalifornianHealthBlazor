using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalifornianHealthBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalifornianHealthBlazor.Data.Config
{
    public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
    {
        public void Configure(EntityTypeBuilder<Consultant> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Firstname)
                .IsRequired();

            builder.Property(c => c.Lastname)
                .IsRequired();

            builder.Property(c => c.Specialty)
                .IsRequired();

            builder.ToTable("Consultant");
        }
    }
}
