using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data.Configurations
{
	class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Code).IsRequired();
            //builder.Property(x => x.Semester).IsRequired();
            builder.Property(x => x.Credit).IsRequired();
            builder.Property(x => x.Descripton).HasMaxLength(500);
            builder.Property(x => x.ProgramsId).IsRequired();
            builder.Property(x => x.SemesterId).IsRequired();
            builder.ToTable("Course");
        }
    }
}
