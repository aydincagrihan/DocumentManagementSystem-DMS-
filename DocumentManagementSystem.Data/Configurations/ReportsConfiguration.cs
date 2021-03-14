using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data.Configurations
{
	class ReporstConfiguration : IEntityTypeConfiguration<Reports>
	{
        public void Configure(EntityTypeBuilder<Reports> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.ReportNo).IsRequired();
            builder.Property(x => x.ReportName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UserId).IsRequired();
            builder.ToTable("Report");
        }
    }
}
